using UnityEngine;

namespace Commons.Util
{
    public static class TweenUtil
    {
        public static void CancelPendingTweens(GameObject obj)
        {
            iTween.Stop(obj);
        }
    }
}
