using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)
using System;
public class BattleManager : MonoBehaviour
{
    public static BattleManager instance; //對戰管理實體物件
    public SpawnAlliance data;
    public GameObject pass;
    public GameObject lose;
    public bool FinishBattle;

    public bool passLv;
    public bool loseLv;

    [Header("派遣區")]
    public Transform trambattle;

    [Header("遊戲載入畫面")]
    public GameObject gameView;

    [Header("角色雇用照片")]
    public List<Transform> BattleRoleTransform = new List<Transform>();

    [Header("角色雇用按鍵")]
    public List<GameObject> BattleRolebutton = new List<GameObject>();

    [Header("角色雇用按鍵")]
    public List<int> RoleCost = new List<int>();

    public int LV;

    public Image loading;

    public float imageCD = 0.9f;

    public float imageCD2 = 0f;

    private Vector3 SpawnPos;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        //Screen.SetResolution(1280, 720, true);
        LVsave.exitdeck = true;
        LVsave.icedead = false;
        LVsave.magiciandead = false;
        LVsave.isMinerSpawn = false;
        LVsave.boosspawn = false;
        LVsave.finalboss = false;
        CreateCard();
        StartCoroutine(Startloadingimage());
    }
    
    public IEnumerator Startloadingimage()
    {
        while (imageCD > 0)        //迴圈 while(布林值) "當布林值為 true 時執行敘述"
        {
            imageCD = imageCD - 0.01f;
            loading.fillAmount = imageCD / 0.9f;                            //更新載入吧條
                                                                            //等待
            if (imageCD <= 0)    //判斷式 if(布林值) "當布林值為true時執行一次"  
            {
                gameView.SetActive(false); //關閉遊戲載入畫面
            }
            yield return new WaitForSeconds(0.01f);
        }
        // 初始化對話內容
        if (LVsave.lastLV == 1)
        {
           dialogue.instance.InitDialogue(new string[] { "有人被綁起來，看起來快死了，要快點去救他", "等等那是我朋友國國騎士，可惡的網美璇璇軍團把他綁走了，我跟你一起去吧!" }, new int[] { 0, 1 });
        }
        if (LVsave.lastLV == 2)
        {
            dialogue.instance.InitDialogue(new string[] { "這森林好複雜要小心走", "快看!那邊有個人", "可惡，是網美璇璇的手下嗎...", "我們不是網美璇璇的手下，我們是要去討罰網美璇璇的!", "那就讓我加入你們吧!我是鼠翔，原本是網美璇璇的奴隸，後來逃出來了，我可以挖礦讓你們的金幣賺更多!", "太好了，發財致富就看你了!" }, new int[] { 0, 1, 3, 0, 3, 0 });
        }
        if (LVsave.lastLV == 3)
        {
            dialogue.instance.InitDialogue(new string[] { "守在城堡前面的就是史蒂芬，小心他會召喚落雷", "落雷有甚麼好怕的，看我扛過去", "是蓉阿，看來你是不打算成全我們呢，我會盡全力阻止你們傷害網美璇璇小姐的!", "水瓶渣男，你完蛋了" }, new int[] { 5, 2, 4, 5 });
        }
    }
    public IEnumerator Endloadingimage()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        ao.allowSceneActivation = false;
        gameView.SetActive(true); //關閉遊戲載入畫面
        while (imageCD < 1)        //迴圈 while(布林值) "當布林值為 true 時執行敘述"
        {
            imageCD = imageCD + 0.01f;
            loading.fillAmount = imageCD / 0.9f;                            //更新載入吧條
            print(imageCD);                                                            //等待
            if (imageCD >= 0.9f)    //判斷式 if(布林值) "當布林值為true時執行一次"  
            {
                gameView.SetActive(false); //關閉遊戲載入畫面
                ao.allowSceneActivation = true;    //允許自動載入場景
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void Lose() //失敗:我方基地血量小於0，遊戲顯示失敗畫面
    {
        dialogue.instance.InitDialogue(new string[] { "我這麼漂亮，為什麼會發生這種事..." }, new int[] {0});
        loseLv = true;
        lose.SetActive(true);
        FinishBattle = true;
    }
    public void Win() //過關:敵方基地血量小於0，遊戲顯示過關畫面
    {
        if (LVsave.lastLV == 1) dialogue.instance.InitDialogue(new string[] { "謝謝你們救了我，可惡的網美璇璇，我一定要找他報仇，我超耐揍，一定對你們有幫助的", "很好我們又多一個同伴了" }, new int[] { 2, 0 });
        else if (LVsave.lastLV == 2) dialogue.instance.InitDialogue(new string[] { "前面就是網美璇璇的豪宅", "可惡的網美璇璇竟敢讓我這麼難堪", "史蒂芬等等阿", "小姐你怎麼了，怎麼看起來這麼急躁", "我的暗戀對象史蒂芬，他跟一個叫網美璇璇的跑了，明明我們這麼要好...", "我們正要去找網美璇璇算帳你要一起嗎?", "好，我要殺了這對狗男女!我能召喚隕石，看我把它們都灰飛煙滅!" }, new int[] { 0, 2, 5, 1, 5, 3, 5 });
        else if (LVsave.lastLV == 3) dialogue.instance.InitDialogue(new string[] { "可惡，到此為止了嗎...網美璇璇小姐我來陪你了!(自殺)","渣男都去死","啊哈~終於拿到解藥，這樣大家就都能恢復正常了", "木木木", "歡樂的時光總是過得特別快", "你們看這甚麼!有一個大寶箱", "我希望打開不會是一位水瓶座男性..." }, new int[] {4,5, 0, 3, 2, 1, 5 });
        passLv = true;
        FinishBattle = true;
        pass.SetActive(true);
    }
    public void NextLv()
    {
        if (LVsave.lastLV == 1) LVsave.RolesCount = LVsave.RolesCount + 2;
        else if (LVsave.lastLV == 2) LVsave.RolesCount = LVsave.RolesCount + 1;
        LVsave.lastLV = LVsave.lastLV + 1;
        StartCoroutine(Endloadingimage());
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CreateCard()
    {
        for (int i = 0; i < LVsave.RolesCount; i++) //迴圈執行 卡牌數量
        {
            Transform temp = Instantiate(GetCard.instance.battlecardObject, trambattle).transform; //變形 = 生成(物件，父物件).變形
            CardData card = GetCard.instance.cards[i]; //卡片資料
            //尋找子物件並更新資料
            temp.Find("花費").GetComponent<Text>().text = card.cost.ToString();
            temp.Find("攻擊").GetComponent<Text>().text = card.attack.ToString();
            temp.Find("血量").GetComponent<Text>().text = card.hp.ToString();         
            temp.Find("遮色片").Find("圖片").GetComponent<Image>().sprite = Resources.Load<Sprite>(card.file); // 尋找圖片子物件.圖片 = 來源.載入<圖片>(檔案名稱)
            temp.gameObject.AddComponent<BattleCard>().index = card.index; //添加元件<戰鬥卡牌> 編號 = 卡牌.編號
            temp.gameObject.GetComponent<BattleCard>().temp = temp.transform; //添加元件<戰鬥卡牌> 圖鑑 = 物件.變形
            temp.gameObject.GetComponent<BattleCard>().cost = card.cost; //添加元件<戰鬥卡牌> 消耗 = 卡牌.消耗
            BattleRoleTransform.Add(temp.transform);
            BattleRolebutton.Add(temp.gameObject);
            RoleCost.Add(card.cost);
        }
    }
 
    public void RoleSpawm(int index,GetCard cards)
    {     
        CardData card = cards.cards[index - 1]; //卡片資料(Element從0開始，編號從1開始，故-1)
        if (data.Spawn[index - 1].Alliance.layer == LayerMask.NameToLayer("魔法師")) SpawnPos = new Vector2(-7.4f, -2.82f); //座標
        else SpawnPos = new Vector2(-7.4f, -2.3f); //座標
        Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
        GameObject temp = Instantiate(data.Spawn[index - 1].Alliance, SpawnPos, qua); //生成
        temp.gameObject.GetComponent<Alliance>().hp = card.hp; //添加元件<盟友>.血量 = 卡片.血量
        temp.gameObject.GetComponent<RoleMove>().speed = card.speed; //添加元件<盟友移動> 速度 = 卡牌.速度
        temp.gameObject.GetComponent<RoleMove>().damage = card.attack; //添加元件<盟友移動>.傷害 = 卡牌.攻擊
        temp.gameObject.GetComponent<RoleMove>().AttackCD = card.AttackCD; //添加元件<盟友移動>.攻擊速度 = 卡片.攻擊速度
        temp.gameObject.GetComponent<RoleMove>().AttackDistance = card.AtackDistance; //添加元件<盟友移動>.攻擊距離 = 卡片.攻擊距離
    }
}