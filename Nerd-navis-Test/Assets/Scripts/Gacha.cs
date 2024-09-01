using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gacha : MonoBehaviour
{
    int n_GachaID;
    float[] farr_GachaRates = new float[System.Enum.GetValues(typeof(ItemManager.ItemGrade)).Length];
    int n_GachaRandomBagID;

    public int GachaID => n_GachaID;
    public int GachaRandomBagID => n_GachaRandomBagID;
    public float GachaRate(ItemManager.ItemGrade grade) { return farr_GachaRates[(int)grade]; }

    public void Initialize(int ID, float NormalRate, float RareRate, float EpicRate, int RandomBagID)
    {
        n_GachaID = ID;
        farr_GachaRates[(int)ItemManager.ItemGrade.Normal] = NormalRate;
        farr_GachaRates[(int)ItemManager.ItemGrade.Rare] = RareRate;
        farr_GachaRates[(int)ItemManager.ItemGrade.Epic] = EpicRate;
        n_GachaRandomBagID = RandomBagID;
    }
}
