using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    private StateManager stateManager1, stateManager2;
    private GameManager gameManager;

    private Slider hP_Bar1, hP_Bar2;
    private Text timerText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateManager1 = GameObject.Find("Player1").transform.Find("StateManager").GetComponent<StateManager>();
        stateManager2 = GameObject.Find("Player2").transform.Find("StateManager").GetComponent<StateManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        hP_Bar1 = GameObject.Find("HP_Bar1").GetComponent<Slider>();
        hP_Bar2 = GameObject.Find("HP_Bar2").GetComponent<Slider>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        hP_Bar1.value = (float)stateManager1.HP / 100.0f;
        hP_Bar2.value = (float)stateManager2.HP / 100.0f;
        timerText.text = (gameManager.remainFrame / 60).ToString();
    }
}
