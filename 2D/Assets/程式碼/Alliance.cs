using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Alliance : MonoBehaviour
{
    public float hp ;


    private BattleManager bm;
    /// <summary>
    /// 動畫控制器
    /// </summary>
    private Animator ani;

    public bool dead = false;

    private void Start()
    {
        if (gameObject.layer == LayerMask.NameToLayer("礦工"))
        {
            LVsave.isMinerSpawn = true;
        }
        gameObject.GetComponent<Alliance>().dead = dead; //添加元件<移動>.速度 = 速度
        ani = GetComponent<Animator>(); //取得元件<泛型>()
        bm = FindObjectOfType<BattleManager>();
    }
    public void Update()
    {
        if (dead == false)
        {
            if (BattleManager.instance.loseLv == true)
            {
                StartCoroutine(Dead());
            }
        }
    }
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收收到的傷害值</param>
    public void Damage(float damage)
    {
        hp -= damage;
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        Invoke("ResetColor", 0.2f);
        if (dead == false)
        {
            if (hp <= 0)
            {
                StartCoroutine(Dead());
            }
        }
    }
    private void ResetColor()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
    /// <summary>
    /// 死亡
    /// </summary>
    private IEnumerator Dead()
    {
        dead = true;
        ani.SetTrigger("死亡觸發");
        if (gameObject.layer == LayerMask.NameToLayer("魔法師"))
        {
            LVsave.magiciandead = true;
        }
        if (gameObject.layer == LayerMask.NameToLayer("屠龍武士"))
        {
            LVsave.dragonkillerdead = true;
        }
        if (gameObject.layer == LayerMask.NameToLayer("礦工"))
        {
            LVsave.isMinerSpawn = false;
        }
        yield return new WaitForSeconds(1);
     
        Destroy(gameObject);
    }      
}
