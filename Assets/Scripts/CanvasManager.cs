using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public StateManager stateManager1, stateManager2;

    private Slider hP_Bar1, hP_Bar2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hP_Bar1 = transform.GetChild(0).GetComponent<Slider>();
        hP_Bar2 = transform.GetChild(1).GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        hP_Bar1.value = (float)stateManager1.HP / 100.0f;
        hP_Bar2.value = (float)stateManager2.HP / 100.0f;
    }
}
