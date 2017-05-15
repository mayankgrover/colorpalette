using Commons.Services;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.Sound
{
    [RequireComponent(typeof(Toggle))]
    public class ControllerSound: MonoBehaviour
    {
        private Toggle setting;

        private void Awake()
        {
            setting = GetComponent<Toggle>();
            setting.onValueChanged.AddListener(onSettingChanged);
        }

        private void OnEnable()
        {
            setting.isOn = ServiceSounds.Instance.IsMute;
        }

        private void onSettingChanged(bool status)
        {
            ServiceSounds.Instance.SetMute(status);
        }
    }
}
