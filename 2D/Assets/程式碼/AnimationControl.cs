using UnityEngine;
using UnityEngine.UI;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class AnimationControl : MonoBehaviour
{

    public void Start()
    {
        if (gameObject.layer == LayerMask.NameToLayer("隕石爆炸"))
        {
            StartCoroutine(DESOB());
        }
    }
    void Update()
    {
        if (LVsave.magiciandead == true && gameObject.layer == LayerMask.NameToLayer("隕石"))
        {
            Destroy(gameObject);
        }

        if (LVsave.icedead == true && gameObject.layer == LayerMask.NameToLayer("火球"))
        {
            Destroy(gameObject);
        }
        if (LVsave.dragonkillerdead == true && gameObject.layer == LayerMask.NameToLayer("劍氣"))
        {
            Destroy(gameObject);
        }
    }
    public IEnumerator DESOB()
    {
       yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
