using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetAxis("Horizontal") < 0)
        {
            rb.velocity = Vector3.zero;
        }
        
        rb.AddForce(new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0));

        rb.AddForce(new Vector3(0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime,0 ));*/

        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
        transform.position += new Vector3(0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0);

    }
}
