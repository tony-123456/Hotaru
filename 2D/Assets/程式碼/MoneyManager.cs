using UnityEngine;
using UnityEngine.UI;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)
using System;

public class MoneyManager : MonoBehaviour
{

    public static MoneyManager instance; //對戰管理實體物件
    [Header("金錢數量")]
    public Text textmoney;

    [Header("錢包等級")]
    public Text textmoneyLV;

    [Header("錢包花費")]
    public Text textwalletcost;

    [Header("錢包按鈕")]
    public Button btnwallet;

    [Header("派遣區")]
    public Transform trambattle;

    public int maxmoney = 100;
    public bool countmoney = false;
    public int moneyLV = 1;
    public int money = 0;
    public int walletcost = 50;
    public int killmoney;
    public AudioSource aud;
    public AudioClip GetMoneySound;
    public bool ifGomoney = false;
    public float moneysecond;

    private void Update()
    {
        if (dialogue.instance.gogame == true && ifGomoney == false)
        {
            ifGomoney = true;
            GoMoney();
        }
        if (money >= maxmoney)
        {

            textmoney.text = maxmoney + "/" + maxmoney; //更新卡牌數量
            money = maxmoney;
            if (money == maxmoney)
            {
                countmoney = false;
            }
            RoleCost();
        }
        if (LVsave.isMinerSpawn == true)
        {
            moneysecond = 0.05f;
        }
        else
        {
            moneysecond = 0.1f;
        }
    }

    public void Awake()
    {       
        btnwallet.onClick.AddListener(UPWallet); //添加監聽(開始遊戲)
        instance = this;
    }

    public void Start()
    {
        maxmoney = 100;
        moneyLV = 1;
        walletcost = 50;
        textmoney.text = money + "/" + maxmoney; //更新卡牌數量
        textmoneyLV.text = "LV" + moneyLV;
        textwalletcost.text = walletcost + "元";
    }

    public void GoMoney()
    {
        StartCoroutine(moneycount());
    }

    public void UPWallet()
    {
        moneyLV = moneyLV + 1;
        if (moneyLV == 2)
        {
            money = money - walletcost;
            if (countmoney == false)
            {
                StartCoroutine(moneycount());
            }
            maxmoney = 200;
            walletcost = 100;
            textmoneyLV.text = "LV" + moneyLV;
            textwalletcost.text = walletcost + "元";
        }
        if (moneyLV == 3)
        {
            money = money - walletcost;
            if (countmoney == false)
            {
                StartCoroutine(moneycount());
            }
            maxmoney = 300;
            walletcost = 200;
            textmoneyLV.text = "LV" + moneyLV;
            textwalletcost.text = walletcost + "元";
        }
        if (moneyLV == 4)
        {
            money = money - walletcost;
            if (countmoney == false)
            {
                StartCoroutine(moneycount());
            }
            maxmoney = 400;
            walletcost = 300;
            textmoneyLV.text = "LV" + moneyLV;
            textwalletcost.text = walletcost + "元";
        }
        if (moneyLV == 5)
        {
            money = money - walletcost;
            if (countmoney == false)
            {
                StartCoroutine(moneycount());
            }
            maxmoney = 500;
            textmoneyLV.text = "MAX LV";
            textwalletcost.text = "打烊";
            btnwallet.interactable = false;
        }
    }

    public IEnumerator moneycount()
    {
        if (countmoney == false)
        {
            while (money < maxmoney)
            {
                countmoney = true;
                money = money + 1;
                textmoney.text = money + "/" + maxmoney; //更新卡牌數量
                if (money < walletcost)
                {
                    btnwallet.interactable = false;
                }
                if (moneyLV < 5 && money >= walletcost)
                {
                    btnwallet.interactable = true;
                }
                RoleCost();
                yield return new WaitForSeconds(moneysecond);
            }
        }   
    }

    public void RoleCost()
    {
        if (BattleManager.instance.FinishBattle == true) return;
        for (int i = 0; i < LVsave.RolesCount; i++)
        {          
            if (money < BattleManager.instance.RoleCost[i])
            {
                BattleManager.instance.BattleRoleTransform[i].Find("遮色片").Find("圖片").GetComponent<Image>().color = Color.gray; // 尋找圖片子物件.顏色 = 顏色.灰色;
                BattleManager.instance.BattleRolebutton[i].GetComponent<Button>().interactable = false;               
            }
            if (BattleManager.instance.BattleRolebutton[i].GetComponent<BattleCard>().canProduce == true)
            {
                if (money >= BattleManager.instance.RoleCost[i])
                {
                    BattleManager.instance.BattleRoleTransform[i].Find("遮色片").Find("圖片").GetComponent<Image>().color = Color.white; // 尋找圖片子物件.顏色 = 顏色.白色;
                    BattleManager.instance.BattleRolebutton[i].GetComponent<Button>().interactable = true;                 
                }
            }
        }
     }

    public void MinusRoleCost(int cost)
    {
        money = money - cost;
        if (countmoney == false)
        {
            StartCoroutine(moneycount());
        }
    }

    public void killmonster()
    {
        money = money + killmoney;
        aud.PlayOneShot(GetMoneySound);
    }
}
