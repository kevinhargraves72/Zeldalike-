using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public int hitPoints = 3;
    private GameController gameController;
    private TilemapController tController;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    Animator animator;

    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        Tilemap tObject = GameObject.FindObjectsOfType<Tilemap>()[0];
        tController = tObject.GetComponent<TilemapController>();
        GameObject gObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gObject.GetComponent<GameController>();        
        //Debug.Log("Bounds:  " + tController.minX + " , " + tController.maxX);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            ChangeDirection();
            //direction = -direction;
            //timer = changeTime;
        }
        else if(transform.position.x < tController.minX || transform.position.x > tController.maxX || 
                transform.position.y < tController.minY || transform.position.y > tController.maxY){
            ChangeDirection();
        }

        if (!broken)
        {
            return;
        }
    }

    void ChangeDirection(){
        GameObject gObject = GameObject.FindGameObjectWithTag("Player");
        float playerX = gObject.transform.position.x;
        float playerY = gObject.transform.position.y;

        int xDir = 1;
        int yDir = 1;
        if(transform.position.x > playerX){
            xDir = -1;
        }
        if(transform.position.y > playerY){
            yDir = -1;
        }

        if(transform.position.x < tController.minX){
            xDir = 1;
        }
        if(transform.position.x > tController.maxX){
            xDir = -1;
        }
        if(transform.position.y < tController.minY){
            yDir = 1;
        }
        if(transform.position.y > tController.maxY){
            yDir = -1;
        }



        int rand = Random.Range(1, 10);
        if(rand < 5){
            vertical = true;
            direction = yDir;
        }else{
            vertical = false;
            direction = xDir;
        }
        timer = Random.Range(0.5f, 3.0f);

    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);

        if (!broken)
        {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            animator.SetTrigger("Attack");
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        gameController.enemyDied();
    }

    public void Hit(){
        hitPoints--;
        if(hitPoints <= 0){
            //kill the enemy, let game controller know
            gameController.enemyDied();
        }

    }

}
