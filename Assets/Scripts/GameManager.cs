using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private void Awake()
    {
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // NewGame();
        timeCounter.text = "Time: 03:00.00";
        timerGoing = false;    
        BeginTimer();
    }

    public void BeginTimer() 
    {
        timerGoing = true;
        elapsedTime =  30f;

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
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }

     private int level;

    private void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // private void LoadLevel(int index)
    // {
    //     level = index;

    //     Camera camera = Camera.main;

    //     // Don't render anything while loading the next scene to create
    //     // a simple scene transition effect
    //     if (camera != null) {
    //         camera.cullingMask = 0;
    //     }

    //     Invoke(nameof(LoadScene), 1f);
    // }

    // private void LoadScene()
    // {
    //     SceneManager.LoadScene(level);
    // }

    // public void LevelComplete()
    // {
    //     // score += 1000;

    //     int nextLevel = level;

    //     if (nextLevel < SceneManager.sceneCountInBuildSettings) {
    //         LoadLevel(nextLevel);
    //     } else {
    //         LoadLevel(1);
    //     }
    // }


}