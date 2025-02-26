using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    public int roundTime = 99;
    public int remainFrame;
    public int currentFrame = 0;
    private bool isPlaying = false;

    private void Awake()
    {
        // フレームレートの設定
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        remainFrame = roundTime * 60;
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame++;

        // 3秒後に開始
        if (currentFrame == 180)
        {
            ChangePlaying();
        }

        if (isPlaying)
        {
            remainFrame--;
            if (remainFrame < 0)
            {
                remainFrame = 0;
                ChangePlaying();
            }
        }
    }

    public void ChangePlaying()
    {
        isPlaying = !isPlaying;
        Debug.Log("isPlaying: " + isPlaying);
    }
}
