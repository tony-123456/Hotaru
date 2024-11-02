using UnityEngine;
using UnityEngine.UI;

public class dialogue : MonoBehaviour
{
    private float charsPerSecond = 0.1f; // 打字時間間隔
    public string[] words; // 保存需要顯示的文字
    private int[] RolesIndex;
    public int strindex = 0; // 控制語句
    public static dialogue instance; // 對戰管理實體物件
    private bool isActive = false;
    private float lastRealTime; // 記錄上一次的實時時間
    public Text myText;
    public GameObject Dia;
    public int currentPos = 0; // 當前打字位置
    public bool islongWriting = false;
    public bool gogame = false;
    public bool end = false;
    public static bool startdia = false;
    public GameObject Arrow;
    public Image People;
    public Sprite[] PeopleTexture;
    public Text PeopleNameText;
    private string[] names;

    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        startdia = false;
        names = new string[] { "璇璇", "忍者莉", "國國騎士" , "礦工鼠翔" , "魔法師-史蒂芬" , "魔法師-蓉" , "網美璇璇", "真‧網美璇璇"};

        lastRealTime = Time.realtimeSinceStartup;
    }

    public void InitDialogue(string[] dialogueTexts, int[] dialoguePictureIndexs)
    {
        Dia.SetActive(true);
        Time.timeScale = 0f;
        words = dialogueTexts;
        RolesIndex = dialoguePictureIndexs;
        People.sprite = PeopleTexture[RolesIndex[0]];
        PeopleNameText.text = names[RolesIndex[0]];
        Startdia();
    }

    public void Update()
    {
        if (Dia.active == true)
        {
            OnStartWriter();
            if (Input.GetMouseButtonDown(0))
            {
                if (!end)
                {
                    myText.text = words[strindex];
                    OnFinish();
                    Arrow.SetActive(true);
                }
                else
                {
                    Arrow.SetActive(false);
                    myText.text = "";
                    lastRealTime = Time.realtimeSinceStartup;
                    currentPos = 0;
                    strindex++; // 下一句
                    isActive = true;
                    end = false;
                    if (strindex >= words.Length) // 防止超出字串的長度
                    {
                        strindex = 0;
                        Dia.SetActive(false);
                        Time.timeScale = 1f;
                        //startdia = true;
                        gogame = true;
                    }
                    else
                    {
                        People.sprite = PeopleTexture[RolesIndex[strindex]];
                        PeopleNameText.text = names[RolesIndex[strindex]];
                    } 
                }
            }
        }
    }

    public void StartEffect()
    {
        isActive = true;
    }

    public void Startdia()
    {
        lastRealTime = Time.realtimeSinceStartup;
        currentPos = 0;
        strindex = 0;
        isActive = true;
    }

    /// 打字效果
    public void OnStartWriter()
    {
        if (isActive)
        {
            float realDeltaTime = Time.realtimeSinceStartup - lastRealTime;

            if (realDeltaTime >= charsPerSecond)
            {
                if (!end)
                {
                    lastRealTime = Time.realtimeSinceStartup;
                    currentPos++;
                    myText.text = words[strindex].Substring(0, currentPos); // 刷新文本顯示内容
                    if (currentPos >= words[strindex].Length)
                    {
                        OnFinish();
                    }
                }
            }
        }
    }

    /// 結束打字，初始化數據
    public void OnFinish()
    {
        isActive = false;
        end = true;
        lastRealTime = Time.realtimeSinceStartup;
        currentPos = 0;
    }
}