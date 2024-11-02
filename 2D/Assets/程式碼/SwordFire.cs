using UnityEngine;

public class SwordFire : MonoBehaviour
{

    public float damage = 3f;
    public string type;

    public void Update()
    {
        transform.Translate(5 * Time.deltaTime, 0, 0, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "怪物")//碰到怪物物件玩家
        {
            other.GetComponent<Enemy>().Damage(damage);//將傷害值傳給玩家

        }
        if (other.tag == "敵方基地")//碰到怪物物件玩家
        {
            other.GetComponent<EnemyBase>().Damage(damage);//將傷害值傳給玩家
        }
        if (other.tag == "冰龍")//碰到怪物物件玩家
        {
            other.GetComponent<Enemy>().Damage(damage);//將傷害值傳給玩家

        }
    }
    public void Start()
    {
        Invoke("destoryob", 10);
    }
    public void destoryob()
    {
        Destroy(gameObject);
    }

}