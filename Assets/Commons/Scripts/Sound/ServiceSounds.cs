using Commons.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Commons.Services
{
    public enum SoundEffect
    {
        UI_Button_Click,
        Game_Success,
        Game_Bonus,
        Game_Level_Cleared,
        Game_Over,
        Game_PlayerClick,
        Game_PlayerSwipe,
    }

    [Serializable]
    public struct SoundPair
    {
        [SerializeField] public SoundEffect effect;
        [SerializeField] public AudioClip clip;
    }

    public class ServiceSounds: MonoSingleton<ServiceSounds>
    {
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource soundEffects;

        [SerializeField] private List<SoundPair> audioClips;

        private bool isMute = false;
        public bool IsMute { get { return isMute; } }

        protected override void Start()
        {
            base.Start();
            LoadSoundSetting();
        }

        private void LoadSoundSetting()
        {
            isMute = PlayerPrefs.GetInt(StringConstants.MUTE_STATUS, 0) == 1 ? 
                true : false;
        }

        private void SaveSoundSetting()
        {
            PlayerPrefs.SetInt(StringConstants.MUTE_STATUS, isMute == true ? 1 : 0);
        }

        public void PlaySoundEffect(SoundEffect sfx)
        {
            SoundPair pair = audioClips.Find(x => x.effect == sfx);
            if (pair.clip != null) {
                soundEffects.PlayOneShot(pair.clip);
            }
        }

        public void SetMute(bool status)
        {
            isMute = status;
            if (soundEffects != null) soundEffects.mute = status;
            if (backgroundMusic != null) backgroundMusic.mute = status;
            SaveSoundSetting();
        }
    }
}
