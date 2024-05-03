using System;
using System.Collections.Generic;
using ComponentData;
using Cysharp.Threading.Tasks;
using Unity.Entities;
using UnityEngine;
using R3;
using Unity.Burst;
using Unity.Collections;
using Unity.Transforms;
using UnityEngine.Jobs;

namespace Components
{
    public class ECSManager : MonoBehaviour
    {
        [SerializeField] private InGameUI _inGameUI;
        [SerializeField] private InputProvider _provider;
        [SerializeField] private Transform _trooperPrefab;

        [SerializeField] private Landmark[] _landmarks;
        
        private EntityManager _entityManager;
        private Entity _playerEntity;

        private List<Transform> _trooperTransforms = new();

        private const int LandmarkLayer = 1 << 3;
        private async void OnEnable()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            await UniTask.WaitUntil(() => World.DefaultGameObjectInjectionWorld.IsCreated);
            // Entityを取得
            _playerEntity = _entityManager.CreateEntityQuery(typeof(PlayerData)).GetSingletonEntity();
            
            InitInputProvider();
            InitInGameUI();
            InitLandmark();
        }

        private void InitInputProvider()
        {
            _provider.OnClick.Subscribe(mousePosition => SelectLandmark(mousePosition));
        }

        private void InitInGameUI()
        {
            _inGameUI.OnSpawnTrooper.Subscribe(_ =>
            {
                _entityManager.AddComponent<SpawnTrooperTag>(_playerEntity);
            });
        }
        private void InitLandmark()
        {
            foreach (var landmark in _landmarks)
            {
                var entity = _entityManager.CreateEntity();
                _entityManager.SetName(entity, landmark.name);
                
                var landmarkData = new LandmarkData();
                landmarkData.Position = landmark.transform.position;
                _entityManager.AddComponentData(entity, landmarkData);

                landmark.Entity = entity;
            }
        }

        private void Update()
        {
            SetEnergyUI();
            ViewGameObjectByEntities();
        }
        
        private void SetEnergyUI()
        {
            var player = _entityManager.GetComponentData<PlayerData>(_playerEntity);
            _inGameUI.SetEnergy(player.Energy);
        }

        private void ViewGameObjectByEntities()
        {
            // ECS側の情報を取得
            var trooperEntities = 
                _entityManager.CreateEntityQuery(typeof(LocalTransform), typeof(TrooperData));
            var trooperEntitiesTransform = trooperEntities.ToComponentDataArray<LocalTransform>(Allocator.TempJob);

            // もし数が一致してなかったら新たなGameObjectを生成
            if (trooperEntitiesTransform.Length > _trooperTransforms.Count)
            {
                _trooperTransforms.Add(Instantiate(_trooperPrefab));
            }
            
            // ToArrayのタイミングを変えれば、アロケーションが減らせそう
            var trooperGameObjectsTransform 
                = new TransformAccessArray(_trooperTransforms.ToArray());
            
            var jobHandle = new MoveTrooperJob
            {
                LocalTransforms = trooperEntitiesTransform
            }.Schedule(trooperGameObjectsTransform);
            
            jobHandle.Complete();
            
            trooperEntitiesTransform.Dispose();
        }
        private void SelectLandmark(Vector2 mousePosition)
        {
            var ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, Single.PositiveInfinity, LandmarkLayer))
            {
                var landmarkEntity = hit.collider.GetComponent<Landmark>().Entity;
                _entityManager.AddComponent<SelectLandmarkTag>(landmarkEntity);
            }
        }
        
        [BurstCompile]
        private struct MoveTrooperJob : IJobParallelForTransform
        {
            [ReadOnly] public NativeArray<LocalTransform> LocalTransforms;
            
            public void Execute(int index, TransformAccess transform)
            {
                transform.position = LocalTransforms[index].Position;
            }
        }
    }
}