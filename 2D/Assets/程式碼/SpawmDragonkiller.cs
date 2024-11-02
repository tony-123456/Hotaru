
using UnityEngine;

public class SpawmDragonkiller : MonoBehaviour
{
    public GameObject dragonkiller;
    public bool dragonkillerspawn = false;

     public void Update()
    {
       if( dialogue.instance.gogame == true && dragonkillerspawn == false)
        {
            spawndragonkill();
        }    
    }
    public void spawndragonkill()
    {
        dragonkillerspawn = true;
        Vector3 pos = new Vector2(-60f, -2.3f); //座標
        Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
        GameObject temp = Instantiate(dragonkiller, pos, qua); //生成
        temp.gameObject.GetComponent<RoleMove>().speed = 2; //添加元件<盟友移動> 速度 = 卡牌.速度
        temp.gameObject.GetComponent<RoleMove>().damage = 3; //添加元件<盟友移動>.傷害 = 卡牌.攻擊
        temp.gameObject.GetComponent<RoleMove>().AttackCD = 3; //添加元件<盟友移動>.攻擊速度 = 卡片.攻擊速度
        temp.gameObject.GetComponent<RoleMove>().AttackDistance = 1; //添加元件<盟友移動>.攻擊距離 = 卡片.攻擊距離
        temp.gameObject.GetComponent<Alliance>().hp = 30; //添加元件<盟友>.血量 = 卡片.血量
    }
}
