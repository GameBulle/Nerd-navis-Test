using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;  

public class ItemManager : MonoBehaviour
{
    static ItemManager instance;
    public static ItemManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ItemManager>();
                if(!instance)
                    instance = new GameObject("ItemManager").AddComponent<ItemManager>();
            }
            return instance;
        }
    }

    public enum ItemGrade { Normal, Rare, Epic }
    public enum ItemOptionType { AttackIncrease, DefenseIncrease, HpIncrease }

    List<Item> l_ItemList;

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);

        l_ItemList = new();
    }

    public void Initialize()
    {
        List<Dictionary<string, string>> l_Data = CSV.Read("ItemList");
        for(int i=1;i<l_Data.Count;i++)
        {
            Item i_Item =  gameObject.AddComponent<Item>();
            i_Item.Initialize(
                int.Parse(l_Data[i]["n_ItemID"]),
                (ItemGrade)Enum.Parse(typeof(ItemGrade), l_Data[i]["e_ItemGrade"]),
                (ItemOptionType)Enum.Parse(typeof(ItemOptionType), l_Data[i]["e_ItemOptionType"]),
                int.Parse(l_Data[i]["n_DefaultValue"]),
                l_Data[i]["s_IconPath"]);
            l_ItemList.Add(i_Item);
        }
    }
}
