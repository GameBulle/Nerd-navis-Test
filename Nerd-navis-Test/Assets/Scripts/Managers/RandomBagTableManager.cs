using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBagTableManager : MonoBehaviour
{
    static RandomBagTableManager instance;
    public static RandomBagTableManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<RandomBagTableManager>();
                if(!instance)
                    instance = new GameObject("RandomBagTableManager").AddComponent<RandomBagTableManager>();
            }
            return instance;
        }
    }

    Dictionary<int, List<int>> d_RandomBagDictionary;

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);

        d_RandomBagDictionary = new();
        //Initialize();
    }

    public void Initialize()
    {
        int n_Key = -1;
        List<Dictionary<string, string>> l_Data = CSV.Read("GachaRandomBag");
        for(int i=1;i<l_Data.Count;i++)
        {
            int n_DropID = int.Parse(l_Data[i]["n_DropID"]);
            if(n_Key != n_DropID)
            {
                n_Key = n_DropID;
                d_RandomBagDictionary[n_Key] = new();
            }
            d_RandomBagDictionary[n_Key].Add(int.Parse(l_Data[i]["n_GachaRewardID"]));
        }
    }

    public List<int> GetTableItemList(int RandomBagID) { return d_RandomBagDictionary[RandomBagID]; }
}
