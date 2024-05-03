using System;
using Cysharp.Threading.Tasks;
using R3;
using Unity.Entities;
using UnityEngine;

namespace Components
{
    public class InputProvider : MonoBehaviour
    {
        public Observable<Vector2> OnClick => _onClick;
        
        private CastleInput _input;
        private EntityManager _entityManager;

        private readonly Subject<Vector2> _onClick = new();
        private void Start()
        {
            InitInput();
        }

        private void InitInput()
        {
            _input = new();
            _input.InGameAction.Enable();
            
            _input.InGameAction.Click.Enable();
            _input.InGameAction.Click.performed += _ => 
                _onClick.OnNext(_input.InGameAction.MousePosition.ReadValue<Vector2>());
            
            _input.InGameAction.MousePosition.Enable();
        }
    }
}