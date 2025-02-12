using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Animator playerAnim;

    public void Idleing()
    {
        // アニメーション更新
        playerAnim.SetBool("isCrouching", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isWalking", false);
    }
    public void Walking()
    {
        // アニメーション更新
        playerAnim.SetBool("isCrouching", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isWalking", true);
    }
    public void Crouching()
    {
        // アニメーション更新
        playerAnim.SetBool("isWalking", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isCrouching", true);
    }
    public void Jumping()
    {
        // アニメーション更新
        playerAnim.SetBool("isCrouching", false);
        playerAnim.SetBool("isWalking", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isJumping", true);
    }
    public void JumpAttack()
    {
        // アニメーション更新
        playerAnim.SetBool("isJumpAttacking", true);
    }
    public void Attack_Cast()
    {
        // アニメーション更新
        playerAnim.SetBool("isCrouching", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isWalking", false);
        playerAnim.SetBool("isCasting", true);
    }
    public void End_Cast()
    {
        playerAnim.SetBool("isCasting", false);
    }
}
