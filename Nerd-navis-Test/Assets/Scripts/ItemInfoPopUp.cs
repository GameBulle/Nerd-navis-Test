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

    ItemSlot Slot;

    private void Awake()
    {
        Slot = Instantiate(Resources.Load<ItemSlot>("Prefab/Slot"), SlotTransform);
    }

    public void SetItemInfoSlot(int ItemID)
    {
        PopUpObject.gameObject.SetActive(true);
        Slot.SetItemSlot(ItemID);
        Item item = ItemManager.Instance.GetItem(ItemID);
        ValueText.text = GameManager.Instance.MakeValueUnit(item.Value);

        switch(item.Type)
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
