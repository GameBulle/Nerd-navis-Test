using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    static GachaManager instance;
    public static GachaManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GachaManager>();
                if (!instance)
                    instance = new GameObject("GachaManager").AddComponent<GachaManager>();
            }
            return instance;
        }
    }

    List<Gacha> l_GachaList;    // 모든 Gacha(뽑기)를 관리하는 List

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);

        l_GachaList = new();
    }

    public void Initialize()
    {
        List<Dictionary<string, string>> l_Data = CSV.Read("GachaGradeInfo");
        for (int i = 1; i < l_Data.Count; i++)
        {
            Gacha g_Gacha = gameObject.AddComponent<Gacha>();
            g_Gacha.Initialize(int.Parse(l_Data[i]["n_GachaID"]),
                float.Parse(l_Data[i]["n_NormalGachaRate"]),
                float.Parse(l_Data[i]["n_RareGachaRate"]),
                float.Parse(l_Data[i]["n_EpicGachaRate"]),
                int.Parse(l_Data[i]["n_GachaRandombagID"]));
            l_GachaList.Add(g_Gacha);
        }
    }

    public Gacha GetGacha(int GachaID)
    {
        int n_Index = l_GachaList.FindIndex(x => x.GachaID == GachaID);
        return (n_Index != -1 ? l_GachaList[n_Index] : null);
    }
}
