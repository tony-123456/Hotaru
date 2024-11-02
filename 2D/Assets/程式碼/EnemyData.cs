using UnityEngine;

[CreateAssetMenu(fileName = "怪物資料", menuName = "TowerDefend/怪物資料")]
public class EnemyData : ScriptableObject
{
    [Header("血量"), Range(1, 200)]
    public float hp;
    [Header("攻擊"), Range(0, 10)]
    public float attack;
    [Header("移動速度"), Range(0, 10)]
    public float speed;
    [Header("攻擊速度"), Range(0, 10)]
    public float attackcd;
    [Header("攻擊距離"), Range(0, 15)]
    public float AttackDistance;
    [Header("掉落金錢"), Range(0, 500)]
    public int deadmoney;
}
