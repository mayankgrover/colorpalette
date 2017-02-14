using UnityEngine;

namespace Commons.Scripts
{
    public class ZoomUtil
    {
        public static float GetTouchDeltaMagnitude()
        {
            float deltaTouchMag = 0f;
            if (Input.touchCount >= 2) {

                Touch touchZero = Input.GetTouch(0);
                Touch touchFirst = Input.GetTouch(1);

                Vector2 touchPreviousZeroPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchPreviousFirstPos = touchFirst.position - touchFirst.deltaPosition;

                float touchPreviousDeltaMag = (touchPreviousZeroPos - touchPreviousFirstPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchFirst.position).magnitude;

                deltaTouchMag = touchDeltaMag - touchPreviousDeltaMag;
            }

            return deltaTouchMag;
        }
    }
}
