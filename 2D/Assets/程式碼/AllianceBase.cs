using UnityEngine;
using UnityEngine.UI;

public class AllianceBase : MonoBehaviour
{
    public float hp = 100;
    public int maxhp = 100;
    private BattleManager bm;
    public AudioSource aud;
    public AudioClip knock;

    [Header("血量")]
    public Text HP;
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收收到的傷害值</param>
    public void Damage(float damage)
    {

        if (hp > 0)
        {
            hp -= damage;
            HP.text = "HP:" + hp + "/100";
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            aud.PlayOneShot(knock);
            Invoke("ResetColor", 0.2f);
            if (hp <= 0)
            {
                
                Destroy(gameObject, 0.5f);
                BattleManager.instance.Lose();
            }
        }
    }
    private void ResetColor()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}
