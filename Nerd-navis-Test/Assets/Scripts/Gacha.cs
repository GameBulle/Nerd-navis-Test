using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    int n_GachaID;
    Dictionary<int, float> d_WeightRandomDic = new();   // key : ItemID, Value : Weight

    public int GachaID => n_GachaID;

    // RandomBagID에 맞는 Table의 획득 가능한 각 Item에 가중치 부여
    public void Initialize(int ID, float NormalRate, float RareRate, float EpicRate, int RandomBagID)
    {
        n_GachaID = ID;
        List<int> l_ItemList = RandomBagTableManager.Instance.GetTableItemList(RandomBagID);
        int n_NormalCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Normal).Count;   // Normal 등급 장비 수
        int n_RareCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Rare).Count;   // Rare 등급 장비 수
        int n_EpicCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Epic).Count;   // Epic 등급 장비 수

        for (int i = 0; i < l_ItemList.Count; i++)
        {
            Item item = ItemManager.Instance.GetItem(l_ItemList[i]);
            switch (item.Grade)
            {
                case ItemManager.ItemGrade.Normal:
                    d_WeightRandomDic[l_ItemList[i]] = NormalRate / n_NormalCount;  // Normal 뽑기 확률 / Normal 등급 장비 수
                    break;
                case ItemManager.ItemGrade.Rare:
                    d_WeightRandomDic[l_ItemList[i]] = RareRate / n_RareCount;     // Rare 뽑기 확률 / Rare 등급 장비 수
                    break;
                case ItemManager.ItemGrade.Epic:
                    d_WeightRandomDic[l_ItemList[i]] = EpicRate / n_EpicCount;  // Epic 뽑기 확률 / Epic 등급 장비 수
                    break;
            }
        }
    }

    public Dictionary<int, int> PickRandomItem(int Time)    // 가중치 랜덤 뽑기 함수
    {
        Dictionary<int, int> d_PickedItemDic = new();   // key : ItemID, Value : Count
        float f_Weight = 0f;    // 현재 가중치
        double d_Pivot; // 뽑을 가중치 기준

        for (int i = 0; i < Time; i++)
        {
            d_Pivot = new System.Random().NextDouble() * 100;   // [0.0, 1.0) * 100
            f_Weight = 0f;
            foreach (KeyValuePair<int, float> d in d_WeightRandomDic)
            {
                f_Weight += d.Value;
                if (d_Pivot <= f_Weight)
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
