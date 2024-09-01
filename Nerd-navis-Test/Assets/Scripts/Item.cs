using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    int n_ItemID;
    ItemManager.ItemGrade e_ItemGrade;
    ItemManager.ItemOptionType e_ItemOptionType;
    int n_Value;
    Sprite ItemIcon;

    public int ItemID => n_ItemID;
    public ItemManager.ItemGrade Grade => e_ItemGrade;
    public ItemManager.ItemOptionType Type => e_ItemOptionType;
    public int Value => n_Value;
    public Sprite Icon => ItemIcon;

    public void Initialize(int ID, ItemManager.ItemGrade grade, ItemManager.ItemOptionType type, int DefaultValue, string Path)
    {
        n_ItemID = ID;
        e_ItemGrade = grade;
        e_ItemOptionType = type;
        n_Value = DefaultValue;
        ItemIcon = Resources.Load<Sprite>(Path);
    }
}
