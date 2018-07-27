using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour,IPauseRestart
{
   
   
    private float startTime;
    public int score;
    public int Score { get { return score; } }
    private float stopTime;
    private float stoppedTime;
    private Coroutine startCountingCoroutine; 
    [SerializeField]private int timeScoreRate; 

    

	// Use this for initialization
	void Start ()
    {
        stoppedTime = 0f;
        startTime = Time.time;
        stopTime = 0;
        score = 0;
        LevelManagerScript.Instance.LoosingEvent += StopCounting;

    }
	
	// Update is called once per frame
	void Update ()
    {
    }
    
    public IEnumerator IStartCounting()
    {
        while(true)
        {
            score = (int)((Time.time - startTime - stoppedTime) / timeScoreRate);
   
            yield return null;
        }
        
    }

    public void StopCounting()
    {
        if (startCountingCoroutine != null)
        {
            StopCoroutine(startCountingCoroutine);
            startCountingCoroutine = null;
        }

        stopTime = Time.time; 
    }

    public void StartCounting()
    {
        
     
        stoppedTime += (Time.time - stopTime);
        stopTime = 0;



        startCountingCoroutine = StartCoroutine(IStartCounting());

    }

    public void Pause()
    {
        StopCounting();
    }

    public void Restart()
    {
        StartCounting();
    }


    



    



}
