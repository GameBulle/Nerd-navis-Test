using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceMenu : MonoBehaviour
{
    [SerializeField] Slider MoneyMakeSlider;
    [SerializeField] TextMeshProUGUI LeftMakeTimeText;
    [SerializeField] GameObject CanGetNoticeBoard;
    [SerializeField] TextMeshProUGUI MakedMoneyText;

    float f_MaxMoneyLimit;          // 얻을 수 있는 최대 자원량
    float f_CanGetRefillMoneyCount; // 획득 가능한 자원의 최소양

    private void FixedUpdate()
    {
        MoneyMakeSlider.value = InterfaceManager.Instance.MakeMoneyTimer;
        LeftMakeTimeText.text = string.Format("{0:F2}", (InterfaceManager.Instance.RefillMoneyInterval - MoneyMakeSlider.value));

        if (MoneyMakeSlider.value == MoneyMakeSlider.maxValue)
        {
            MoneyMakeSlider.value = 0f;
            LeftMakeTimeText.text = "0";
        }

        if (InterfaceManager.Instance.MakedMoney >= f_CanGetRefillMoneyCount && !CanGetNoticeBoard.activeSelf)
        {
            CanGetNoticeBoard.SetActive(true);
            MakedMoneyText.color = Color.black;
        }
    }

    public void Initialize(float maxMoney, float getCount)
    {
        f_MaxMoneyLimit = maxMoney;
        f_CanGetRefillMoneyCount = getCount;

        MoneyMakeSlider.value = 0f;
        MoneyMakeSlider.maxValue = InterfaceManager.Instance.RefillMoneyInterval;
        LeftMakeTimeText.text = InterfaceManager.Instance.RefillMoneyInterval.ToString();
        UpdateMakedMoney();
        MakedMoneyText.color = Color.red;
        CanGetNoticeBoard.SetActive(false);
    }

    public void UpdateMakedMoney() { MakedMoneyText.text = GameManager.Instance.MakeValueUnit(InterfaceManager.Instance.MakedMoney); }

    public void OnClickGetMoneyButton()
    {
        if (InterfaceManager.Instance.MakedMoney < f_CanGetRefillMoneyCount     // 생산된 자원량이 획득 가능한 최소 자원량보다 작은지
            || !CanGetNoticeBoard.activeSelf                                    // 획득 가능 알림판이 비활성화 됐는지
            || GameManager.Instance.Money >= f_MaxMoneyLimit)                   // 소유한 자원이 소유 가능한 자원 한계치보다 많은지
            return;

        // 소유 자원 + 생산된 자원이 소유 가능한 자원 한계치보다 많을 때
        if (GameManager.Instance.Money + InterfaceManager.Instance.MakedMoney >= f_MaxMoneyLimit)
        {
            // 소유한 자원양을 소유 가능한 자원 한계치까지 증가시키고
            // 증가시키고 남은 자원량을 현재 생산된 자원량에서 뺌
            float n_Left = f_MaxMoneyLimit - GameManager.Instance.Money;
            GameManager.Instance.Money += (int)n_Left;
            InterfaceManager.Instance.MakedMoney -= n_Left;
        }
        else
        {
            GameManager.Instance.Money += (int)InterfaceManager.Instance.MakedMoney;
            InterfaceManager.Instance.MakedMoney = 0f;
        }

        if(InterfaceManager.Instance.MakedMoney < f_CanGetRefillMoneyCount) // 획득하고 남은 자원량이 획득 가능한 자원의 최소양 보다 적은지
        {
            CanGetNoticeBoard.SetActive(false);
            MakedMoneyText.color = Color.red;
        }

        MakedMoneyText.text = GameManager.Instance.MakeValueUnit(InterfaceManager.Instance.MakedMoney);
    }
}
