using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    int n_ItemID;
    ItemManager.ItemGrade e_ItemGrade;
    ItemManager.ItemOptionType e_ItemOptionType;
    int n_Value;
    int n_Count;
    int n_Level;
    Sprite ItemIcon;

    public int ItemID => n_ItemID;
    public ItemManager.ItemGrade Grade => e_ItemGrade;
    public ItemManager.ItemOptionType Type => e_ItemOptionType;
    public int Value { get { return n_Value; } set { n_Value = value; } }
    public int Count { get { return n_Count; } set { n_Count = value; } }
    public int Level { get { return n_Level; } set { n_Level = value; } }
    public Sprite Icon => ItemIcon;

    public void Initialize(int ID, ItemManager.ItemGrade grade, ItemManager.ItemOptionType type, int DefaultValue, string Path)
    {
        n_ItemID = ID;
        e_ItemGrade = grade;
        e_ItemOptionType = type;
        n_Value = DefaultValue;
        n_Count = 0;
        n_Level = 0;
        ItemIcon = Resources.Load<Sprite>(Path);
    }
}
