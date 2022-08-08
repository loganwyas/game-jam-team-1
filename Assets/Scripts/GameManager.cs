using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        timeCounter.text = "Time: 03:00.00";
        timerGoing = false;    
        BeginTimer();
    }

    public void BeginTimer() 
    {
        timerGoing = true;
        elapsedTime = 2f;

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
               break;
            }
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
    

}