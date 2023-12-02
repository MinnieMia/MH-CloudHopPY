using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   
    private Spawner spawner;
    public GameObject title;
    private Vector2 screenBounds;
    public GameObject playerPrefab;
    private GameObject player;
    private bool gameStarted = false;
    public GameObject splash;
    public GameObject scoreSystem;
    public Text scoreText;
    public int pointsWorth = 1;
    private int score;
    // Start is called before the first frame update

 private void OnCollisionEnter(Collision Collision)
    {
        Destroy(gameObject);
    }

    void Awake()
    {
       spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
       screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
       player = playerPrefab;
       scoreText.enabled = false;
    }
    // Update is called once per frame
    void Start()
    {
        spawner.active = false;
        title.SetActive(true);
        splash.SetActive(false);
    }
    void Update()
    {
      if (!gameStarted)
      {
        if(Input.anyKeyDown)
        {
            ResetGame();
        }
      }else{
            if (!player)
            {
                OnPlayerKilled();
            }
        }
        var nextBomb = GameObject.FindGameObjectsWithTag("Bomb");
        foreach (GameObject bombObject in nextBomb)
        {
            if(!gameStarted)
            {
                Destroy(bombObject);
            } else if (bombObject.transform.position.y < (-screenBounds.y) && gameStarted)
            { 
                scoreSystem.GetComponent<Score>().AddScore(pointsWorth);
                Destroy(bombObject);
            }
            
        }
    }


    void ResetGame()
    {
        spawner.active = true;
        title.SetActive(false);
        player = Instantiate(playerPrefab, new Vector3(0,0,0), playerPrefab.transform.rotation);
        splash.SetActive(false);
        gameStarted = true;

        scoreText.enabled = true;
        scoreSystem.GetComponent<Score>().score = 0;
        scoreSystem.GetComponent<Score>().Start();
    }
    void OnPlayerKilled()
    {
        spawner.active = false;
        splash.SetActive(true);
        gameStarted = false;
        score = scoreSystem.GetComponent<Score>().score;

    }
}
