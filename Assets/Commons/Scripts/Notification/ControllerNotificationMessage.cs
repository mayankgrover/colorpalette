
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

        private Vector3 defPosition;
        private Hashtable tweenOptions;

        protected override void Awake()
        {
            base.Awake();
            defPosition = transform.position;
            iTween.Init(gameObject);

            tweenOptions = new Hashtable();
            tweenOptions["scale"] = Vector3.one;
            tweenOptions["time"] = 2f;
            tweenOptions["oncomplete"] = "onTweenComplete";
            tweenOptions["easetype"] = iTween.EaseType.easeOutExpo;
        }

        public void ShowMessage(string message, float duration = 1.5f)
        {
            TweenUtil.CancelPendingTweens(gameObject);
            text.text = message;
            transform.localScale = Vector3.one * 0.5f;
            tweenOptions["time"] = duration;
            Enable();
            StartCoroutine(PlayTween(gameObject));
        }

        private IEnumerator PlayTween(GameObject gameObject)
        {
            yield return new WaitForEndOfFrame();
            iTween.ScaleTo(gameObject, tweenOptions);
        }

        private void onTweenComplete()
        {
            Disable();
        }
    }
}
