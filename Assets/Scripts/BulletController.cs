using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * 8 * Time.deltaTime;
        transform.Rotate(0, 0, -10);
        if (transform.position.x > 50)
        {
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
