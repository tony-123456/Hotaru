using UnityEngine;
using System.Collections;
public class EnemyMove : MonoBehaviour
{
    public static EnemyMove instance; //對戰管理實體物件
    public float speed;
    public LayerMask RoleLayer;
    public bool Stop;
    public float damage;
    public float AttackCD;
    private Animator ani;
    public bool CanAttack;
    public RaycastHit2D hit;
    public RaycastHit2D hit2;
    public RaycastHit2D hit3;
    public float AttackDistance;
    public bool dead;
    public GameObject fireball;
    public GameObject explotion;
    public GameObject firestone;
    public GameObject thounder;
    public void Start()
    {
        instance = this;
        ani = GetComponent<Animator>();
        CanAttack = true;
    }
    /// <summary>
    /// 移動方法
    /// </summary>
    /// 
    private void MoveMethod()
    {
        if (dead == false)
        {
            if (Stop == false)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
            }
        }
    }

    private void Update()
    {
        if(dead == false)
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
            Vector2 direction = Vector2.left;
            float distance = AttackDistance;
            Debug.DrawRay(position, direction, Color.green);
            hit = Physics2D.Raycast(position, direction, distance, RoleLayer);
            if (hit.collider != null)
            {                            
                    if (hit.collider.tag == "我方基地")
                    {
                       if (gameObject.layer == LayerMask.NameToLayer("火靈王"))
                        {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);
                            yield return new WaitForSeconds(0.1f);
                            Vector3 posFire = transform.position; //火球座標 = 屠龍者座標
                            posFire.x -= 1.1f;  //微調y軸
                            posFire.y -= 0.5f;  //微調y軸
                            hit.collider.GetComponent<AllianceBase>().Damage(damage);
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp = Instantiate(firestone, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            ani.SetBool("攻擊完", true);
                            yield return new WaitForSeconds(2);
                            ani.SetBool("攻擊完", false);
                      }
                      else if (gameObject.layer == LayerMask.NameToLayer("守護者"))
                      {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);
                            Vector3 pos1 = new Vector3(-1.83f, 0.77f, 0);
                            Vector3 pos2 = new Vector3(1.79f, 0.77f, 0);
                            Vector3 pos3 = new Vector3(5.41f, 0.77f, 0);
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp = Instantiate(thounder, pos1, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp2 = Instantiate(thounder, pos2, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp3 = Instantiate(thounder, pos3, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);
                            ani.SetBool("攻擊", false);
                            yield return new WaitForSeconds(2f);
                      }
                      else
                        {
                            CanAttack = false;
                            ani.SetBool("攻擊", true);
                            Stop = true;
                            hit.collider.GetComponent<AllianceBase>().Damage(damage);
                            yield return new WaitForSeconds(AttackCD);
                       }
                    }
                    else if (hit.collider.tag == "我方角色")
                    {
                        if (gameObject.layer == LayerMask.NameToLayer("火靈王"))
                        {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);
                            Vector3 posFire = transform.position; //火球座標 = 屠龍者座標
                            posFire.x -= 1.1f;  //微調y軸
                            posFire.y -= 0.5f;  //微調y軸
                            hit.collider.GetComponent<Alliance>().Damage(damage);
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp = Instantiate(firestone, posFire, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            ani.SetBool("攻擊完", true);
                            yield return new WaitForSeconds(2);
                            ani.SetBool("攻擊完", false);
                        }
                        else if (gameObject.layer == LayerMask.NameToLayer("守護者"))
                        {
                            CanAttack = false;
                            Stop = true;
                            ani.SetBool("攻擊", true);
                            Vector3 pos1 = new Vector3(-1.83f, 0.77f, 0);
                            Vector3 pos2 = new Vector3(1.79f, 0.77f, 0);
                            Vector3 pos3 = new Vector3(5.41f, 0.77f, 0);
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp = Instantiate(thounder, pos1, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);
                            GameObject temp2 = Instantiate(thounder, pos2, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);                   
                            GameObject temp3 = Instantiate(thounder, pos3, Quaternion.identity); //生成(物件,座標,角度)，Quaternion.identity 角度類型-零角度
                            yield return new WaitForSeconds(0.5f);
                            ani.SetBool("攻擊", false);
                            yield return new WaitForSeconds(2f);
                        }
                        else
                        {
                             CanAttack = false;
                             ani.SetBool("攻擊", true);
                             Stop = true;
                             hit.collider.GetComponent<Alliance>().Damage(damage);
                             yield return new WaitForSeconds(AttackCD);
                        }
                    }                        
                   CanAttack = true;
                }
            else
            {
                Stop = false;
                ani.SetBool("攻擊", false);
            }
        }        
    }

}