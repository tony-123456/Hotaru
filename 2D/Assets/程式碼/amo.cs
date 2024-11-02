using UnityEngine;

public class amo : MonoBehaviour
{
    public float damage = 3f;
    public void Update()
    {
        transform.Translate(10 * Time.deltaTime, 0, 0, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "怪物")//碰到怪物物件玩家
        {
            other.GetComponent<Enemy>().Damage(damage);//將傷害值傳給玩家
            Destroy(gameObject);
        }
        if (other.tag == "敵方基地")//碰到怪物物件玩家
        {
            other.GetComponent<EnemyBase>().Damage(damage);//將傷害值傳給玩家
            Destroy(gameObject);
        }
    }
}
