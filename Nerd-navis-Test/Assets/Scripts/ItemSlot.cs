using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image OutLine;
    [SerializeField] Image ItemIcon;
    [SerializeField] TextMeshProUGUI RequireUpgradeText;
    [SerializeField] TextMeshProUGUI ItemCountText;

    int n_ItemID;
    int n_RequireUpgrade;
    int n_ItemCoun;

    public void SetItemSlot(int ItemID)
    {
        n_ItemID = ItemID;
        Item item = ItemManager.Instance.GetItem(n_ItemID);
        if (item == null)
        {
            Debug.LogError("Item is Null");
            return;
        }

        ItemIcon.sprite = item.Icon;
    }
}
