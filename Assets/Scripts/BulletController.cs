using UnityEngine;

public class BulletController : MonoBehaviour
{
    private bool isLeftSide;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLeftSide = transform.parent.parent.GetChild(1).GetComponent<StateManager>().isLeftSide;
        this.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeftSide)
        {
            transform.position += Vector3.right * 8 * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * 8 * Time.deltaTime;
        }
        
        if (transform.position.x > 50)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.GetChild(1).GetComponent<StateManager>().HP -= 10;
        }
        Destroy(gameObject);
    }
}
