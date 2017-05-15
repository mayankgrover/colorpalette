using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.SocialMedia
{
    [RequireComponent(typeof(Button))]
    public class ControllerButtonTwitter: MonoBehaviour
    {
        private Button button;

        private void Start()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(onClickButton);
        }

        private void onClickButton()
        {
            StartCoroutine(OpenTwitterProfile());
        }

        private IEnumerator OpenTwitterProfile()
        {
            DateTime clickTime = DateTime.Now;
            Application.OpenURL(string.Format(StringConstants.TWITTER_APP_URL, StringConstants.TWITTER_USERNAME));
            yield return new WaitForSecondsRealtime(1.5f);

            if((DateTime.Now - clickTime).TotalSeconds <= 2.5f) {
                Application.OpenURL(string.Format(StringConstants.TWITTER_WEB_URL, StringConstants.TWITTER_USERNAME));
            }
        }
    }
}
