using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemManager.ItemOptionType ItemType;
    [SerializeField] Transform SlotParentTransform;

    InventoryItemSlot InventorySlotPrefab;
    List<InventoryItemSlot> l_ItemSlotList;
    SortedSet<int> h_ItemHashSet;

    public ItemManager.ItemOptionType Type => ItemType;

    public void Initialize()
    {
        h_ItemHashSet = new();
        l_ItemSlotList = new();
        InventorySlotPrefab = Resources.Load<InventoryItemSlot>("Prefab/InventoryItemSlot");
    }

    public void SetItem(int ItemID) 
    {
        h_ItemHashSet.Add(ItemID); 
        if(h_ItemHashSet.Count != l_ItemSlotList.Count)
        {
            InventoryItemSlot Slot = Instantiate(InventorySlotPrefab, SlotParentTransform);
            l_ItemSlotList.Add(Slot);
        }
    }

    public void UpdateInventoryUI()
    {
        int n_Index = -1;
        float n_Value = 0f;
        foreach(int ID in h_ItemHashSet)
        {
            n_Index++;
            l_ItemSlotList[n_Index].SetInventoryItemSlot(ID);
            n_Value += ItemManager.Instance.GetItem(ID).Value;
        }
        
        switch(ItemType)
        {
            case ItemManager.ItemOptionType.AttackIncrease:
                GameManager.Instance.Damage = n_Value;
                break;
            case ItemManager.ItemOptionType.DefenseIncrease:
                GameManager.Instance.Defence = n_Value;
                break;
            case ItemManager.ItemOptionType.HpIncrease:
                GameManager.Instance.Hp = n_Value;
                break;
        }
    }
}