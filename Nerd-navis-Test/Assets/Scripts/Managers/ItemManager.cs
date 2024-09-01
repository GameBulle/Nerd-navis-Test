using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

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
        l_UpgradeValueList = new List<int>[l_Data2.Count - 1];
        for (int i = 0; i < l_Data2.Count - 1; i++)
            l_UpgradeValueList[i] = new();

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
}
