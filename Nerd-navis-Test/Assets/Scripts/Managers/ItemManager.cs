using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Drawing;

public class ItemManager : MonoBehaviour
{
    static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
                if (!instance)
                    instance = new GameObject("ItemManager").AddComponent<ItemManager>();
            }
            return instance;
        }
    }

    public enum ItemGrade { Normal, Rare, Epic }
    public enum ItemOptionType { AttackIncrease, DefenseIncrease, HpIncrease }

    List<Item> l_ItemList;
    List<Vector2> l_LimitAndCostList;   // x : 강화 단계 이하 조건, y : 단계 별 요구 비용
    List<int>[] l_UpgradeValueList;

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);

        l_ItemList = new();
        l_LimitAndCostList = new();

        int n_Size = System.Enum.GetValues(typeof(ItemManager.ItemGrade)).Length;
        l_UpgradeValueList = new List<int>[n_Size];
        for (int i = 0; i < n_Size; i++)
            l_UpgradeValueList[i] = new();

        Initialize();
    }

    public void Initialize()
    {
        List<Dictionary<string, string>> l_Data = CSV.Read("ItemList");
        for (int i = 1; i < l_Data.Count; i++)
        {
            Item i_Item = gameObject.AddComponent<Item>();
            i_Item.Initialize(
                int.Parse(l_Data[i]["n_ItemID"]),
                (ItemGrade)Enum.Parse(typeof(ItemGrade), l_Data[i]["e_ItemGrade"]),
                (ItemOptionType)Enum.Parse(typeof(ItemOptionType), l_Data[i]["e_ItemOptionType"]),
                int.Parse(l_Data[i]["n_DefaultValue"]),
                l_Data[i]["s_IconPath"]);
            l_ItemList.Add(i_Item);
        }

        List<Dictionary<string, string>> l_Data2 = CSV.Read("ItemOptionUpgrade");
        for (int i = 1; i < l_Data2.Count; i++)
        {
            Vector2 v;
            v.x = int.Parse(l_Data2[i]["n_UpgradeBelowLimit"]);
            v.y = int.Parse(l_Data2[i]["n_UpgradeCost"]);
            l_LimitAndCostList.Add(v);
            l_UpgradeValueList[(int)ItemManager.ItemGrade.Normal].Add(int.Parse(l_Data2[i]["n_NormalUpgradeValue"]));
            l_UpgradeValueList[(int)ItemManager.ItemGrade.Rare].Add(int.Parse(l_Data2[i]["n_RareUpgradeValue"]));
            l_UpgradeValueList[(int)ItemManager.ItemGrade.Epic].Add(int.Parse(l_Data2[i]["n_EpicUpgradeValue"]));
        }
    }

    public Item GetItem(int ItemID)
    {
        int n_Index = l_ItemList.FindIndex(x => x.ItemID == ItemID);
        return (n_Index != -1 ? l_ItemList[n_Index] : null);
    }

    public void CheckIncreaseItem(int ItemID, int Count)
    {
        int n_Index = l_ItemList.FindIndex(x => x.ItemID == ItemID);
        if(n_Index == -1)
        {
            Debug.LogError("Item Null");
            return;
        }

        if (l_ItemList[n_Index].Level == 0)
            l_ItemList[n_Index].Level++;
        l_ItemList[n_Index].Count += Count;
        IncreaseItem(l_ItemList[n_Index]);
    }

    void IncreaseItem(Item item)
    {
        int n_Step = 0;
        while (l_LimitAndCostList[n_Step].x > item.Level) { n_Step++; }

        if (item.Count >= l_LimitAndCostList[n_Step].y)
        {
            item.Count -= (int)l_LimitAndCostList[n_Step].y;
            item.Level++;
            item.Value += l_UpgradeValueList[(int)item.Grade][n_Step];
            IncreaseItem(item);
        }
        else
            item.UpgradeRequire = (int)l_LimitAndCostList[n_Step].y;
    }
}
