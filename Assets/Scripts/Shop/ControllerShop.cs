
using Commons.Singleton;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

using PlayerPrefs = ZPlayerPrefs;
using Commons.Services;

public class ControllerShop: MonoSingleton<ControllerShop>
{
    [SerializeField] private Button backButton;
    [SerializeField] private Text Coins;
    [SerializeField] private Transform CharactersContent;

    [SerializeField] private ControllerShopItemCharacter PrefabCharacterShopItem;
    [SerializeField] private List<ShopCharacterData> characterData;

    private List<ShopCharacterData> characterShopData;
    private Dictionary<int, ControllerShopItemCharacter> characters;

    protected override void Awake()
    {
        base.Awake();
        backButton.onClick.AddListener(onBackClick);
        Disable();
    }

    private void onBackClick()
    {
        ServiceSounds.Instance.PlaySoundEffect(SoundEffect.UI_Button_Click);
        Disable();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape)) {
    //        onBackClick();
    //    }
    //}

    protected override void Start()
    {
        base.Start();
        PlayerProfile.Instance.OnCoinsUpdated += UpdateUI;
        Init();
    }

    public Sprite GetSelectedPlayerSprite()
    {
        return characterData.Find(c => c.Id == PlayerProfile.Instance.SelectedCharacterId).Image;
    }

    public override void Enable()
    {
        base.Enable();
        UpdateUI();
    }

    private void UpdateUI()
    {
        Coins.text = PlayerProfile.Instance.Coins + "c";
    }

    private void Init()
    {
        LoadData();
        GenerateCharacters();
        UpdateUI();
    }

    private void LoadData()
    {
        characterShopData = new List<ShopCharacterData>();
        for(int i = 0; i < characterData.Count; i++) {
            ShopCharacterData data = characterData[i];
            ShopCharacterData newData = new ShopCharacterData(data);
            LoadCharacterData(newData);
            newData.IsSelected = characterData[i].Id == PlayerProfile.Instance.SelectedCharacterId;
            characterShopData.Add(newData);
        }
    }

    private void GenerateCharacters()
    {
        characters = new Dictionary<int, ControllerShopItemCharacter>();
        foreach(ShopCharacterData data in characterShopData)
        {
            ControllerShopItemCharacter character = GameObject.Instantiate<ControllerShopItemCharacter>
                (PrefabCharacterShopItem, parent: CharactersContent, worldPositionStays: false);
            character.Init(data);
            character.OnBuyCharacter += onBuyCharacter;
            character.OnSelectCharacter += onSelectCharacter;
            characters.Add(data.Id, character);
        }
    }

    private void onSelectCharacter(int id)
    {
        //Debug.Log("SelectCharacter:" + id);
        ShopCharacterData prevSelected = GetShopCharacterData(PlayerProfile.Instance.SelectedCharacterId);
        ShopCharacterData newSelected = GetShopCharacterData(id);
        prevSelected.IsSelected = false;
        newSelected.IsSelected = true;
        characters[prevSelected.Id].Refresh(prevSelected);
        characters[newSelected.Id].Refresh(newSelected);
        PlayerProfile.Instance.UpdateSelectedCharacter(id);
        SaveCharacterInfo(prevSelected);
        SaveCharacterInfo(newSelected);
    }

    private void onBuyCharacter(int id)
    {
        Debug.Log("BuyCharacter:" + id);
        ShopCharacterData character = GetShopCharacterData(id);
        PlayerProfile.Instance.UpdateCoins(-1 * character.Cost);
        character.IsPurchased = true;
        SaveCharacterInfo(character);
        onSelectCharacter(id);
    }

    private ShopCharacterData GetShopCharacterData(int id) {
        return characterShopData.Find(c => c.Id == id);
    }

    #region PLAYER PREF FOR SHOP 

    private void SaveCharacterInfo(ShopCharacterData data)
    {
        string keyPurchased = "Character_" + data.Id + "_Purchased";
        PlayerPrefs.SetInt(keyPurchased, data.IsPurchased == true ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadCharacterData(ShopCharacterData data)
    {
        string keyPurchased = "Character_" + data.Id + "_Purchased";
        data.IsPurchased = PlayerPrefs.GetInt(keyPurchased, 0) == 1 ? true : false;
        //return data;
    }

    #endregion PLAYER PREF FOR SHOP 

}
