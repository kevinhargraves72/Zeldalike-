using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class BossController : MonoBehaviour
{
    public float changeTime = 3.0f;
    private GameController gameController;
    private TilemapController tController;

    public float timeInvincible = 2.0f;
    public GameObject bossProjectile;
    bool isInvincible;
    float invincibleTimer;  
    float bulletDelay = 3.0f;  

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    public int hitPoints = 10;

    Animator animator;

    bool broken = true;
    private IEnumerator spawner;

    // Start is called before the first frame update
    void Start()
    {
        timer = changeTime;
        animator = GetComponent<Animator>();
        GameObject gObject = GameObject.FindGameObjectWithTag("GameController");
        gameController = gObject.GetComponent<GameController>();               
    }

    void Update(){
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    public void startBulletSpawner(){
        spawner = SpawnBullets();
        StartCoroutine(spawner);
    }

    public void stopBulletSpawner(){
        StopCoroutine(spawner);
    }    

    IEnumerator SpawnBullets(){
        while(true){
            yield return new WaitForSeconds(bulletDelay);
            bulletDelay = Random.Range(0.01f, 0.3f);            
            Vector2 bulletStart = new Vector2(transform.position.x, transform.position.y - 2.0f);    
            GameObject myBullet = Instantiate(bossProjectile, bulletStart, transform.rotation);            
            ProjectileBoss myProjectile = myBullet.GetComponent<ProjectileBoss>();

            Vector2 randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 0.0f));
            randomDirection.Normalize();
            //Normalize();
            myProjectile.Launch(randomDirection, 300);
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

    public void Hit(){
        if (isInvincible)
            return;
        isInvincible = true;
        invincibleTimer = timeInvincible;

        animator.SetTrigger("Fixed");
        hitPoints--;
        Debug.Log("Boss got hit, points now: " + hitPoints);
        if(hitPoints <= 0){
            //kill the enemy, let game controller know
            gameController.bossDied();
        }

    }

}
