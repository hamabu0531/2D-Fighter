using UnityEngine;

public class AttackBoxManager : MonoBehaviour
{
    public int damage;
    private StateManager stateManager1, stateManager2;

    public void Start()
    {
        stateManager1 = GameObject.Find("Player1").transform.Find("StateManager").GetComponent<StateManager>();
        stateManager2 = GameObject.Find("Player2").transform.Find("StateManager").GetComponent<StateManager>();
    }
    public void OnEnable()
    {
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        this.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // 攻撃相手がプレイヤー1の場合
        if (other.gameObject.CompareTag("Player1_HitBox"))
        {
            if(stateManager1.isGuarding || stateManager1.isParrying)
            {
                stateManager1.HP -= 0;
            }
            else
            {
                stateManager1.HP -= damage;
            }
        }

        // 攻撃相手がプレイヤー2の場合
        else if (other.gameObject.CompareTag("Player2_HitBox"))
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
    }
}
