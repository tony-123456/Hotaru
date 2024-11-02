using UnityEngine;
using System.Collections.Generic; // 系統.集合.一般
using UnityEngine.UI;
using UnityEngine.SceneManagement;//引用場景管理API
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class DeckManager : MonoBehaviour
{
    [Header("商店內的角色資訊")]
    public List<CardData> deck = new List<CardData>(); //牌組清單

    [Header("已選角色資訊")]
    public GameObject DeckObject;

    [Header("已選的角色成員")]
    public Transform contentDeck;

    [Header("開始遊戲按鈕")]
    public Button btnStart;

    [Header("遊戲載入畫面")]
    public GameObject gameView;

    public static DeckManager instance;
    public GameObject monsterexplain;
    public GameObject gameexplain;
    public bool exitdeck;
    public Image loading; 
    public float imageCD = 0;

    /// <summary>
    /// 牌組管理器實體物件
    /// </summary>

    //protected 保護:允許子類別使用成員
    //virtual 虛擬:允許子類別用 override 覆寫
    protected virtual void Awake()
    {
        instance = this;
        btnStart.interactable = false; //取消開始遊戲按鈕 互動
        btnStart.onClick.AddListener(StartBattle); //添加監聽(開始遊戲)
        LVsave.exitdeck = false;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Choose4Card();
    }

    protected virtual void Choose4Card()
    {
        while (deck.Count < 4)
        {
            int r = Random.Range(1, GetCard.instance.cards.Length + 1);

            CardData card = GetCard.instance.cards[r - 1]; //取得卡牌資訊

            //尋找要新增卡牌在清單內的資料
            //=>黏巴達 (Lambda c# 7)
            //相同卡牌 = 牌組.尋找全部(卡牌 => 卡牌.等於(目前點選的卡牌資訊))
            List<CardData> sameCard = deck.FindAll(c => c.Equals(card));
            if (sameCard.Count < 1)  //如果相同卡牌<1張才能新增
            {
                //AddCard(r);
            }
        }
    }


    /// <summary>
    /// 添加卡牌至牌組內
    /// </summary>
    /// <param name="index"></param>
    public void AddCard(int index)
    {
        if (deck.Count < 4) //判斷牌庫 < 4張
        {
            CardData card = GetCard.instance.cards[index - 1]; //取得卡牌資訊

            //尋找要新增卡牌在清單內的資料
            //=>黏巴達 (Lambda c# 7)
            //相同卡牌 = 牌組.尋找全部(卡牌 => 卡牌.等於(目前點選的卡牌資訊))
            List<CardData> sameCard = deck.FindAll(c => c.Equals(card));

            if (sameCard.Count < 1)  //如果相同卡牌<1張才能新增
            {
                deck.Add(GetCard.instance.cards[index - 1]); //牌組.增加(取得卡牌.實體物件.卡牌資料[編號])
                Transform temp;
                temp = Instantiate(DeckObject, contentDeck).transform; //生成 牌組卡牌資訊物件 到 牌組內容
                temp.gameObject.AddComponent<DeckObject>().index = card.index; //添加牌組物件腳本，讓按鈕有功能
                temp.name = "已選角色資訊:" + card.name;
                temp.Find("遮色片").Find("圖片").GetComponent<Image>().sprite = Resources.Load<Sprite>(card.file); // 尋找圖片子物件.圖片 = 來源.載入<圖片>(檔案名稱)        
            }
        }
        if (deck.Count > 1) btnStart.interactable = true; //如果卡牌 = 4張 開啟開始遊戲按鈕 互動
    }

    /// <summary>
    /// 刪除排組內的卡牌
    /// </summary>
    /// <param name="index"></param>
    public void DeleteCard(int index)
    {
        CardData card = GetCard.instance.cards[index - 1]; //選取的卡牌
        List<CardData> sameCard = deck.FindAll(c => c.Equals(card));
        deck.Remove(card); //牌組刪除
        Transform temp = GameObject.Find("已選角色資訊:" + card.name).transform; //牌組物件
        Destroy(temp.gameObject);   
        if (deck.Count < 1) btnStart.interactable = false; //取消開始遊戲按鈕 互動
    }

    /// <summary>
    /// 開始遊戲
    /// </summary>
    private void StartBattle()
    {
        gameView.SetActive(true); //顯示遊戲載入畫面
        StartCoroutine(Loading());         //啟動協程
    }
    
    private IEnumerator Loading()
    {     
        //SceneManager.LoadScene("關卡1");  //載入場景
        AsyncOperation ao = SceneManager.LoadSceneAsync("關卡"+ LVsave.lastLV);

        ao.allowSceneActivation = false;     //關閉自動載入場景
        

        while (imageCD < 1)        //迴圈 while(布林值) "當布林值為 true 時執行敘述"
        {
            imageCD = imageCD + 0.01f;
            loading.fillAmount = imageCD / 0.9f;                            //更新載入吧條
                                                              //等待
            if (imageCD >= 0.9f)    //判斷式 if(布林值) "當布林值為true時執行一次"  
            {
                gameView.SetActive(false); //關閉遊戲載入畫面
                ao.allowSceneActivation = true;    //允許自動載入場景            
            }
            yield return new WaitForSeconds(0.01f);
        }       
    }
    public void MonsterExplain()
    {
        monsterexplain.SetActive(true);
    }
    public void CloseMonsterExplain()
    {
        monsterexplain.SetActive(false);
    }
    public void GameExplain()
    {
        gameexplain.SetActive(true);
    }
    public void CloseGameExplain()
    {
        gameexplain.SetActive(false);
    }
}

  


