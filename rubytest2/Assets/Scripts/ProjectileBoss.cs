using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoss : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
        /*
        if(rigidbody2d.velocity.magnitude < 3.0){
            Destroy(gameObject);
        }
        */

    }

    void DeleteMe(){
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        Invoke("DeleteMe", 2.0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Velocity on hit: " + rigidbody2d.velocity.magnitude);


        //we also add a debug log to know what the projectile touch
        RubyController e = other.collider.GetComponent<RubyController>();
        if (e != null)
        {
            e.ChangeHealth(-1);
            Destroy(gameObject);
        }
        

        
    }
}
