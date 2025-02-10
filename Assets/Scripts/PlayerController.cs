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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = moveInput.ReadValue<Vector2>();



        // 攻撃
        if (attackLKInput.triggered)
        {
            Debug.Log("弱キック");
        }
        if(attackMKInput.triggered)
        {
            Debug.Log("中キック");
        }
        if(attackLPInput.triggered)
        {
            Debug.Log("弱パンチ");
        }
        if (attackMPInput.triggered)
        {
            Debug.Log("中パンチ");
        }

        // 移動
        float x = move.x;
        float y = move.y;

        // ジャンプ
        if (y > 0.5f && isGrounded)
        {
            // 前ジャンプ
            if (x > 0.5f)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
                isGrounded = false;
            }
            // 後ろジャンプ
            else if (x < -0.5f)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(-10, 10), ForceMode2D.Impulse);
                isGrounded = false;
            }
            // 垂直ジャンプ
            else
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
        // しゃがみ
        else if (y < -0.5f && isGrounded)
        {
            // 前しゃがみ
            if (x > 0.5f)
            {
                Debug.Log("前しゃがみ");
            }
            // 後ろしゃがみ
            else if (x < -0.5f)
            {
                Debug.Log("後ろしゃがみ");
            }
            // しゃがみ
            else
            {
                Debug.Log("しゃがみ");
            }
        }
        // 横移動
        else if(x != 0 && isGrounded)
        {
            transform.Translate(new Vector3(x * speed, 0, 0) * Time.deltaTime);
        }

        Debug.Log("x: " + x + " y: " + y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
