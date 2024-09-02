using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image OutLine;
    [SerializeField] Image ItemLevelImage;
    [SerializeField] Image ItemIcon;
    [SerializeField] Slider UpgradeSlider;
    [SerializeField] TextMeshProUGUI ItemCountText;
    [SerializeField] TextMeshProUGUI ItemLevelText;

    public void SetItemSlot(int ItemID)
    {
        Item item = ItemManager.Instance.GetItem(ItemID);
        if (item == null)
        {
            Debug.LogError("Item is Null");
            return;
        }

        Color color = Color.white;
        switch(item.Grade)
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

        ItemLevelImage.color = color;
        ItemIcon.sprite = item.Icon;
        UpgradeSlider.maxValue = item.UpgradeRequire;
        UpgradeSlider.value = item.Count;
        ItemCountText.text = MakeItemCountStr(item.Count,item.UpgradeRequire);
        ItemLevelText.text = MakeItemLevelStr(item.Level);
    }

    string MakeItemLevelStr(int ItemLevel)
    {
        StringBuilder Sb = new();
        Sb.Append("Lv.");
        Sb.Append(ItemLevel);
        return Sb.ToString();
    }

    string MakeItemCountStr(int Count, int Require)
    {
        StringBuilder Sb = new();
        Sb.Append(Count);
        Sb.Append(" / ");
        Sb.Append(Require);
        return Sb.ToString();
    }
}
