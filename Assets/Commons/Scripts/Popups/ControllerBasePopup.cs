using System;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.Popups
{
    public class ControllerBasePopup : MonoBehaviour
    {
        [SerializeField] private Button btnClosePopup;

        protected virtual void Awake() {
            RegisterClickHandlers();
        }

        protected virtual void Start() {
            Hide();
        }

        protected virtual void RegisterClickHandlers() {
            if(btnClosePopup != null) btnClosePopup.onClick.AddListener(OnClosePopup);
        }

        protected virtual void OnClosePopup() {
            Hide();
        }

        internal virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        internal virtual void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
