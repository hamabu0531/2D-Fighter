using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour
{
    public StateManager stateManager;

    public GameObject bullet;
    public GameObject attackBox_LP, attackBox_MP;

    // 弱パンチ
    public void Attack_LP()
    {
        int recovery = 12; // 全体フレーム(硬直)
        int startup = 4; // 発生フレーム

        Debug.Log("弱パンチ");

        StartCoroutine(LP_Coroutine(recovery, startup));
    }

    private IEnumerator LP_Coroutine(int recovery, int startup)
    {
        int i = 1;
        // 発生フレームまで待機
        while (i < startup)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定
        attackBox_LP.SetActive(true);

        // 硬直終了まで待機
        while (i < recovery)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定終了
        attackBox_LP.SetActive(false);

        // 硬直解除
        stateManager.isAttacking = false;
    }

    // 中パンチ
    public void Attack_MP()
    {
        int recovery = 20; // 全体フレーム(硬直)
        int startup = 6; // 発生フレーム

        Debug.Log("中パンチ");

        StartCoroutine(MP_Coroutine(recovery, startup));
    }

    private IEnumerator MP_Coroutine(int recovery, int startup)
    {
        int i = 1;
        // 発生フレームまで待機
        while (i < startup)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定
        attackBox_MP.SetActive(true);

        // 硬直終了まで待機
        while (i < recovery)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定終了
        attackBox_MP.SetActive(false);

        // 硬直解除
        stateManager.isAttacking = false;
    }

    // 中キック
    public void Attack_MK()
    {
        int recovery = 50; // 全体フレーム(硬直)
        int startup = 40; // 発生フレーム

        Debug.Log("中キック");

        StartCoroutine(MK_Coroutine(recovery, startup));
    }

    private IEnumerator MK_Coroutine(int recovery, int startup)
    {
        int i = 1;
        // 発生フレームまで待機
        while (i < startup)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 攻撃判定
        Vector3 generatePos = transform.position + new Vector3(stateManager.isLeftSide ? 1.8f : -1.8f, -1, 0);
        GameObject bul = Instantiate(bullet, generatePos, Quaternion.identity, this.transform);

        // 硬直終了まで待機
        while (i < recovery)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 硬直解除
        stateManager.isAttacking = false;
    }

    // ジャンプ攻撃
    public void JumpAttack()
    {
        int recovery = 3; // 着地後の硬直
        int startup = 15; // 発生フレーム

        stateManager.Attacking();

        Debug.Log("ジャンプ攻撃");

        StartCoroutine(JumpAttack_Coroutine(recovery, startup));
    }
    private IEnumerator JumpAttack_Coroutine(int recovery, int startup)
    {
        int i = 1;
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

        // 硬直終了まで待機
        i = 1;
        while (!stateManager.isGrounded)
        {
            yield return null; // 1フレーム待機
        }
        while (i < recovery)
        {
            i++;
            yield return null; // 1フレーム待機
        }

        // 硬直解除
        stateManager.isAttacking = false;
    }
}
