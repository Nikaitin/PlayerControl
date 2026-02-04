using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackArea;

    private bool attacking = false;

    private float timeToAttack = 0.25f;
    private float timer = 0f;



    void Start()
    {
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                attackArea.SetActive(false);
            }
        }
    }

    private void Attack()
    {
        attacking = true;
        attackArea.SetActive(true);
    }
}
