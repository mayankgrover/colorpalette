using UnityEngine;

namespace Commons.Util
{
    public static class TweenUtil
    {
        public static void CancelPendingTweens(GameObject obj)
        {
            //iTween[] tweens = obj.GetComponents<iTween>();
            //if(tweens.Length > 0)
            //{
            //    Debug.Log("killing tweens: " + tweens.Length);
            //    for(int i =0; i< tweens.Length; i++) {
            //        Component.Destroy(tweens[i]);
            //    }
            //}
            iTween.Stop(obj);
        }
    }
}
