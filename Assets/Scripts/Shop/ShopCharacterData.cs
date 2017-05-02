
using System;
using UnityEngine;

[Serializable]
public class ShopCharacterData
{
    [SerializeField] public int Id;
    [SerializeField] public int Cost;
    [SerializeField] public Sprite Image;
    [SerializeField] public bool IsPurchased;
    [SerializeField] public bool IsSelected;

    public ShopCharacterData() {
    }

    public ShopCharacterData(ShopCharacterData duplicate)
    {
        this.Id = duplicate.Id;
        this.Cost = duplicate.Cost;
        this.Image = duplicate.Image;
        this.IsPurchased = duplicate.IsPurchased;
        this.IsSelected = duplicate.IsSelected;
    }
}
