using UnityEngine;
using System.Collections;

public class RoleMove : MonoBehaviour
{
    public static RoleMove instance; //對戰管理實體物件

    public float speed;
    public LayerMask RoleLayer;
    public bool Stop;
    public float damage;
    public float AttackCD;
    private Animator ani;
    public RaycastHit2D hit;
    public RaycastHit2D hit2;
    public RaycastHit2D hit3;
    public bool CanAttack;
    public float AttackDistance;
    public bool dead;
    public GameObject Meteorite;
    public GameObject explotion;
    public GameObject gathering;
    public GameObject sword;
    public GameObject amo;
    public void Start()
    {
        instance = this;
        ani = GetComponent<Animator>();
        CanAttack = true;
    }
    /// <summary>
    /// 移動方法
    /// </summary>
    private void MoveMethod()
    {
        if (dead == false)
        {
            if (Stop == false)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
                if (gameObject.layer == LayerMask.NameToLayer("屠龍武士"))
                {
                    ani.SetBool("攻擊", false);
                    ani.SetBool("攻擊完", false);
                }         
            }
        }
    }
    private void Update()
    {
        if (dead == false)
        {
            StartCoroutine(Attack());
            MoveMethod();
        }
    }
    private IEnumerator Attack()
    {
        if (CanAttack == true)
        {
            Vector2 position = transform.position;
            Vector2 direction = Vector2.right;
            float distance = AttackDistance;
            Debug.DrawRay(position, direction, Color.green);
            hit = Physics2D.Raycast(position, direction, distance, RoleLayer);
         if (hit.collider != null)
          {
                    if (hit.collider.tag == "敵方基地")
                    {                    
                        if (gameObject.layer == LayerMask.NameToLayer("魔法師"))
                        {
                            CanAttack = false;
                            Stop = true;
                            Vector3 posFire = transform.position; //火球座標 = 魔法師座標
                            posFire.x += 0.8f;  //微調x軸
                            posFire.y += 2.2f;  //微調y軸
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp1 = Instantiate(gathering, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(5);
                            Destroy(temp1);
                            ani.SetBool("攻擊", true);
                            Vector3 posMeteorite = new Vector3 (0, 7.66f, 0);
                            Vector3 posexplotion = new Vector3(0f, -1.59f, 0);
                            yield return new WaitForSeconds(1);
                            GameObject temp2 = Instantiate(Meteorite, posMeteorite, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(2.5f);
                            Destroy(temp2);                       
                            GameObject temp3 = Instantiate(explotion, posexplotion, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);
                            Destroy(temp3);
                            ani.SetBool("攻擊", false);
                        }
                        else if(gameObject.layer == LayerMask.NameToLayer("屠龍武士"))
                        {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);                          
                            Vector3 posFire = transform.position; //火球座標 = 屠龍者座標
                            posFire.x += 1.1f;  //微調y軸
                            posFire.y += 0.5f;  //微調y軸
                            hit.collider.GetComponent<EnemyBase>().Damage(damage);
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp = Instantiate(sword, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            ani.SetBool("攻擊完", true);
                            yield return new WaitForSeconds(2);
                            ani.SetBool("攻擊完", false);
                        }
                        else if (gameObject.layer == LayerMask.NameToLayer("機器人"))
                         {
                        CanAttack = false;
                        Stop = true;
                        ani.SetBool("攻擊", true);
                        Vector3 posFire = transform.position; //火球座標 = 屠龍者座標
                        posFire.x += 0.62f;  //微調y軸
                        posFire.y += 0.19f;  //微調y軸
                        GameObject temp = Instantiate(amo, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                        yield return new WaitForSeconds(AttackCD);
                    }
                        else
                        {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);
                            
                            hit.collider.GetComponent<EnemyBase>().Damage(damage);
                            yield return new WaitForSeconds(AttackCD);
                        }
                    }
                    else if (hit.collider.tag == "怪物")
                    {          
                        if (gameObject.layer == LayerMask.NameToLayer("魔法師"))
                        {
                            CanAttack = false;
                            Stop = true;
                            Vector3 posFire = transform.position; //火球座標 = 冰龍座標
                            posFire.x += 0.8f;  //微調x軸
                            posFire.y += 2.2f;  //微調y軸
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp1 = Instantiate(gathering, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(5);
                            Destroy(temp1);
                            ani.SetBool("攻擊", true);
                            Vector3 posMeteorite = new Vector3(0, 7.66f, 0);
                            Vector3 posexplotion = new Vector3(0f, -1.59f, 0);
                            yield return new WaitForSeconds(1);
                            GameObject temp2 = Instantiate(Meteorite, posMeteorite, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(2.5f);
                            Destroy(temp2);
                            GameObject temp3 = Instantiate(explotion, posexplotion, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);
                            Destroy(temp3);
                            ani.SetBool("攻擊", false);
                        }
                        else if (gameObject.layer == LayerMask.NameToLayer("屠龍武士"))
                        {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);                          
                            Vector3 posFire = transform.position; //火球座標 = 屠龍者座標
                            posFire.x += 1.1f;  //微調y軸
                            posFire.y += 0.5f;  //微調y軸
                            hit.collider.GetComponent<Enemy>().Damage(damage);
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp = Instantiate(sword, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            ani.SetBool("攻擊完", true);
                            yield return new WaitForSeconds(2);
                            ani.SetBool("攻擊完", false);
                        }
                        else if (gameObject.layer == LayerMask.NameToLayer("機器人"))
                         {
                        CanAttack = false;
                        Stop = true;
                        ani.SetBool("攻擊", true);
                        Vector3 posFire = transform.position; //火球座標 = 屠龍者座標
                        posFire.x += 0.62f;  //微調y軸
                        posFire.y += 0.19f;  //微調y軸
                        GameObject temp = Instantiate(amo, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                        yield return new WaitForSeconds(AttackCD);
                    }
                        else
                        {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);                          
                            hit.collider.GetComponent<Enemy>().Damage(damage);
                            yield return new WaitForSeconds(AttackCD);
                        }                       
                    }
                    else if (hit.collider.tag == "冰龍")
                    {
                      if (gameObject.layer == LayerMask.NameToLayer("槍手"))
                      {
                        CanAttack = false;
                        Stop = true;
                        ani.SetBool("攻擊", true);
                        hit.collider.GetComponent<Enemy>().Damage(damage);
                        yield return new WaitForSeconds(AttackCD);
                      }
                     else if (gameObject.layer == LayerMask.NameToLayer("屠龍武士"))
                      {
                        CanAttack = false;
                        Stop = true;
                        ani.SetBool("攻擊", true);
                        Vector3 posFire = transform.position; //火球座標 = 屠龍者座標
                        posFire.x += 1.1f;  //微調y軸
                        posFire.y += 0.5f;  //微調y軸
                        hit.collider.GetComponent<Enemy>().Damage(damage);
                        yield return new WaitForSeconds(0.5f);
                        GameObject temp = Instantiate(sword, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                        ani.SetBool("攻擊完", true);
                        yield return new WaitForSeconds(2);
                        ani.SetBool("攻擊完", false);
                      }
                     else
                      {
                        CanAttack = false;
                        Stop = true;
                        ani.SetBool("攻擊", true);                    
                        yield return new WaitForSeconds(AttackCD);
                      }                     
                     }
                    CanAttack = true;
          }
            else
            {
                if (gameObject.layer == LayerMask.NameToLayer("礦工"))
                {

                }
                else
                {
                    ani.SetBool("攻擊", false);
                    Stop = false;
                }
                 
            }
        }         
    }
}