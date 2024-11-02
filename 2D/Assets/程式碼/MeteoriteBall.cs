using UnityEngine;

public class MeteoriteBall : MonoBehaviour
{
    
    public float damage = 20f;
    public string type;

    public void Update()
    {
        transform.Translate(0, -4 * Time.deltaTime, 0, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "怪物")//碰到怪物物件玩家
        {
            print("隕石砸到人了");
            other.GetComponent<Enemy>().Damage(damage);//將傷害值傳給玩家

        }
        if (other.tag == "敵方基地")//碰到怪物物件玩家
        {
            other.GetComponent<EnemyBase>().Damage(damage);//將傷害值傳給玩家
        }
    }
}
