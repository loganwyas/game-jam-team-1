using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text coinCounter;

    public Text timeCounter;

    public GameObject leaderboardMenu;

    public GameObject leaderboardManager;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;
    private int coins = 0;
private int level;
    private void Awake()
    {
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        NewGame();
    }

    public void BeginTimer() 
    {
        timerGoing = true;
        elapsedTime =  180f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer() 
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer() 
    {
        while(timerGoing) 
        {
            if(elapsedTime <= 0){
               EndTimer();
            //    NewGame();
                leaderboardManager.GetComponent<Leaderboard>().SubmitScore(coins * 50);
                StartCoroutine(leaderboardManager.GetComponent<Leaderboard>().FetchTopHighscoresRoutine());
                leaderboardMenu.SetActive(true);
               break;
            }
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }

    private void NewGame(){
        timeCounter.text = "Time: 03:00.00";
        timerGoing = false;    
        BeginTimer();
        setCoins(0);
        LoadLevel(1);
    }

    private void setCoins(int number){
    coins = number;
    coinCounter.text = "Coins: " + coins.ToString();
    }

    public void NextLevel(){
        if(elapsedTime > 0){
            setCoins(coins + 1);
            Restart();
        }
    }

    private void Restart()
    {
      LoadLevel(1);
    }

        private void LoadLevel(int index)
    {
        level = index;

        Invoke(nameof(LoadScene), 1f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(level);
    }


}