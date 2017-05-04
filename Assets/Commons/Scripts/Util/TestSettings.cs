using Commons.Singleton;
using UnityEngine;

namespace Commons.Util
{
    public class TestSettings: MonoSingleton<TestSettings>
    {
        [SerializeField] private bool isPlayerGod;

        public bool CanPlayerDie { get { return !(Debug.isDebugBuild && isPlayerGod); } }
    }
}
