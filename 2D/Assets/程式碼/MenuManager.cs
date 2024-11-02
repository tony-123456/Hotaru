
using UnityEngine;
using UnityEngine.UI;//引用介面API
using UnityEngine.SceneManagement;//引用場景管理API
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)


public class MenuManager : MonoBehaviour
{
    public GameObject explain;
    public GameObject blackboard;
    public GameObject NOTE;

    /// <summary>
    /// 開始載入背景場景
    /// </summary>
    public IEnumerator StartLoading()
    {
        blackboard.SetActive(true);
        yield return new WaitForSeconds(3);
        AsyncOperation ao = SceneManager.LoadSceneAsync("選擇角色選單");
        ao.allowSceneActivation = true;     //關閉自動載入場景
    }

    public void gogame()
    {
        StartCoroutine(StartLoading());
    }

    public void Explain()
    {
        explain.SetActive(true);
    }
    public void Note()
    {
        NOTE.SetActive(true);
    }
    public void CloseExplain()
    {
        explain.SetActive(false);
    }
    public void CloseNote()
    {
        NOTE.SetActive(false);
    }
    /// <summary>
    /// 協程方法:載入
    /// </summary>
    /// <returns></returns>

    private void Start()
    {
        //Screen.SetResolution(1280, 720, false); //螢幕.設定解析度(寬,高,是否全螢幕) - EXE或網頁版(Build Settings)
    }
}
