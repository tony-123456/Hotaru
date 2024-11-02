using UnityEngine;
using UnityEngine.SceneManagement;//引用場景管理API

public class LVsave : MonoBehaviour
{
    public static int lastLV = 1;
    public static int RolesCount = 1;
    public static bool icedead = false;
    public static bool magiciandead = false;
    public static bool dragonkillerdead = false;
    public static LVsave instance;
    public static bool exitdeck;
    public static bool isMinerSpawn = false;
    public static bool boosspawn = false;
    public static bool finalboss = false;
    public void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        print("lv為 :" + lastLV);
    }
}
