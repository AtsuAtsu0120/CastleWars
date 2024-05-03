using Components.Base;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class InGameUI : UIBase
    {
        public Observable<Unit> OnSpawnTrooper => _onSpawnTrooper;
        [SerializeField] private Slider _energySlider;

        private Subject<Unit> _onSpawnTrooper = new();
        public void SetEnergy(float value)
        {
            _energySlider.value = value;
        }

        public void SpawnTrooper()
        {
            _onSpawnTrooper.OnNext(Unit.Default);
        }
    }
}