using System;
using System.Collections;
using UnityEngine;

namespace Commons.Tweens
{
    public class TweenEffectPulsating : MonoBehaviour
    {
        [SerializeField] private bool onDisableRestart;
        [SerializeField] private float duration;
        [SerializeField] private float delay;
        [SerializeField] private float scaleFactor;
        [SerializeField] private iTween.LoopType loopType;
        [SerializeField] private iTween.EaseType easeType;

        private void Awake()
        {
            iTween.Init(gameObject);
        }

        private void OnEnable()
        {
            StartCoroutine(PlayAnimation(delay));
        }

        private void OnDisable()
        {
            DestroyExistingTween();
        }

        private void DestroyExistingTween()
        {
            iTween tween = GetComponent<iTween>();
            if (tween != null) Destroy(tween);
        }

        private IEnumerator PlayAnimation(float delayInSeconds = 0f)
        {
            yield return new WaitForSeconds(delayInSeconds);
            iTween.ScaleTo(gameObject, new Hashtable() {
                { "scale", transform.localScale * scaleFactor },
                { "time", duration },
                //{ "delay", delay },
                { "easetype", easeType },
                { "looptype", loopType },
            });
        }

    }
}
