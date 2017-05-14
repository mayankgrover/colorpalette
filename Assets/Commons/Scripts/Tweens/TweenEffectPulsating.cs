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

        private Vector3 localScale;

        private void Awake()
        {
            localScale = transform.localScale;
            iTween.Init(gameObject);
        }

        private void OnEnable()
        {
            transform.localScale = localScale;
            StartCoroutine(PlayAnimation(delay));
        }

        private void OnDisable()
        {
            DestroyExistingTween();
        }

        private void DestroyExistingTween()
        {
            //iTween tween = GetComponent<iTween>();
            //if (tween != null) Destroy(tween);
            iTween.Stop(gameObject);
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
