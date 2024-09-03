using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaMenu : MonoBehaviour
{
    [SerializeField] GachaResult GachaResult;
    [SerializeField] Inventory[] inventories;   // Weapon, Defence, Shield

    float f_RequireGachaPrice;  // 뽑기 1회에 필요한 자원량
    int n_GachaID;  // 현재 장비탭에 따른 GachaID

    public void Initialize(float gachaPrice)
    {
        f_RequireGachaPrice = gachaPrice;
        n_GachaID = 10000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (i == 0)     // 뽑기 메뉴 활성화 시, 무기탭 UI가 먼저 포커싱 되도록 함
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
            inventories[i].Initialize();
        }
        GachaResult.Initialize();
    }

    public void OnClickGachaButton(int GachaTime)
    {
        if (f_RequireGachaPrice * GachaTime > GameManager.Instance.Money)    // 시행할려는 뽑기 횟수만큼 자원이 있는지 확인
            return;
            
        GameManager.Instance.Money -= (int)f_RequireGachaPrice * GachaTime;
        Gacha gacha = GachaManager.Instance.GetGacha(n_GachaID);    // 현재 활성화된 장비 UI(Inventory)에 맞는 뽑기 정보를 가져옴

        Dictionary<int, int> d_PickedItemDic = gacha.PickRandomItem(GachaTime); // Key : 뽑기로 얻은 아이템의 ID, Value : 뽑기로 얻은 아이템의 수량
        foreach (KeyValuePair<int, int> d in d_PickedItemDic)
        {
            ItemManager.Instance.CheckIncreaseItem(d.Key, d.Value);
            for(int i=0;i<inventories.Length;i++)
            {
                if (inventories[i].Type == ItemManager.Instance.GetItem(d.Key).Type)    // 뽑은 Item의 옵션 타입에 맞는 Inventory로 얻은 Item 전달
                {
                    inventories[i].SetItem(d.Key);
                    break;
                }
            }
        }

        GachaResult.GachaProduction(d_PickedItemDic);   // 뽑기 연출 실행

        for (int i = 0; i < inventories.Length; i++)
            inventories[i].UpdateInventoryUI();     // 장비 UI 최신화
    }

    public void OnClickWeaponTab()  // 무기탭 클릭 했을 때
    {
        n_GachaID = 10000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i].Type == ItemManager.ItemOptionType.AttackIncrease)
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
        }
    }

    public void OnClickArmorTab()   // 방어구탭 클릭 했을 때
    {
        n_GachaID = 20000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i].Type == ItemManager.ItemOptionType.DefenseIncrease)
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
        }
    }

    public void OnClickShieldTab()  // 방패탭 클릭 했을 때
    {
        n_GachaID = 30000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (inventories[i].Type == ItemManager.ItemOptionType.HpIncrease)
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
        }
    }
}
