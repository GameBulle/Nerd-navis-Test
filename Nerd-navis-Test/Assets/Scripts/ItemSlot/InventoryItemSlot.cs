using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlot : ItemSlot
{
    [SerializeField] Slider UpgradeSlider;  // ( 현재 수량 /  업그레이드 요구 수량) 표현할 슬라이더
    [SerializeField] TextMeshProUGUI ItemLevelText;

    public override void SetInventoryItemSlot(int ItemID)
    {
        base.SetInventoryItemSlot(ItemID);

        Item item = ItemManager.Instance.GetItem(ItemID);
        UpgradeSlider.maxValue = item.UpgradeRequire;
        UpgradeSlider.value = item.Count;
        ItemCountText.text = MakeItemCountStr(item.Count, item.UpgradeRequire);
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

    public void OnClickItemSlot()   // ItemIcon을 클릭했을 때 아이템 정보 팝업창이 나오게 하는 함수
    {
        ItemInfoPopUp PopUp = FindObjectOfType<ItemInfoPopUp>();
        PopUp.SetItemInfoSlot(n_ItemID);
    }
}
