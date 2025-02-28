using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    private GameObject hitbox, hitboxLP, hitboxMP, hitboxHP, hitboxLK, hitboxMK, hitboxHK;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        hitbox = transform.Find("HitBox_Original").gameObject;
        hitboxLP = transform.Find("HitBox_LP").gameObject;
        hitboxMP = transform.Find("HitBox_MP").gameObject;
    }

    private void Start()
    {
        ResetHitbox();
    }

    public void HitBox_MP()
    {
        hitbox.SetActive(false);
        hitboxMP.SetActive(true);
    }

    public void HitBox_LP()
    {
        hitbox.SetActive(false);
        hitboxLP.SetActive(true);
    }

    public void ResetHitbox()
    {
        hitbox.SetActive(true);
        hitboxMP.SetActive(false);
        hitboxLP.SetActive(false);
    }
}
