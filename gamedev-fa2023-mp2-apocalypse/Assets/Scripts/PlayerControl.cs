using BarthaSzabolcs.Tutorial_SpriteFlash;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 5;
    public Animator anim;
    public Collider2D exit;
    public GameObject mainCam;
    public GameObject rightAttack;
    public GameObject leftAttack;
    private float attackDisplacement = 0.6f;
    private bool canAttack;
    public bool canGetHit;
    public bool canMove;

    public GameObject[] hearts;
    
    public int health;

    public SimpleFlash simpleFlash;


    Scene scene;
    
    

    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canAttack = true;
        canGetHit = true;
        canMove = true;
        health = 5;
        

    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(scene.name == "Outside")
        {
            rightAttack.transform.position = transform.position + new Vector3(attackDisplacement, 0, 0);
            leftAttack.transform.position = transform.position + new Vector3(-attackDisplacement, 0, 0);
        }
        



        anim.SetFloat("Horizontal",movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        
        //checks if player is in open world scene and makes camera follow player if so
       
        if (scene.name == "Outside")
        {
            mainCam.transform.position = transform.position + new Vector3(0, 0, -10f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canAttack==true && scene.name == "Outside") 
        {
            if (movement.x >= 0) 
            {
                rightAttack.SetActive(true);
            }else if (movement.x < 0) 
            {
                leftAttack.SetActive(true);
            }
            canAttack = false;
            StartCoroutine(AttackDuration());
        }
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");

        }
    }

    private void FixedUpdate()
    {
        if (canMove == true)
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Exit")
        {
            if (scene.name == "StartingRoom")
            {
                SceneManager.LoadScene("Outside");
            }
            else if (scene.name == "Outside")
            {
                SceneManager.LoadScene("StartingRoom");
            }

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy" && canGetHit == true)
        {
            canGetHit = false;
            canMove = false;
            health -= 1;
            hearts[health].SetActive(false);

            StartCoroutine(HitCooldown());
            StartCoroutine(MoveCooldown());
        }
    }
    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(.2f);
        rightAttack.SetActive(false);
        leftAttack.SetActive(false);
        canAttack = true;
    }
    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(.5f);
        canGetHit = true;
    }
    IEnumerator MoveCooldown()
    {
        yield return new WaitForSeconds(.2f);
        canMove = true;
    }

}
