
using Commons.Singleton;
using Commons.Util;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Commons.Notification
{
    public class ControllerNotificationMessage: MonoSingleton <ControllerNotificationMessage>
    {
        [SerializeField] private Text text;
        private Hashtable tweenOptions;

        protected override void Awake()
        {
            base.Awake();
            iTween.Init(text.gameObject);

            tweenOptions = new Hashtable();
            tweenOptions["scale"] = Vector3.one;
            tweenOptions["time"] = 1.5f;
            tweenOptions["oncomplete"] = "onTweenComplete";
            tweenOptions["easetype"] = iTween.EaseType.easeOutExpo;

            Disable();
        }

        public void ShowMessage(string message, float duration = 1.5f)
        {
            TweenUtil.CancelPendingTweens(text.gameObject);
            StopAllCoroutines();

            text.text = message;
            text.transform.localScale = Vector3.one * 0.1f;
            tweenOptions["time"] = duration;

            Enable();
            StartCoroutine(PlayTween(text.gameObject, duration));
        }

        private IEnumerator PlayTween(GameObject gameObject, float duration)
        {
            yield return new WaitForEndOfFrame();
            iTween.ScaleTo(gameObject, tweenOptions);
            yield return new WaitForSeconds(duration);
            Disable();
        }

        private void onTweenComplete()
        {
            Disable();
        }
    }
}
