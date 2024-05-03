using System;
using UnityEngine;

namespace Components.Base
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UIBase : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        public void SetUIActive(bool isActive)
        {
            _canvasGroup.blocksRaycasts = isActive;
            _canvasGroup.alpha = isActive ? 1 : 0;
        }
        
        #if UNITY_EDITOR
        private void Reset()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
#endif
    }
}