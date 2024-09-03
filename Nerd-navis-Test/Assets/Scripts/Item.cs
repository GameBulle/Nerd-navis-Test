using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    int n_ItemID;   // 아이템의 ID
    ItemManager.ItemGrade e_ItemGrade;  // 아이템의 등급
    ItemManager.ItemOptionType e_ItemOptionType;    // 아이템의 능력치 타입
    int n_Value;    // 아이템의 현재 능력치
    int n_Count;    // 아이템의 현재 수량
    int n_Level;    // 아이템의 현재 레벨
    int n_UpgradeRequire;   // 다음 업그레이드에 필요한 수량
    Sprite ItemIcon;

    public int ItemID => n_ItemID;
    public ItemManager.ItemGrade Grade => e_ItemGrade;
    public ItemManager.ItemOptionType Type => e_ItemOptionType;
    public int Value { get { return n_Value; } set { n_Value = value; } }
    public int Count { get { return n_Count; } set { n_Count = value; } }
    public int Level { get { return n_Level; } set { n_Level = value; } }
    public int UpgradeRequire {  get { return n_UpgradeRequire; } set { n_UpgradeRequire = value; } }
    public Sprite Icon => ItemIcon;

    public void Initialize(int ID, ItemManager.ItemGrade grade, ItemManager.ItemOptionType type, int DefaultValue, string Path)
    {
        n_ItemID = ID;
        e_ItemGrade = grade;
        e_ItemOptionType = type;
        n_Value = DefaultValue;
        n_Count = -1;
        n_Level = 0;
        ItemIcon = Resources.Load<Sprite>(Path);
    }
}
