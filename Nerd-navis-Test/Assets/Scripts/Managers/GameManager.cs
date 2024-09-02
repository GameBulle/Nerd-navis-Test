using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (!instance)
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return instance;
        }
    }

    float f_Damage, f_Defence, f_Hp;
    int n_Money;

    public float Damage 
    { 
        get { return f_Damage; } 
        set 
        { 
            f_Damage = value;
            InterfaceManager.Instance.UpdateStatus();
        } 
    }
    public float Defence 
    { 
        get { return f_Defence; } 
        set 
        { 
            f_Defence = value;
            InterfaceManager.Instance.UpdateStatus();
        } 
    }
    public float Hp 
    { 
        get { return f_Hp; } 
        set 
        { 
            f_Defence = value;
            InterfaceManager.Instance.UpdateStatus();
        } 
    }
    public int Money 
    { 
        get { return n_Money; } 
        set 
        {
            n_Money = value;
            InterfaceManager.Instance.UpdateMoney();
        } 
    }

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);
    }

    private void Start()
    {
        Initialize();
        InterfaceManager.Instance.Initialize();
        ItemManager.Instance.Initialize();
        RandomBagTableManager.Instance.Initialize();
        GachaManager.Instance.Initialize();
    }

    void Initialize()
    {
        f_Damage = 0f;
        f_Defence = 0f;
        f_Hp = 0f;
    }

    public string MakeValueUnit(float value)
    {
        StringBuilder Sb = new();
        int n_Share = 0;

        while(value >= 1000)
        {
            value = value / 1000;
            n_Share++;
        }

        value = Mathf.Floor(value * 100f) / 100f;
        Sb.Append(value);

        switch(n_Share)
        {
            case 1:
                Sb.Append("k");
                break;
            case 2:
                Sb.Append("m");
                break;
            case 3:
                Sb.Append("g");
                break;
        }

        return Sb.ToString();
    }
}
