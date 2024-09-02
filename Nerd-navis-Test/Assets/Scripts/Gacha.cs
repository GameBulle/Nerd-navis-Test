using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    int n_GachaID;
    Dictionary<int, float> d_WeightRandomDic = new();   // key : ItemID, Value : Weight

    public int GachaID => n_GachaID;

    public void Initialize(int ID, float NormalRate, float RareRate, float EpicRate, int RandomBagID)
    {
        n_GachaID = ID;
        List<int> l_ItemList = RandomBagTableManager.Instance.GetTableItemList(RandomBagID);
        int n_NormalCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Normal).Count;
        int n_RareCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Rare).Count;
        int n_EpicCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Epic).Count;

        for (int i = 0; i < l_ItemList.Count; i++)
        {
            Item item = ItemManager.Instance.GetItem(l_ItemList[i]);
            switch (item.Grade)
            {
                case ItemManager.ItemGrade.Normal:
                    d_WeightRandomDic[l_ItemList[i]] = NormalRate / n_NormalCount;
                    break;
                case ItemManager.ItemGrade.Rare:
                    d_WeightRandomDic[l_ItemList[i]] = RareRate / n_RareCount;
                    break;
                case ItemManager.ItemGrade.Epic:
                    d_WeightRandomDic[l_ItemList[i]] = EpicRate / n_EpicCount;
                    break;
            }
        }
    }

    Dictionary<int, int> PickRandomItem(int Time)
    {
        Dictionary<int, int> d_PickedItemDic = new();   // key : ItemID, Value : Count
        float weight = 0f;
        double pivot;

        for (int i = 0; i < Time; i++)
        {
            pivot = new System.Random().NextDouble() * 100;
            weight = 0f;
            foreach (KeyValuePair<int, float> d in d_WeightRandomDic)
            {
                weight += d.Value;
                if (pivot <= weight)
                {
                    if (d_PickedItemDic.ContainsKey(d.Key))
                        d_PickedItemDic[d.Key]++;
                    else
                        d_PickedItemDic.Add(d.Key, 1);
                    break;
                }
            }
        }
        return d_PickedItemDic;
    }
}
