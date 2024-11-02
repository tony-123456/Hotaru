using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("生成資料")]
    public EnemySpawnData data;
    public bool isspawn = false;
    private BattleManager bm;
    public bool pass;
    private void Start()
    {
        bm = FindObjectOfType<BattleManager>();  
    }

    private void Update()
    {
        if(dialogue.instance.gogame == true && isspawn ==false)
        {
            print("生成開始");
            isspawn = true;
            StartCoroutine(StartSpawn());
        }
    }

    private IEnumerator StartSpawn()
    {
        for (int i = 0; i < data.spawn.Length; i++)
        {
            if (bm.FinishBattle == true) yield break;
            else yield return new WaitForSeconds(data.spawn[i].time);

          for (int j = 0; j < data.spawn[i].monsters.Length; j++)
            {
               Vector3 pos = new Vector3(data.spawn[i].monsters[j].x, data.spawn[i].monsters[j].y, 0); //座標
                if(pos == new Vector3(6,-1))
                {
                    Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
                    LVsave.boosspawn = true;
                    GameObject temp = Instantiate(data.spawn[i].monsters[j].monster, pos, qua); //生成
                }
                else if (pos == new Vector3(7.2f, 0.11f))
                {
                    Quaternion qua = Quaternion.Euler(0, 180, 0); //角度
                    GameObject temp = Instantiate(data.spawn[i].monsters[j].monster, pos, qua); //生成
                    LVsave.boosspawn = true;
                    LVsave.finalboss = true;
                    dialogue.instance.InitDialogue(new string[] { "憑你們這些醜der也想挑戰我?", "廢話少說解藥拿來!", "看來今天不把你們薰死，你們不會罷休的，嚐嚐我的芳香屁吧!" }, new int[] { 6, 0, 6 });
                }
                else if (pos == new Vector3(7.2f, 0))
                {
                    Quaternion qua = Quaternion.Euler(0, 180, 0); //角度
                    GameObject temp = Instantiate(data.spawn[i].monsters[j].monster, pos, qua); //生成
                    LVsave.boosspawn = true;
                }
                else
                {
                    Quaternion qua = Quaternion.Euler(0, 180, 0); //角度
                    GameObject temp = Instantiate(data.spawn[i].monsters[j].monster, pos, qua); //生成

                }               
            }
        }
    }
}