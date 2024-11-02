using UnityEngine;
using UnityEngine.UI;

public class DeckObject : MonoBehaviour
{
    /// <summary>
    /// 牌組物件編號
    /// </summary>
    public int index;

    private void Start()
    {
        transform.Find("移除角色鍵").GetComponent<Button>().onClick.AddListener(DeleteCard);
    }

    /// <summary>
    /// 牌組物件內的增加卡牌按鈕功能
    /// </summary>
    public void AddCard()
    {
        DeckManager.instance.AddCard(index);
    }

    public void DeleteCard()
    {
        DeckManager.instance.DeleteCard(index);
    }

}
