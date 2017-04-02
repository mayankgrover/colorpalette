using System;
using System.Collections.Generic;
using UnityEngine;

namespace Commons.Util
{
    /// <summary>
    /// Loads Dynamic IViews in the Resources/Views folder
    /// @author Mayank
    /// </summary>
    public class LoadViewUtil
    {
        private class ViewNotFoundException : Exception {
            public ViewNotFoundException(string viewPath) : base(viewPath) { }
        }

        private const string VIEW_PREFAB_PREFIX_PATH = "Views/";
        private static readonly Dictionary<string, GameObject> prefabsLoaded = new Dictionary<string, GameObject>();

        public static V LoadPrefab<V>(Transform parent) where V : IView
        {
            string viewName = typeof(V).Name;
            string prefabLocation = VIEW_PREFAB_PREFIX_PATH + viewName;

            GameObject prefab = IsViewAlreadyLoaded(viewName) ? 
                prefabsLoaded[viewName] : 
                Resources.Load<GameObject>(prefabLocation);

            if (prefab == null) {
                throw new ViewNotFoundException(prefabLocation);
            }

            prefabsLoaded[viewName] = prefab;

            GameObject gameObject = GameObject.Instantiate(prefab);
            gameObject.transform.SetParent(parent, false);
            return gameObject.GetComponent<V>();
        }

        private static bool IsViewAlreadyLoaded(string viewName)
        {
            return prefabsLoaded.ContainsKey(viewName);
        }
    }
}
