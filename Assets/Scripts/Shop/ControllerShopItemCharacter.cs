
using System;
using UnityEngine;
using UnityEngine.UI;

public class ControllerShopItemCharacter: MonoBehaviour 
{
    private int Id;
    private bool IsPurchased;
    private int Cost;

    public Action<int> OnBuyCharacter;
    public Action<int> OnSelectCharacter;

    [SerializeField] private Image sprite; 
    [SerializeField] private Text cost; 
    [SerializeField] private Image isPurchased; 
    [SerializeField] private Image isSelected; 
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    public void Init(ShopCharacterData characterData)
    {
        UpdateData(characterData);
        UpdateUI(characterData);
        gameObject.SetActive(true);
    }

    private void UpdateData(ShopCharacterData characterData)
    {
        Id = characterData.Id;
        Cost = characterData.Cost;
        IsPurchased = characterData.IsPurchased;
    }

    public void Refresh(ShopCharacterData characterData)
    {
        UpdateData(characterData);
        UpdateUI(characterData);
    }

    private void UpdateUI(ShopCharacterData data)
    {
        sprite.sprite = data.Image;
        cost.text = data.Cost + "c";
        //isPurchased.enabled = !data.IsPurchased;
        isPurchased.gameObject.SetActive(!data.IsPurchased);
        isSelected.enabled = data.IsSelected;
    }

    private void OnButtonClick()
    {
        if (IsPurchased) {
            UpdateSelectedCharacter();
        }
        else if (Cost <= PlayerProfile.Instance.Coins) {
            BuyPlayerCharacter();
        }
        else {
            NotEnoughCoins();
        }
    }

    private void NotEnoughCoins()
    {
        Debug.LogError("Not enough coins:" + Cost + " balance:" + PlayerProfile.Instance.Coins);
    }

    private void BuyPlayerCharacter()
    {
        //Debug.Log("Trying to buy character: " + Id);
        if (OnBuyCharacter != null) OnBuyCharacter(Id);
    }

    private void UpdateSelectedCharacter()
    {
        //Debug.Log("Select character:" + Id);
        if (OnSelectCharacter != null) OnSelectCharacter(Id);
    }
}
