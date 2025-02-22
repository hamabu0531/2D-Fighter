using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;
    private StateManager stateManager1, stateManager2;
    private bool isLeftSide;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isLeftSide = transform.parent.parent.GetChild(1).GetComponent<StateManager>().isLeftSide;
        this.transform.SetParent(null);

        stateManager1 = GameObject.Find("Player1").transform.GetChild(1).GetComponent<StateManager>();
        stateManager2 = GameObject.Find("Player2").transform.GetChild(1).GetComponent<StateManager>();
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player1_HitBox"))
        {
            if (stateManager1.isGuarding || stateManager1.isParrying)
            {
                stateManager1.HP -= 0;
            }
            else
            {
                stateManager1.HP -= damage;
            }
        }
        else if(collision.gameObject.CompareTag("Player2_HitBox"))
        {
            if (stateManager2.isGuarding || stateManager2.isParrying)
            {
                stateManager2.HP -= 0;
            }
            else
            {
                stateManager2.HP -= damage;
            }
        }
        Destroy(gameObject);
    }
}
