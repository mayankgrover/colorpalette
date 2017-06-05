using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    [RequireComponent(typeof(Button))]
    public class TestAddCoins: MonoBehaviour
    {
        private Button addCoins;

        private void Awake()
        {
            if(Debug.isDebugBuild == false) {
                Destroy(this);
            }

            addCoins = GetComponent<Button>();
            addCoins.onClick.AddListener(onClickAddCoins);
        }

        private void onClickAddCoins()
        {
            PlayerProfile.Instance.UpdateCoins(500);
        }
    }
}
