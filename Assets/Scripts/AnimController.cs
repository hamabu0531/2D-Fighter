using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public StateManager stateManager;
    public SpriteRenderer playerSR;
    public Sprite[] playerSprites;
    public int[] framesPerSprite;
    private bool loop; // アニメーションのループ
    private Dictionary<string, AttackInfo> jsonData;
    private Coroutine currentAnimation;

    private void Start()
    {
        jsonData = GameObject.Find("JsonDB").GetComponent<JsonDB>().jsonData;
    }

    public void Idleing()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Idleing");

        // ループ
        loop = true;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = 7;// ここを個別にJSONで指定
        }

        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }
    public void Walking()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Walking");

        // ループ
        loop = true;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = 7;// ここを個別にJSONで指定
        }

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }
    public void Crouching()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Crouching");

        //ループ
        loop = false;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = 500;// ここを個別にJSONで指定
        }

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }
    public void Jumping()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Jumping");

        // ループ
        loop = false;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = 7;// ここを個別にJSONで指定
        }

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }

    public void Parrying()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Parrying");

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        playerSR.sprite = playerSprites[0];
    }

    // 弱パンチ

    public void Attack_LP()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Attack_LP");

        // ループ
        loop = false;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = jsonData["LP"].transitionFrames;// ここを個別にJSONで指定
        }

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }

    // 中パンチ

    public void Attack_MP()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Attack_MP");

        // ループ
        loop = false;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = jsonData["MP"].transitionFrames;// ここを個別にJSONで指定
        }

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }

    // 中キック
    public void Attack_MK()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/Attack_MK");

        // ループ
        loop = false;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = jsonData["MK"].transitionFrames;// ここを個別にJSONで指定
        }

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }

    // ジャンプ攻撃
    public void JumpAttack()
    {
        // playerSpritesを設定
        playerSprites = Resources.LoadAll<Sprite>("Sprites/JumpAttack");

        // ループ
        loop = false;

        // framesPerSpriteを設定
        framesPerSprite = new int[playerSprites.Length];
        for (int i = 0; i < playerSprites.Length; i++)
        {
            framesPerSprite[i] = jsonData["JumpAttack"].transitionFrames; // ここを個別にJSONで指定
        }

        // アニメーション更新
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
        }
        currentAnimation = StartCoroutine(ChangeAnimation(playerSprites, framesPerSprite, loop));
    }

    private IEnumerator ChangeAnimation(Sprite[] sprites, int[] framesPerSprite, bool loop)
    {
        while (true)
        {
            for (int i=0; i < sprites.Length; i++)
            {
                playerSR.sprite = sprites[i];

                // 60fpsに対応
                for (int j = 0; j < framesPerSprite[i]; j++)
                {
                    yield return new WaitForEndOfFrame();
                }
            }
            if (!loop)
            {
                break;
            }
        }
    }
}
