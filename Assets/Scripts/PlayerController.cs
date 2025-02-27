using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    // 入力
    public InputActionAsset inputActions;
    public InputActionMap playerActionMap;
    public InputAction moveInput;
    public InputAction attackLKInput, attackMKInput, attackLPInput, attackMPInput, attackHPInput, attackHKInput, parryInput;


    // 変数
    private float speed = 5.0f;
    public GameObject enemy, bullet;

    // 他クラス
    public AnimController animController;
    public StateManager stateManager;
    public AttackManager attackManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PlayerのActionInputを取得
        playerActionMap = inputActions.FindActionMap("Player");

        // PlayerのActionInputからmoveInputとattackInputを取得
        moveInput = playerActionMap.FindAction("Move");
        attackLKInput = playerActionMap.FindAction("Attack_LK");
        attackMKInput = playerActionMap.FindAction("Attack_MK");
        attackLPInput = playerActionMap.FindAction("Attack_LP");
        attackMPInput = playerActionMap.FindAction("Attack_MP");
        attackHPInput = playerActionMap.FindAction("Attack_HP");
        attackHKInput = playerActionMap.FindAction("Attack_HK");
        parryInput = playerActionMap.FindAction("Parry");

        // ActionInputの有効化
        moveInput.Enable();
        attackLKInput.Enable();
        attackMKInput.Enable();
        attackLPInput.Enable();
        attackMPInput.Enable();
        attackHPInput.Enable();
        attackHKInput.Enable();
        parryInput.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // 入力をテンキー表示
        int input = GetInput();

        // 硬直中でない
        if (!stateManager.isAttacking)
        {
            // 1P, 2Pの判定とキャラの向きを更新
            if (enemy.transform.position.x < transform.position.x)
            {
                stateManager.isLeftSide = false;
            }
            else
            {
                stateManager.isLeftSide = true;
            }

            if ((stateManager.isGrounded && stateManager.isLeftSide && transform.localScale.x < 0) ||
                (stateManager.isGrounded && !stateManager.isLeftSide && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            // パリィ中でない場合
            if (!stateManager.isParrying)
            {
                // 攻撃
                if (attackLKInput.triggered)
                {
                    if (!stateManager.isGrounded)
                    {
                        stateManager.Attacking();
                        animController.JumpAttack();
                        attackManager.JumpAttack();
                        return;
                    }
                    else
                    {
                        Debug.Log("弱キック");
                        return;
                    }
                }
                if (attackMKInput.triggered)
                {
                    if (!stateManager.isGrounded)
                    {
                        stateManager.Attacking();
                        animController.JumpAttack();
                        attackManager.JumpAttack();
                        return;
                    }
                    else
                    {
                        stateManager.Attacking();
                        animController.Attack_MK();
                        attackManager.Attack_MK();
                        return;
                    }
                }
                if (attackLPInput.triggered)
                {
                    if (!stateManager.isGrounded)
                    {
                        stateManager.Attacking();
                        animController.JumpAttack();
                        attackManager.JumpAttack();
                        return;
                    }
                    else
                    {
                        stateManager.Attacking();
                        animController.Attack_LP();
                        attackManager.Attack_LP();
                        return;
                    }
                }
                if (attackMPInput.triggered)
                {
                    if (!stateManager.isGrounded)
                    {
                        stateManager.Attacking();
                        animController.JumpAttack();
                        attackManager.JumpAttack();
                        return;
                    }
                    else
                    {
                        stateManager.Attacking();
                        animController.Attack_MP();
                        attackManager.Attack_MP();
                        return;
                    }
                }
                if (attackHPInput.triggered)
                {
                    if (!stateManager.isGrounded)
                    {
                        stateManager.Attacking();
                        animController.JumpAttack();
                        attackManager.JumpAttack();
                        return;
                    }
                    else
                    {
                        stateManager.Attacking();
                        animController.Attack_HP();
                        attackManager.Attack_HP();
                        return;
                    }
                }
                if (parryInput.ReadValue<float>() > 0 && stateManager.isGrounded)
                {
                    Debug.Log("Parrying!");
                    stateManager.Parrying();
                    animController.Parrying(); // 今は仮にHPにParryを設定
                    return;
                }

                // 地上判定
                if (stateManager.isGrounded)
                {
                    // ジャンプ
                    if (input == 7 || input == 8 || input == 9)
                    {
                        animController.Jumping();
                        stateManager.Jumping();
                        Jumping(input);
                    }
                    // しゃがみ
                    else if (input == 1 || input == 2 || input == 3)
                    {
                        if (!stateManager.isCrouching)
                        {
                            animController.Crouching();
                        }
                        if (stateManager.isLeftSide && input == 1 || !stateManager.isLeftSide && input == 3)
                        {
                            stateManager.Crouching(true);
                        }
                        else
                        {
                            stateManager.Crouching(false);
                        }

                        Crouching(input);
                    }
                    // 立ち状態
                    else
                    {
                        // 歩き
                        if (input == 4 || input == 6)
                        {
                            if (!stateManager.isWalking)
                            {
                                animController.Walking();
                            }
                            Walking(input);

                            // 後ろ歩き
                            if (stateManager.isLeftSide && input == 4 || !stateManager.isLeftSide && input == 6)
                            {
                                stateManager.Walking(true);
                            }
                            else
                            {
                                stateManager.Walking(false);
                            }
                        }
                        // 直立
                        else
                        {
                            if (!stateManager.isIdleing)
                            {
                                animController.Idleing();
                                stateManager.Idleing();
                            }
                        }

                    }
                }
            }
            else
            {
                if (parryInput.ReadValue<float>() == 0)
                {
                    stateManager.Idleing();
                    animController.Idleing();
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            stateManager.isGrounded = true;
        }
    }

    public void Walking(int input)
    {
        // 移動
        if (input == 6)
        {
            transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
        }
        else if (input == 4)
        {
            transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime);
        }
    }
    public void Crouching(int input)
    {
        // 前しゃがみ
        if (input == 3)
        {
            Debug.Log("前しゃがみ");
        }
        // 後ろしゃがみ
        else if (input == 1)
        {
            Debug.Log("後ろしゃがみ");
        }
        // しゃがみ
        else if (input == 2)
        {
            Debug.Log("しゃがみ");
        }
    }
    public void Jumping(int input)
    {
        // 前ジャンプ
        if (input == 9)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            
            rb.AddForce(new Vector2(5, 20), ForceMode2D.Impulse);
        }
        // 後ろジャンプ
        else if (input == 7)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-5, 20), ForceMode2D.Impulse);
        }
        // 垂直ジャンプ
        else if (input == 8)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
        }
    }
   

    public int GetInput()
    {
        // 入力の取得
        Vector2 move = moveInput.ReadValue<Vector2>();

        int currentInput;
        if (move.x > 0.5f)
        {
            if (move.y > 0.5f)
            {
                currentInput = 9;
            }
            else if (move.y < -0.5f)
            {
                currentInput = 3;
            }
            else
            {
                currentInput = 6;
            }
        }
        else if (move.x < -0.5f)
        {
            if (move.y > 0.5f)
            {
                currentInput = 7;
            }
            else if (move.y < -0.5f)
            {
                currentInput = 1;
            }
            else
            {
                currentInput = 4;
            }
        }
        else
        {
            if (move.y > 0.5f)
            {
                currentInput = 8;
            }
            else if (move.y < -0.5f)
            {
                currentInput = 2;
            }
            else
            {
                currentInput = 5;
            }
        }
        return currentInput;
    }
}
