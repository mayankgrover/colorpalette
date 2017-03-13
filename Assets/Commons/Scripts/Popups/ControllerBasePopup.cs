using UnityEngine;

namespace Commons.Popups
{
    public class ControllerBasePopup : MonoBehaviour
    {
        protected virtual void Awake() {
            RegisterClickHandlers();
        }

        protected virtual void Start() {
            Hide();
        }

        protected virtual void RegisterClickHandlers()
        {
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
