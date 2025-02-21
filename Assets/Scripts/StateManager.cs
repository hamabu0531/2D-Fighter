using UnityEngine;

public class StateManager : MonoBehaviour
{
    public bool isGrounded = true; // 地面に接地しているかどうか
    public bool isLeftSide = true; // 左側にいるかどうか
    public bool isAttacking = false; // 攻撃中かどうか
    public bool isIdleing = false; // アイドル状態かどうか
    public bool isWalking = false; // 歩行状態かどうか
    public bool isCrouching = false; // しゃがみ状態かどうか
    public bool isGuarding = false; // 防御状態かどうか
    public bool isParrying = false; // 受け流し状態かどうか

    public int HP = 100;


    public void Idleing()
    {
        isIdleing = true;
        isWalking = false;
        isCrouching = false;
        isGuarding = false;
        isParrying = false;
    }

    public void Walking(bool guard)
    {
        isIdleing = false;
        isWalking = true;
        isCrouching = false;
        isGuarding = guard;
        isParrying = false;
    }

    public void Crouching(bool guard)
    {
        isIdleing = false;
        isWalking = false;
        isCrouching = true;
        isGuarding = guard;
        isParrying = false;
    }

    public void Jumping()
    {
        isIdleing = false;
        isWalking = false;
        isCrouching = false;
        isGrounded = false;
        isGuarding = false;
        isParrying = false;
    }

    public void Attacking()
    {
        isIdleing = false;
        isWalking = false;
        isCrouching = false;
        isAttacking = true;
        isGuarding = false;
        isParrying = false;
    }

    public void Parrying()
    {
        isIdleing = false;
        isWalking = false;
        isCrouching = false;
        isAttacking = false;
        isGuarding = false;
        isParrying = true;
    }
}
