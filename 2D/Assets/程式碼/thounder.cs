using UnityEngine;
using System.Collections.Generic;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class thounder : MonoBehaviour
{
    public float damage = 5f;
    public string type;
    public AudioClip sound;
    public AudioSource Aud;

    public void Start()
    {
        Aud.PlayOneShot(sound);
        StartCoroutine(keep1sec());
    }
    public IEnumerator keep1sec()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "我方角色")//碰到怪物物件玩家
        {
            other.GetComponent<Alliance>().Damage(damage);//將傷害值傳給玩家
        }
        if (other.tag == "我方基地")//碰到怪物物件玩家
        {
            other.GetComponent<AllianceBase>().Damage(damage);//將傷害值傳給玩家
        }
    }

}