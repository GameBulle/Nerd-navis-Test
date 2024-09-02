using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachMenu : MonoBehaviour
{
    [SerializeField] Inventory[] inventories;

    float f_RequireGachaPrice;
    int GachaID;

    public void Initialize(float gachaPrice)
    {
        f_RequireGachaPrice = gachaPrice;
        //Array.Sort(inventories, (x, y) => (int)x.ItemOption < (int)y.ItemOption ? 1 : -1);
        inventories[0].gameObject.SetActive(true);
        GachaID = 10000;
        for (int i = 0; i < inventories.Length; i++)
            inventories[i].Initialize();
    }

    public void OnClickGachaButton(int GachTime)
    {
        if (f_RequireGachaPrice * GachTime > GameManager.Instance.Money)
            return;
            
        GameManager.Instance.Money -= (int)f_RequireGachaPrice * GachTime;
        Gacha gacha = GachaManager.Instance.GetGacha(GachaID);

        Dictionary<int, int> d_PickedItemDic = gacha.PickRandomItem(GachTime);
        foreach (KeyValuePair<int, int> d in d_PickedItemDic)
        {
            ItemManager.Instance.CheckIncreaseItem(d.Key, d.Value);
            for(int i=0;i<inventories.Length;i++)
            {
                if (inventories[i].Type == ItemManager.Instance.GetItem(d.Key).Type)
                {
                    inventories[i].SetItem(d.Key);
                    break;
                }
            }
        }

        for (int i = 0; i < inventories.Length; i++)
            inventories[i].UpdateInventoryUI();
    }

    public void OnClickWeaponTab()
    {
        GachaID = 10000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i].Type == ItemManager.ItemOptionType.AttackIncrease)
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
        }
    }

    public void OnClickArmorTab()
    {
        GachaID = 20000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i].Type == ItemManager.ItemOptionType.DefenseIncrease)
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
        }
    }

    public void OnClickShieldTab()
    {
        GachaID = 30000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i].Type == ItemManager.ItemOptionType.HpIncrease)
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
        }
    }
}
