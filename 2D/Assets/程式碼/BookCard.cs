using UnityEngine;
using UnityEngine.UI;

public class BookCard : MonoBehaviour
{
    /// <summary>
    /// 卡片圖鑑的編號
    /// </summary>
    public int index;
  
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChooseBookCard); // 取得按鈕.點擊.添加監聽器(方法)
    }

    /// <summary>
    /// 選擇圖鑑內的卡片
    /// </summary>
    private void ChooseBookCard()
    {
        print("選取的圖鑑編號為 :" + index);
        DeckManager.instance.AddCard(index);
       
    }
}
