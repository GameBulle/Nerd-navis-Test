using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaMenu : MonoBehaviour
{
    [SerializeField] GachaResult GachaResult;
    [SerializeField] Inventory[] inventories;   // Weapon, Defence, Shield

    float f_RequireGachaPrice;  // �̱� 1ȸ�� �ʿ��� �ڿ���
    int n_GachaID;  // ���� ����ǿ� ���� GachaID

    public void Initialize(float gachaPrice)
    {
        f_RequireGachaPrice = gachaPrice;
        n_GachaID = 10000;
        for (int i = 0; i < inventories.Length; i++)
        {
            if (i == 0)     // �̱� �޴� Ȱ��ȭ ��, ������ UI�� ���� ��Ŀ�� �ǵ��� ��
                inventories[i].gameObject.SetActive(true);
            else
                inventories[i].gameObject.SetActive(false);
            inventories[i].Initialize();
        }
        GachaResult.Initialize();
    }

    public void OnClickGachaButton(int GachaTime)
    {
        if (f_RequireGachaPrice * GachaTime > GameManager.Instance.Money)    // �����ҷ��� �̱� Ƚ����ŭ �ڿ��� �ִ��� Ȯ��
            return;
            
        GameManager.Instance.Money -= (int)f_RequireGachaPrice * GachaTime;
        Gacha gacha = GachaManager.Instance.GetGacha(n_GachaID);    // ���� Ȱ��ȭ�� ��� UI(Inventory)�� �´� �̱� ������ ������

        Dictionary<int, int> d_PickedItemDic = gacha.PickRandomItem(GachaTime); // Key : �̱�� ���� �������� ID, Value : �̱�� ���� �������� ����
        foreach (KeyValuePair<int, int> d in d_PickedItemDic)
        {
            ItemManager.Instance.CheckIncreaseItem(d.Key, d.Value);
            for(int i=0;i<inventories.Length;i++)
            {
                if (inventories[i].Type == ItemManager.Instance.GetItem(d.Key).Type)    // ���� Item�� �ɼ� Ÿ�Կ� �´� Inventory�� ���� Item ����
                {
                    inventories[i].SetItem(d.Key);
                    break;
                }
            }
        }

        GachaResult.GachaProduction(d_PickedItemDic);   // �̱� ���� ����

        for (int i = 0; i < inventories.Length; i++)
            inventories[i].UpdateInventoryUI();     // ��� UI �ֽ�ȭ
    }

    public void OnClickWeaponTab()  // ������ Ŭ�� ���� ��
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

    public void OnClickArmorTab()   // ���� Ŭ�� ���� ��
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

    public void OnClickShieldTab()  // ������ Ŭ�� ���� ��
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
