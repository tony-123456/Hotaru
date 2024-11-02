using UnityEngine;

public class Ball : MonoBehaviour
{
    public float damage = 3f;
    public string type;
    
    public void Update()
    {
        transform.Translate(-4 * Time.deltaTime, 0, 0, Space.World);    
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
    public void Start()
    {
        Invoke("destoryob", 10);
    }
    public void destoryob()
    {
        Destroy(gameObject);
    }
    
}
