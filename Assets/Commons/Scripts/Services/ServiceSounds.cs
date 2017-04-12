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

        public void PlaySoundEffect(SoundEffect sfx)
        {
            SoundPair pair = audioClips.Find(x => x.effect == sfx);
            if (pair.clip != null) {
                soundEffects.PlayOneShot(pair.clip);
            }
        }
    }
}
