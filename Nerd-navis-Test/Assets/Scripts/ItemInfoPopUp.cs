using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPopUp : MonoBehaviour
{
    [SerializeField] Image TypeIcon;
    [SerializeField] TextMeshProUGUI ValueText;
    [SerializeField] Transform SlotTransform;
    [SerializeField] GameObject PopUpObject;

    InventoryItemSlot InfoPopUpSlotPrefab;

    private void Awake()
    {
        InfoPopUpSlotPrefab = Instantiate(Resources.Load<InventoryItemSlot>("Prefab/InventoryItemSlot"), SlotTransform);
    }

    public void SetItemInfoSlot(int ItemID)
    {
        PopUpObject.gameObject.SetActive(true); // 팝업 활성화
        InfoPopUpSlotPrefab.SetInventoryItemSlot(ItemID);   // Slot에 아이템 정보 셋팅
        Item item = ItemManager.Instance.GetItem(ItemID);
        ValueText.text = GameManager.Instance.MakeValueUnit(item.Value);    // 아이템의 능력치

        switch(item.Type)   // 아이템의 등급에 따른 Icon 가져오기
        {
            case ItemManager.ItemOptionType.AttackIncrease:
                TypeIcon.sprite = Resources.Load<Sprite>("Image/Ui/Attack");
                break;
            case ItemManager.ItemOptionType.DefenseIncrease:
                TypeIcon.sprite = Resources.Load<Sprite>("Image/Ui/Defence");
                break;
            case ItemManager.ItemOptionType.HpIncrease:
                TypeIcon.sprite = Resources.Load<Sprite>("Image/Ui/Health");
                break;
        }
    }
}
