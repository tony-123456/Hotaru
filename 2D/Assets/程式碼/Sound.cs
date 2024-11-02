using UnityEngine;
using System.Collections.Generic;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class Sound : MonoBehaviour
{
    public AudioSource aud;
    public AudioClip kingsound;
    public AudioClip kingbgm;
    public bool play = false;


    public void Update()
    {
        if(play ==false)
        {
            StartCoroutine(KingSound());
        }
    }
    public IEnumerator KingSound()
    {
        if(LVsave.boosspawn == true)
        {
            play = true;
            aud.PlayOneShot(kingsound);
            yield return new WaitForSeconds(1f);
            if(LVsave.finalboss == true)
            {
                aud.clip = kingbgm;
                aud.Play();
            }          
        }
    }

}
