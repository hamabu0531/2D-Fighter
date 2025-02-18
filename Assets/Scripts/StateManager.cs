using UnityEngine;

public class StateManager : MonoBehaviour
{
    public bool isGrounded = true; // 地面に接地しているかどうか
    public bool isLeftSide = true; // 左側にいるかどうか
    public bool isAttacking = false; // 攻撃中かどうか
    public bool isIdleing = false; // アイドル状態かどうか
    public bool isWalking = false; // 歩行状態かどうか
    public bool isCrouching = false; // しゃがみ状態かどうか


    public void Idleing()
    {
        isIdleing = true;
        isWalking = false;
        isCrouching = false;
    }

    public void Walking()
    {
        isIdleing = false;
        isWalking = true;
        isCrouching = false;
    }

    public void Crouching()
    {
        isIdleing = false;
        isWalking = false;
        isCrouching = true;
    }

    public void Jumping()
    {
        isIdleing = false;
        isWalking = false;
        isCrouching = false;
        isGrounded = false;
    }

    public void Attacking()
    {
        isIdleing = false;
        isWalking = false;
        isCrouching = false;
        isAttacking = true;
    }
}
