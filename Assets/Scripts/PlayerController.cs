using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 入力
    public InputActionAsset inputActions;
    public InputActionMap playerActionMap;
    public InputAction moveInput;
    public InputAction attackLKInput, attackMKInput, attackLPInput, attackMPInput;

    // ステータス
    private bool isGrounded = true; // 地面に接地しているかどうか

    // 変数
    private float speed = 5.0f;
    private Animator playerAnim;

    // アニメーション
    public Sprite idleSprite, crouchSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // フレームレートの設定
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
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

        // アニメーションの設定
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = moveInput.ReadValue<Vector2>();

        // テンキー表示
        int input = 0;
        if(move.x > 0.5f)
        {
            if (move.y > 0.5f)
            {
                input = 9;
            }
            else if (move.y < -0.5f)
            {
                input = 3;
            }
            else
            {
                input = 6;
            }
        }
        else if (move.x < -0.5f)
        {
            if(move.y > 0.5f)
            {
                input = 7;
            }
            else if (move.y < -0.5f)
            {
                input = 1;
            }
            else
            {
                input = 4;
            }
        }
        else
        {
            if (move.y > 0.5f)
            {
                input = 8;
            }
            else if (move.y < -0.5f)
            {
                input = 2;
            }
            else
            {
                input = 5;
            }
        }

        // 攻撃
        if (attackLKInput.triggered)
        {
            Debug.Log("弱キック");
            if (!isGrounded)
            {
                JumpAttack();
            }
        }
        if(attackMKInput.triggered)
        {
            Debug.Log("中キック");
            if (!isGrounded)
            {
                JumpAttack();
            }
        }
        if(attackLPInput.triggered)
        {
            Debug.Log("弱パンチ");
            if (!isGrounded)
            {
                JumpAttack();
            }
        }
        if (attackMPInput.triggered)
        {
            Debug.Log("中パンチ");
            if (!isGrounded)
            {
                JumpAttack();
            }
        }

        // ジャンプ
        if (isGrounded && (input == 7 || input == 8 || input == 9))
        {
            Jumping(input);
        }
        // しゃがみ
        else if (isGrounded && (input == 1 || input == 2 || input == 3))
        {
            Crouching(input);            
        }
        // 立ち状態
        else if(isGrounded)
        {
            // 歩き
            if (input == 4 || input == 6)
            {
                Walking(input);
            }
            // 直立
            else
            {
                Idleing();
            }

        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    public void Walking(int input)
    {
        // アニメーション更新
        playerAnim.SetBool("isCrouching", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isWalking", true);

        // 移動
        if (input == 6)
        {
            transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime);
        }
        else if(input == 4)
        {
            transform.Translate(new Vector3(-speed, 0, 0) * Time.deltaTime);
        }
        
    }
    public void Idleing()
    {
        // アニメーション更新
        playerAnim.SetBool("isCrouching", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isWalking", false);
    }
    public void Crouching(int input)
    {
        // アニメーション更新
        playerAnim.SetBool("isWalking", false);
        playerAnim.SetBool("isJumping", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isCrouching", true);

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
        else if(input == 2)
        {
            Debug.Log("しゃがみ");
        }
    }
    public void Jumping(int input)
    {
        // アニメーション更新
        playerAnim.SetBool("isCrouching", false);
        playerAnim.SetBool("isWalking", false);
        playerAnim.SetBool("isJumpAttacking", false);
        playerAnim.SetBool("isJumping", true);

        // 前ジャンプ
        if (input == 9)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(5, 12), ForceMode2D.Impulse);
            isGrounded = false;
        }
        // 後ろジャンプ
        else if (input == 7)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-5, 12), ForceMode2D.Impulse);
            isGrounded = false;
        }
        // 垂直ジャンプ
        else if (input == 8)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }
    public void JumpAttack()
    {
        // アニメーション更新
        playerAnim.SetBool("isJumpAttacking", true);

        // 攻撃
    }
}
