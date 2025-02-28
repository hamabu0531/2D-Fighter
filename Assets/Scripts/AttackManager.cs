using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackManager : MonoBehaviour
{
    public StateManager stateManager;
    public HitBoxManager hitBoxManager;
    private JsonDB jsonDB;

    public GameObject bullet;
    private Transform attackBoxes;
    private GameObject attackBox_LP, attackBox_MP, attackBox_HP, attackBox_JumpAttack;

    private Dictionary<string, AttackInfo> attackData;

    private void Start()
    {
        jsonDB = GameObject.Find("JsonDB").GetComponent<JsonDB>();
        attackData = jsonDB.jsonData;

        attackBoxes = transform.parent.Find("AttackBoxes");
        attackBox_LP = attackBoxes.Find("AttackBox_LP").gameObject;
        attackBox_MP = attackBoxes.Find("AttackBox_MP").gameObject;
        attackBox_HP = attackBoxes.Find("AttackBox_HP").gameObject;
        attackBox_JumpAttack = attackBoxes.Find("AttackBox_JumpAttack").gameObject;
    }

    // 弱パンチ
    public void Attack_LP()
    {
        if(attackData == null || !attackData.ContainsKey("LP")) return;

        int recovery = attackData["LP"].recovery; // 全体フレーム(硬直)
        int startup = attackData["LP"].startup; // 発生フレーム
        int active = attackData["LP"].active; // 攻撃持続フレーム

        Debug.Log("弱パンチ");

        StartCoroutine(LP_Coroutine(recovery, startup, active));
    }

    private IEnumerator LP_Coroutine(int recovery, int startup, int active)
    {
        int i = 0;
        // 発生フレームまで待機
        while (i < startup)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定
        attackBox_LP.SetActive(true);
        // 当たり判定調整
        hitBoxManager.HitBox_LP();

        // 攻撃持続終了まで待機
        int j = 0;
        while (j < active)
        {
            j++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定終了
        attackBox_LP.SetActive(false);

        // 硬直終了まで待機
        while (i < recovery - active)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 硬直解除
        stateManager.isAttacking = false;

        // 当たり判定調整
        hitBoxManager.ResetHitbox();
    }

    // 中パンチ
    public void Attack_MP()
    {
        if (attackData == null || !attackData.ContainsKey("MP")) return;

        int recovery = attackData["MP"].recovery; // 全体フレーム(硬直)
        int startup = attackData["MP"].startup; // 発生フレーム
        int active = attackData["MP"].active; // 攻撃持続フレーム

        Debug.Log("中パンチ");

        StartCoroutine(MP_Coroutine(recovery, startup, active));
    }

    private IEnumerator MP_Coroutine(int recovery, int startup, int active)
    {
        int i = 0;
        // 発生フレームまで待機
        while (i < startup)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定
        attackBox_MP.SetActive(true);
        // 当たり判定調整
        hitBoxManager.HitBox_MP();

        // 攻撃持続終了まで待機
        int j = 0;
        while (j < active)
        {
            j++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定終了
        attackBox_MP.SetActive(false);

        // 硬直終了まで待機
        while (i < recovery - active)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 硬直解除
        stateManager.isAttacking = false;

        // 当たり判定調整
        hitBoxManager.ResetHitbox();
    }

    // 強パンチ
    public void Attack_HP()
    {
        if (attackData == null || !attackData.ContainsKey("HP")) return;

        int recovery = attackData["HP"].recovery; // 全体フレーム(硬直)
        int startup = attackData["HP"].startup; // 発生フレーム
        int active = attackData["HP"].active; // 攻撃持続フレーム

        Debug.Log("強パンチ");

        StartCoroutine(HP_Coroutine(recovery, startup, active));
    }

    private IEnumerator HP_Coroutine(int recovery, int startup, int active)
    {
        int i = 0;
        // 発生フレームまで待機
        while (i < startup)
        {
            i++;
            yield return null; // 1フレーム待機
        }
        // 攻撃判定
        attackBox_HP.SetActive(true);

        // 攻撃持続終了まで待機
        int j = 0;
        while (j < active)
        {
            j++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定終了
        attackBox_HP.SetActive(false);

        // 硬直終了まで待機
        while (i < recovery - active)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 硬直解除
        stateManager.isAttacking = false;

        // 当たり判定調整
        hitBoxManager.ResetHitbox();
    }

    // 中キック
    public void Attack_MK()
    {
        //if (attackData == null || !attackData.ContainsKey("MK")) return;

        int recovery = attackData["MK"].recovery; // 全体フレーム(硬直)
        int startup = attackData["MK"].startup; // 発生フレーム
        int active = attackData["MK"].active; // 攻撃持続フレーム

        Debug.Log("中キック");

        StartCoroutine(MK_Coroutine(recovery, startup, active));
    }

    private IEnumerator MK_Coroutine(int recovery, int startup, int active)
    {
        int i = 0;
        // 発生フレームまで待機
        while (i < startup)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定
        Vector3 generatePos = transform.position + new Vector3(stateManager.isLeftSide ? 1.8f : -1.8f, -1, 0);
        GameObject bul = Instantiate(bullet, generatePos, Quaternion.identity, this.transform);

        // 攻撃持続終了まで待機
        int j = 0;
        while (j < active)
        {
            j++;
            yield return null; // 1フレーム待機
        }

        // 硬直終了まで待機
        while (i < recovery - active)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 硬直解除
        stateManager.isAttacking = false;

        // 当たり判定調整
        hitBoxManager.ResetHitbox();
    }

    // ジャンプ攻撃
    public void JumpAttack()
    {
        if (attackData == null || !attackData.ContainsKey("JumpAttack")) return;

        int recovery = attackData["JumpAttack"].recovery; // 着地後の硬直
        int startup = attackData["JumpAttack"].startup; // 発生フレーム
        int active = attackData["JumpAttack"].active; // 攻撃持続フレーム

        stateManager.Attacking();

        Debug.Log("ジャンプ攻撃");

        StartCoroutine(JumpAttack_Coroutine(recovery, startup, active));
    }
    private IEnumerator JumpAttack_Coroutine(int recovery, int startup, int active)
    {
        int i = 0;
        // 発生フレームまで待機
        while (i < startup)
        {
            // 発生前に着地した場合
            if (stateManager.isGrounded)
            {
                break;
            }

            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定
        attackBox_JumpAttack.SetActive(true);

        // 攻撃持続終了まで待機
        int j = 0;
        while (j < active)
        {
            j++;
            yield return null; // 1フレーム待機
        }

        // 攻撃持続終了
        attackBox_JumpAttack.SetActive(false);

        // 硬直終了まで待機
        i = 1;
        while (!stateManager.isGrounded)
        {
            yield return null; // 1フレーム待機
        }
        while (i < recovery - active)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 硬直解除
        stateManager.isAttacking = false;

        // 当たり判定調整
        hitBoxManager.ResetHitbox();
    }
}
