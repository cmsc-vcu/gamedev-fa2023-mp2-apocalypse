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

    Scene scene;

    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        exit = GameObject.FindWithTag("Exit").GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        
        anim.SetFloat("Horizontal",movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        
        //checks if player is in open world scene and makes camera follow player if so
       
        if (scene.name == "World")
        {
            mainCam.transform.position = transform.position + new Vector3(0, 0, -10f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision = exit) 
        {
            if (scene.name == "StartingRoom")
            {
                SceneManager.LoadScene("World");
            }else if (scene.name == "World")
            {
                SceneManager.LoadScene("StartingRoom");
            }
            
        }
    }
}
