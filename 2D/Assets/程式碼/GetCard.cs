using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; //引用 網路連線API
using System.Collections;
using UnityEngine.SceneManagement;//引用場景管理API

public class GetCard : MonoBehaviour
{
    public CardData[] cards;

    [Header("卡牌物件")]
    public GameObject cardObject;

    [Header("戰鬥中人物按鈕")]
    public GameObject battlecardObject;

    [Header("卡牌內容")]
    public Transform contentCard;

    private CanvasGroup loadingPanel;
    private Image loading;
    public float imageCD = 0;
    /// <summary>
    /// 取得卡牌資料的實體物件
    /// </summary>
    public static GetCard instance;

    private IEnumerator GetCardData()
    {
        loadingPanel.alpha = 1; //顯示
        loadingPanel.blocksRaycasts = true; //啟動遮擋

        // 引用(網路要求 www = 網路要求.Post("網址", ""))
        using (UnityWebRequest www = UnityWebRequest.Post("https://script.google.com/macros/s/AKfycbwozgGEuc9fWDC-b95WTfLr-TMhg8oKztq-S0bKQ0wrNz0IGrPy2IzCnyuUwN7I3H2Q/exec", ""))
        {
            //yield return www.SendWebRequest(); //等待 網路要求時間
            www.SendWebRequest(); //網路要求

            while (www.downloadProgress < 1)
            {
                yield return null;
                loading.fillAmount = www.downloadProgress;
            }

            if (www.isHttpError || www.isNetworkError)
            {
                print("連線錯誤:" + www.error);
            }
            else
            {
                cards = JsonHelper.FromJson<CardData>(www.downloadHandler.text); //將 JSON 轉為陣列並儲存在cards內
                StartCoroutine(Loading());
            }
        }
    }
    private IEnumerator Loading()
    {
        //SceneManager.LoadScene("關卡1");  //載入場景
        AsyncOperation ao = SceneManager.LoadSceneAsync("關卡1");

        ao.allowSceneActivation = false;     //關閉自動載入場景


        while (imageCD < 1)        //迴圈 while(布林值) "當布林值為 true 時執行敘述"
        {
            imageCD = imageCD + 0.01f;
            loading.fillAmount = imageCD / 0.9f;                            //更新載入吧條
                                                                            //等待
            if (imageCD >= 0.9f)    //判斷式 if(布林值) "當布林值為true時執行一次"  
            {
                ao.allowSceneActivation = true;    //允許自動載入場景            
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Awake()
    {
        instance = this;
        loadingPanel = GameObject.Find("載入畫面").GetComponent<CanvasGroup>();
        loading = GameObject.Find("進度條").GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(GetCardData());
    }
}
/// <summary>
/// 卡片資料
/// </summary>
[System.Serializable] //序列化:讓資料顯示在屬性面板上
public class CardData
{
    public int index;
    public string name;
    public string description;
    public int cost;
    public float attack;
    public float hp;
    public float AttackCD;
    public float ProduceCD;
    public float speed;
    public float AtackDistance;
    public string file;
}


public static class JsonHelper //將json轉為陣列資料
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

