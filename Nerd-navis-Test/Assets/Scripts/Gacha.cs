using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    int n_GachaID;
    Dictionary<int, float> d_WeightRandomDic = new();   // key : ItemID, Value : Weight

    public int GachaID => n_GachaID;

    // RandomBagID�� �´� Table�� ȹ�� ������ �� Item�� ����ġ �ο�
    public void Initialize(int ID, float NormalRate, float RareRate, float EpicRate, int RandomBagID)
    {
        n_GachaID = ID;
        List<int> l_ItemList = RandomBagTableManager.Instance.GetTableItemList(RandomBagID);
        int n_NormalCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Normal).Count;   // Normal ��� ��� ��
        int n_RareCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Rare).Count;   // Rare ��� ��� ��
        int n_EpicCount = l_ItemList.FindAll(x => ItemManager.Instance.GetItem(x).Grade == ItemManager.ItemGrade.Epic).Count;   // Epic ��� ��� ��

        for (int i = 0; i < l_ItemList.Count; i++)
        {
            Item item = ItemManager.Instance.GetItem(l_ItemList[i]);
            switch (item.Grade)
            {
                case ItemManager.ItemGrade.Normal:
                    d_WeightRandomDic[l_ItemList[i]] = NormalRate / n_NormalCount;  // Normal �̱� Ȯ�� / Normal ��� ��� ��
                    break;
                case ItemManager.ItemGrade.Rare:
                    d_WeightRandomDic[l_ItemList[i]] = RareRate / n_RareCount;     // Rare �̱� Ȯ�� / Rare ��� ��� ��
                    break;
                case ItemManager.ItemGrade.Epic:
                    d_WeightRandomDic[l_ItemList[i]] = EpicRate / n_EpicCount;  // Epic �̱� Ȯ�� / Epic ��� ��� ��
                    break;
            }
        }
    }

    public Dictionary<int, int> PickRandomItem(int Time)    // ����ġ ���� �̱� �Լ�
    {
        Dictionary<int, int> d_PickedItemDic = new();   // key : ItemID, Value : Count
        float f_Weight = 0f;    // ���� ����ġ
        double d_Pivot; // ���� ����ġ ����

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
