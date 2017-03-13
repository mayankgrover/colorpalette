using Commons.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Commons.Popups
{
    public class ControllerPopupManager: MonoSingleton<ControllerPopupManager>
    {
        private List<ControllerBasePopup> popups = new List<ControllerBasePopup>();

        private const int NO_ACTIVE_POPUP = -1;
        private int popupActiveIndex = NO_ACTIVE_POPUP;

        protected override void Awake()
        {
            base.Awake();
            RegisterPopups();
            GetPopupIndex<ControllerPopupRevive>();
            GetPopupIndex<ControllerBasePopup>();
        }

        public void ShowPopup<T>() where T: ControllerBasePopup
        {
            if (popupActiveIndex != NO_ACTIVE_POPUP) Hide(popupActiveIndex);
            popupActiveIndex = GetPopupIndex<T>();
            if(popupActiveIndex != NO_ACTIVE_POPUP) Show(popupActiveIndex);
        }

        public void HidePopup<T>() where T: ControllerBasePopup
        {
            if (popupActiveIndex != NO_ACTIVE_POPUP) Hide(popupActiveIndex);
        }

        private int GetPopupIndex<T>() where T : ControllerBasePopup
        {
            Type popupType = typeof(T);
            int index = popups.FindIndex(popup => popup.GetType() == popupType);
            //Debug.Log("Index: " + index + " T-type: " + typeof(T) + " P-type: " + popups[index].GetType());
            return index;
        }

        private void RegisterPopups()
        {
            popups = GetComponentsInChildren<ControllerBasePopup>(includeInactive: true).ToList();
        }

        private void Show(int popupIndex)
        {
            popupActiveIndex = popupIndex;
            popups[popupActiveIndex].Show();
        }

        private void Hide(int popupIndex)
        {
            popups[popupIndex].Hide();
            this.popupActiveIndex = NO_ACTIVE_POPUP;
        }

    }
}
