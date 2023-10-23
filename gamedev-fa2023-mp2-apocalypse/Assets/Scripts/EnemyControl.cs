using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public bool alerted;
    public bool upDown;
    public float idleWalkSpeed;
    public float alertedWalkSpeed;
    private float distance;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        idleWalkSpeed = 1.5f;
        alertedWalkSpeed = 2f;
        alerted = false;
        StartCoroutine(BackAndForth());
    }

    // Update is called once per frame
    void Update()
    {
       distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        direction.Normalize();

       if (!alerted)
        {
            if (!upDown)
            {
                anim.SetTrigger("LeftToRight");
                transform.position += new Vector3(idleWalkSpeed * Time.deltaTime, 0, 0);
            }else if (upDown)
            {
                anim.SetTrigger("UpAndDown");
                transform.position += new Vector3(0, idleWalkSpeed * Time.deltaTime, 0);
            }
        }else if (alerted)
        {
            StopCoroutine(BackAndForth());
            anim.SetTrigger("Alerted");
            anim.SetFloat("X",direction.x);
            anim.SetFloat("Y",direction.y);
            transform.position = Vector2.MoveTowards(this.transform.position,player.transform.position, alertedWalkSpeed * Time.deltaTime);
        }
    }
    IEnumerator BackAndForth()
    {
        yield return new WaitForSeconds(5);
        anim.SetTrigger("SwapDirection");
        idleWalkSpeed = -idleWalkSpeed;
        StartCoroutine (BackAndForth());

    }
}
