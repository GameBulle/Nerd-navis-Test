using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachMenu : MonoBehaviour
{
    float f_RequireGachaPrice;
    int GachaID;

    public void Initialize(float gachaPrice)
    {
        f_RequireGachaPrice = gachaPrice;
    }

    public void OnClickGachaButton(int GachaID, int GachTime)
    {
        if (f_RequireGachaPrice * GachTime < GameManager.Instance.Money)
            return;

        GameManager.Instance.Money -= (int)f_RequireGachaPrice * GachTime;
        Gacha gacha = GachaManager.Instance.GetGacha(GachaID);


    }

    public void test(int a,int b)
    {

    }
}
