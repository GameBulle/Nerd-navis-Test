using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] protected Image OutLine;
    [SerializeField] protected Image ItemIcon;
    [SerializeField] protected Image ItemLevelImage;
    [SerializeField] protected TextMeshProUGUI ItemCountText;

    protected int n_ItemID;

    void SetItemSlot( )
    {
        Item item = ItemManager.Instance.GetItem(n_ItemID);
        if (item == null)
            throw new Exception("Item is Null");

        Color color = Color.white;
        switch (item.Grade)
        {
            case ItemManager.ItemGrade.Normal:
                ColorUtility.TryParseHtmlString("#9DA8B6", out color);
                OutLine.sprite = Resources.Load<Sprite>("Image/Ui/ItemSlot_Normal");
                break;
            case ItemManager.ItemGrade.Rare:
                ColorUtility.TryParseHtmlString("#30AF52", out color);
                OutLine.sprite = Resources.Load<Sprite>("Image/Ui/ItemSlot_Rare");
                break;
            case ItemManager.ItemGrade.Epic:
                ColorUtility.TryParseHtmlString("#41AEEE", out color);
                OutLine.sprite = Resources.Load<Sprite>("Image/Ui/ItemSlot_Epic");
                break;
        }

        ItemIcon.sprite = item.Icon;
        if(ItemLevelImage != null)
            ItemLevelImage.color = color;
    }

    public virtual void SetInventoryItemSlot(int ItemID) 
    {
        n_ItemID = ItemID;
        SetItemSlot();
    }
    public virtual void SetGachaItemSlot(int ItemID, int ItemCount) 
    { 
        n_ItemID = ItemID;
        SetItemSlot();
    }
}
