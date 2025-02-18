using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    // 入力
    public InputActionAsset inputActions;
    public InputActionMap playerActionMap;
    public InputAction moveInput;
    public InputAction attackLKInput, attackMKInput, attackLPInput, attackMPInput;

    // ステータス


    // 変数
    private float speed = 5.0f;
    public GameObject enemy, bullet;

    // 他クラス
    public FrameManager frameManager;
    public AnimController animController;
    public StateManager stateManager;

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

        // ActionInputの有効化
        moveInput.Enable();
        attackLKInput.Enable();
        attackMKInput.Enable();
        attackLPInput.Enable();
        attackMPInput.Enable();
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

            // 攻撃
            if (attackLKInput.triggered)
            {
                if (!stateManager.isGrounded)
                {
                    stateManager.Attacking();
                    animController.JumpAttack();
                    JumpAttack();
                }
                else
                {
                    Debug.Log("弱キック");
                }
            }
            if (attackMKInput.triggered)
            {
                if (!stateManager.isGrounded)
                {
                    stateManager.Attacking();
                    animController.JumpAttack();
                    JumpAttack();
                }
                else
                {
                    stateManager.Attacking();
                    animController.Attack_MK();
                    Attack_MK();
                    Debug.Log("中キック");
                }
            }
            if (attackLPInput.triggered)
            {
                if (!stateManager.isGrounded)
                {
                    stateManager.Attacking();
                    animController.JumpAttack();
                    JumpAttack();
                }
                else
                {
                    Debug.Log("弱パンチ");
                }
            }
            if (attackMPInput.triggered)
            {
                if (!stateManager.isGrounded)
                {
                    stateManager.Attacking();
                    animController.JumpAttack();
                    JumpAttack();
                }
                else
                {
                    Debug.Log("中パンチ");
                }
            }

            // 攻撃中でない場合
            if (!attackMPInput.triggered && !attackMKInput.triggered && !attackLPInput.triggered && !attackLKInput.triggered)
            {
                // ジャンプ
                if (stateManager.isGrounded && (input == 7 || input == 8 || input == 9))
                {
                    animController.Jumping();
                    stateManager.Jumping();
                    Jumping(input);
                }
                // しゃがみ
                else if (stateManager.isGrounded && (input == 1 || input == 2 || input == 3))
                {
                    if (!stateManager.isCrouching)
                    {
                        animController.Crouching();
                        stateManager.Crouching();
                    }
                    Crouching(input);
                }
                // 立ち状態
                else if (stateManager.isGrounded)
                {
                    // 歩き
                    if (input == 4 || input == 6)
                    {
                        if (!stateManager.isWalking)
                        {
                            animController.Walking();
                            stateManager.Walking();
                        }
                        Walking(input);
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

        // 硬直解除
        if (frameManager.currentFrame >= frameManager.movableFrame)
        {
            stateManager.isAttacking = false;
            animController.End_Cast();
        }

        // 攻撃発生(弾)
        if (frameManager.currentFrame == frameManager.startupFrame)
        {
            Vector3 generatePos = transform.position + new Vector3(stateManager.isLeftSide ? 1.5f : -1.5f, -1, 0);
            Instantiate(bullet, generatePos, Quaternion.identity);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
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
            rb.AddForce(new Vector2(5, 15), ForceMode2D.Impulse);
        }
        // 後ろジャンプ
        else if (input == 7)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-5, 15), ForceMode2D.Impulse);
        }
        // 垂直ジャンプ
        else if (input == 8)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 15), ForceMode2D.Impulse);
        }
    }
    public void JumpAttack()
    {
        // 攻撃
    }
    public void Attack_MK()
    {
        int recovery = 50; // 全体フレーム(硬直)
        int startup = 40; // 発生フレーム

        Debug.Log("飛び道具");

        frameManager.startupFrame = frameManager.currentFrame + startup;
        frameManager.movableFrame = frameManager.currentFrame + recovery;
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
