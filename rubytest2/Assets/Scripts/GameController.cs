using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    public int currentLevel = 1;
    public int[] levelEnemies = {3,5,7,10};
    public int killCount = 0;
    public float[] levelPoints;
    //no lives just GameOver
    //public int playerLives = 3;
    private RubyController rController;
    public Text startLevelText;
    public GameObject[] enemyPrefabs;   
    public GameObject bossPrefab; 
    private bool wonGame = false;
    private bool lostGame = false;
    public AudioClip loseClip;
    private AudioSource playerAudio;    
    private int oldHealth = 5;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        int myEnemies = levelEnemies[this.currentLevel - 1];
        Debug.Log("Spawn enemies " + myEnemies.ToString());
        GameObject gObject = GameObject.FindGameObjectWithTag("Player");
        rController = gObject.GetComponent<RubyController>();        
        startLevelText.enabled = true;
        Invoke("SpawnEnemies", 2.0f);
    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded");
        startLevelText = GameObject.FindGameObjectWithTag("StartText").GetComponent<Text>();

        startLevelText.enabled = true;
        if(currentLevel == 1){
            startLevelText.text = "Level 1";
        }        
        else if(currentLevel == 2){
            startLevelText.text = "Level 2";
        }
        else if(currentLevel == 3){
            startLevelText.text = "Level 3";
        }
        else if(currentLevel == 4){
            startLevelText.text = "Level 4";
        }
        Invoke("SpawnEnemies", 2.0f);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IncrementLevel(){
        if(currentLevel == 1){
            killCount = 0;            
            oldHealth = rController.currentHealth;
            SceneManager.LoadScene("Level2");
        }
        else if(currentLevel == 2){
            killCount = 0;       
            oldHealth = rController.currentHealth;     
            SceneManager.LoadScene("Level3");
        }
        else if(currentLevel == 3){
            killCount = 0;            
            oldHealth = rController.currentHealth;
            SceneManager.LoadScene("FinalLevel");
        }        
        currentLevel++;        
    }    

    public void enemyDied(){
        Debug.Log("Enemy Died");
        killCount++;
        int myEnemies = levelEnemies[this.currentLevel - 1];
        if(killCount >= myEnemies){
            Debug.Log("All Enemies Dead - Next Level");
            IncrementLevel();
        }
    }

    public void playerDied(){
        //playerLives--;
        
        lostGame = true;
        killCount = 0;
        currentLevel = 1;
        SceneManager.LoadScene("GameOver");
    }

    void SpawnEnemies(){
        int eAmount = levelEnemies[this.currentLevel - 1];
        Tilemap tObject = GameObject.FindObjectsOfType<Tilemap>()[0];
        TilemapController tController = tObject.GetComponent<TilemapController>();
        //Debug.Log("Bounds222: " + tController.minX + " , " + tController.maxX);

        for(int x = 0 ; x < eAmount; x++){
            int enemyIndex = 0;
            float randomX = Random.Range(tController.minX, tController.maxX);
            float randomY = Random.Range(tController.minY, tController.maxY);

            if(enemyPrefabs.Length > 1){ 
                enemyIndex = Random.Range(0, enemyPrefabs.Length - 1);
            }            
            //Debug.Log("Random x " + randomX);
            //Debug.Log("Random y " + randomY);

            GameObject enemyObject = Instantiate(enemyPrefabs[enemyIndex], new Vector2(randomX, randomY), Quaternion.identity);
        }
        startLevelText.enabled = false;
        GameObject gObject = GameObject.FindGameObjectWithTag("Player");
        rController = gObject.GetComponent<RubyController>();
        if(oldHealth != 5){
            Debug.Log("Old health is " + oldHealth);
            int changeHealth = oldHealth - 5;
            //rController.currentHealth = oldHealth;
            rController.ChangeHealth(changeHealth);
        }


        //rController.ChangeHealth(0);
        
    }


    void Awake()
    {
        //only have one RubyController
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        
    }        
}
