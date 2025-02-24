using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player1, player2;
    private float posX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posX = (player1.transform.position.x + player2.transform.position.x) / 2;
        transform.position = new Vector3(posX, 0, -10);
    }
}
