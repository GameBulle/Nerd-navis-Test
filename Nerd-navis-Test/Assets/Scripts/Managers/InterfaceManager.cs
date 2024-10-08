using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    static InterfaceManager instance;
    public static InterfaceManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<InterfaceManager>();
                if (!instance)
                    instance = new GameObject("InterfaceManager").AddComponent<InterfaceManager>();
            }
            return instance;
        }
    }

    [Header("Text")]
    [SerializeField] TextMeshProUGUI DamageText;
    [SerializeField] TextMeshProUGUI DefenceText;
    [SerializeField] TextMeshProUGUI HpText;
    [SerializeField] TextMeshProUGUI MoneyText;
    [SerializeField] TextMeshProUGUI PowerText;

    [Header("Variable")]
    [SerializeField] ResourceMenu resourceMenu;
    [SerializeField] GachaMenu gachaMenu;

    float f_MakeMoneyTimer = 0f;    // 한번 자원 제작하는데 남은 시간(Max = f_RefillMoneyInterval)
    float f_MakedMoney = 0f;    // 생산된 자원(획득 하기전 자원)

    float f_RefillMoneyInterval = 0f;   // 자원 제작 시간
    float f_RefillMoneyCount = 0f;  // 한번의 자원 제작으로 얻는 자원의 양

    public float RefillMoneyInterval => f_RefillMoneyInterval;
    public float MakeMoneyTimer => f_MakeMoneyTimer;
    public float MakedMoney { get { return f_MakedMoney; } set { f_MakedMoney = value; } }

    private void Awake()
    {
        if (this != Instance)
            Destroy(gameObject);
    }
    private void Update()
    {
        f_MakeMoneyTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if(f_MakeMoneyTimer >= f_RefillMoneyInterval)
        {
            f_MakeMoneyTimer = 0f;
            f_MakedMoney += f_RefillMoneyCount;
            resourceMenu.UpdateMakedMoney();
        }
    }

    public void Initialize()
    {
        float f_RequireGachaPrice = 0f;
        float f_MaxMoneyLimit = 0f;
        float f_CanGetRefillMoneyCount = 0f;

        List<Dictionary<string, string>> l_Data = CSV.Read("GlobalValue");
        for (int i = 0; i < l_Data.Count; i++)
        {
            string s_Value = "";
            foreach (KeyValuePair<string, string> d in l_Data[i])
            {
                if (d.Key.Equals("s_GlobalValueID"))
                {
                    s_Value = d.Value;
                    continue;
                }

                switch (s_Value)
                {
                    case "f_RefillMoneyInterval":
                        f_RefillMoneyInterval = float.Parse(d.Value);
                        break;
                    case "f_RefillMoneyCount":
                        f_RefillMoneyCount = float.Parse(d.Value);
                        break;
                    case "f_DefaultMoneyCount":
                        GameManager.Instance.Money += int.Parse(d.Value);
                        break;
                    case "f_RequireGachaPrice":
                        f_RequireGachaPrice = float.Parse(d.Value);
                        break;
                    case "f_MaxMoneyLimit":
                        f_MaxMoneyLimit = float.Parse(d.Value);
                        break;
                    case "f_CanGetRefillMoneyCount":
                        f_CanGetRefillMoneyCount = float.Parse(d.Value);
                        break;
                }
            }
        }

        UpdateStatus();
        resourceMenu.Initialize(f_MaxMoneyLimit, f_CanGetRefillMoneyCount);
        gachaMenu.Initialize(f_RequireGachaPrice);
    }

    public void UpdateStatus()  // 모든 스테이터스(공격력, 방어력, 체력, 전투력)를 Update하는 함수
    {
        DamageText.text = GameManager.Instance.MakeValueUnit(GameManager.Instance.Damage);
        DefenceText.text = GameManager.Instance.MakeValueUnit(GameManager.Instance.Defence);
        HpText.text = GameManager.Instance.MakeValueUnit(GameManager.Instance.Hp);
        PowerText.text = GameManager.Instance.MakeValueUnit(
            GameManager.Instance.Damage + GameManager.Instance.Defence + GameManager.Instance.Hp);
    }

    public void UpdateMoney() { MoneyText.text = GameManager.Instance.MakeValueUnit(GameManager.Instance.Money); }
}
