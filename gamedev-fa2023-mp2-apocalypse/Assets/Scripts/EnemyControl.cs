using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Rigidbody2D rb;
    public Rigidbody2D playerRB;
    public GameObject player;
    public GameObject AlertZone;
    public bool alerted;
    private bool canGetHit;
    public bool upDown;
    public float idleWalkSpeed;
    public float alertedWalkSpeed;
    private float distance;
    private int health;
    public Animator anim;
    Vector2 direction;

    public PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerRB =player.GetComponent<Rigidbody2D>();
        idleWalkSpeed = 1.5f;
        alertedWalkSpeed = 2f;
        health = 3;
        alerted = false;
        canGetHit = true;
        StartCoroutine(BackAndForth());
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        direction.Normalize();

        if (!alerted)
        {
            if (!upDown)
            {
                anim.SetTrigger("LeftToRight");
                transform.position += new Vector3(idleWalkSpeed * Time.deltaTime, 0, 0);
            }
            else if (upDown)
            {
                anim.SetTrigger("UpAndDown");
                transform.position += new Vector3(0, idleWalkSpeed * Time.deltaTime, 0);
            }
        }
        else if (alerted)
        {
            StopCoroutine(BackAndForth());
            anim.SetTrigger("Alerted");
            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, alertedWalkSpeed * Time.deltaTime);
            AlertZone.SetActive(false);
        }

        if (health <= 0)
        {
            playerControl.enemyCount -= 1;
            Destroy(gameObject);
            
        }
    }
    IEnumerator BackAndForth()
    {
        yield return new WaitForSeconds(5);
        anim.SetTrigger("SwapDirection");
        idleWalkSpeed = -idleWalkSpeed;
        StartCoroutine(BackAndForth());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack" && canGetHit == true)
        {
            health -= 1;
            canGetHit = false;
            rb.AddForce(-direction * 15f, ForceMode2D.Impulse);
            StartCoroutine(ResetVelocity());
            
            Debug.Log("Attack Hit");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && playerControl.canGetHit == true)
        {
            playerRB.AddForce(direction * 20f, ForceMode2D.Impulse);
            Debug.Log("Player Collision");



        }
    }
    IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(.2f);
        rb.velocity = Vector3.zero;
        canGetHit = true;
    }
    IEnumerator ResetPlayerVelocity()
    {
        yield return new WaitForSeconds(.2f);
        playerRB.velocity = Vector3.zero;
        
    }
}
