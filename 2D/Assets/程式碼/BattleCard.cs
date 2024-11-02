using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEngine.PlayerLoop;

public class BattleCard : MonoBehaviour
{
    /// <summary>
    /// 卡片圖鑑的編號
    /// </summary>
    public int index;
    public float ProduceCD;
    public Transform temp;
    public float imageCD;
    public int cost;
    public bool canProduce;


    public static BattleCard instance; //對戰管理實體物件
    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        canProduce = true;

        GetComponent<Button>().onClick.AddListener(ChooseBattleCard); // 取得按鈕.點擊.添加監聽器(方法)
        GetComponent<Button>().interactable = false;
        temp.Find("遮色片").Find("圖片").GetComponent<Image>().color = Color.gray; // 尋找圖片子物件.顏色 = 顏色.灰色;
    }

    /// <summary>
    /// 選擇圖鑑內的卡片
    /// </summary>
    private void ChooseBattleCard()
    {
        print("選取的圖鑑編號為 :" + index);
        
        StartCoroutine(SpawnCD(GetCard.instance));
        BattleManager.instance.RoleSpawm(index, GetCard.instance);
        MoneyManager.instance.MinusRoleCost(cost);
    }

    private IEnumerator SpawnCD(GetCard cards)
    {
       
        CardData card = cards.cards[index - 1]; //卡片資料
        cost = card.cost;
        
        ProduceCD = card.ProduceCD;
        GetComponent<Button>().interactable = false;
        temp.Find("遮色片").Find("圖片").GetComponent<Image>().color = Color.gray; // 尋找圖片子物件.顏色 = 顏色.灰色;
        StartCoroutine(SpawnCDImage(GetCard.instance));
        yield return new WaitForSeconds(ProduceCD);
        

    }
    private IEnumerator SpawnCDImage(GetCard cards)
    {
        imageCD = 0;
        while (imageCD < ProduceCD)        //迴圈 while(布林值) "當布林值為 true 時執行敘述"
        {
            canProduce = false;
            temp.transform.Find("載入進度條").gameObject.SetActive(true); //顯示載入畫面 
            imageCD = imageCD + 0.1f;
            temp.Find("載入進度條").Find("載入進度條進度").GetComponent<Image>().fillAmount = imageCD / ProduceCD; // 尋找圖片子物件.顏色 = 顏色.灰色;
            
            if (imageCD >= ProduceCD)
            {
                temp.Find("載入進度條").Find("載入進度條進度").GetComponent<Image>().fillAmount = 0; // 尋找圖片子物件.顏色 = 顏色.灰色;
                temp.transform.Find("載入進度條").gameObject.SetActive(false); //關閉載入畫面
                canProduce = true;
              
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
   
}
