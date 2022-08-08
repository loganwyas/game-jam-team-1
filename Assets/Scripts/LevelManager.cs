using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;
    private int coins = 0;

    private void Awake()
    {
    }

    void Start()
    {
        // DontDestroyOnLoad(gameObject);
        // timeCounter.text = "Time: 03:00.00";
        timerGoing = false;    
        BeginTimer();
    }

    public void BeginTimer() 
    {
        timerGoing = true;
        elapsedTime =  45f;

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
                // TODO: End Game and Send score
               EndTimer();
               NewGame();
               break;
            }
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            // timeCounter.text = timePlayingStr;

            yield return null;
        }
    }

    private void NewGame(){
        coins = 0;
        Restart();
    }

    public void NextLevel(){
        coins += 1;
        // coinCounter.text = "Coins: " + coins.ToString();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
