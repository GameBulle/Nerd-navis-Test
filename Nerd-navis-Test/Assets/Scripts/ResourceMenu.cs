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

    float f_MaxMoneyLimit;
    float f_CanGetRefillMoneyCount;

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
        if (InterfaceManager.Instance.MakedMoney < f_CanGetRefillMoneyCount 
            || !CanGetNoticeBoard.activeSelf 
            || GameManager.Instance.Money >= f_MaxMoneyLimit)
            return;

        if (GameManager.Instance.Money + InterfaceManager.Instance.MakedMoney >= f_MaxMoneyLimit)
        {
            float n_Left = f_MaxMoneyLimit - GameManager.Instance.Money;
            GameManager.Instance.Money += (int)n_Left;
            InterfaceManager.Instance.MakedMoney -= n_Left;
        }
        else
        {
            GameManager.Instance.Money += (int)InterfaceManager.Instance.MakedMoney;
            InterfaceManager.Instance.MakedMoney = 0f;
        }

        CanGetNoticeBoard.SetActive(false);
        MakedMoneyText.color = Color.red;
        MakedMoneyText.text = GameManager.Instance.MakeValueUnit(InterfaceManager.Instance.MakedMoney);
    }
}
