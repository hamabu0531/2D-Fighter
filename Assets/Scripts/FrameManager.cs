using UnityEngine;

public class FrameManager : MonoBehaviour
{
    public int currentFrame = 0; // 現在のフレーム
    public int movableFrame = -1; // 動けるようになるフレーム(硬直の終わり)
    public int startupFrame = -1; // 攻撃発生フレーム

    private void Awake()
    {
        // フレームレートの設定
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentFrame++;
    }
}
