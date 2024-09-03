using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] ItemManager.ItemOptionType ItemType;   // 장비탭의 타입(무기, 방어구, 방패)
    [SerializeField] Transform SlotParentTransform; // Slot의 부모 Transform

    InventoryItemSlot InventorySlotPrefab;
    List<InventoryItemSlot> l_ItemSlotList;
    SortedSet<int> h_ItemHashSet;   // 중복을 허용하지 않고 소유한 ItemID를 오름차순으로 정렬

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
        if(h_ItemHashSet.Count != l_ItemSlotList.Count) // 소유한 아이템의 종류의 수 != 아이템 슬롯의 수
        {
            InventoryItemSlot Slot = Instantiate(InventorySlotPrefab, SlotParentTransform);
            l_ItemSlotList.Add(Slot);
        }
    }

    // InventoryItemSlot 최신화
    public void UpdateInventoryUI()
    {
        int n_Index = -1;
        float n_Value = 0f; // 최신화된 소유한 아이템들의 능력치 합
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