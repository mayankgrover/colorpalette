using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Commons.Tweens
{
    [RequireComponent(typeof(Graphic))]
    public class TweenChangingColorUI: MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Color primaryColor;
        [SerializeField] private Color secondaryColor;
        [SerializeField] private float toggleDuration;

        private Graphic graphic;
        private Color color;

        private void Awake()
        {
            graphic = GetComponent<Graphic>();
        }

        private void OnEnable()
        {
            StopAllCoroutines();
            StartCoroutine(ToggleColor());
        }

        private IEnumerator ToggleColor()
        {
            while(true)
            {
                //Debug.Log("running coroutine");
                graphic.color = primaryColor;
                yield return new WaitForSecondsRealtime(toggleDuration);
                graphic.color = secondaryColor;
                yield return new WaitForSecondsRealtime(toggleDuration);
            }
        }
    }
}
