using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIMangerScript : MonoBehaviour,IPauseRestart
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private Text notificationText;
    [SerializeField]
    private Animation notificationAnimation;
    [SerializeField]
    private Animation countDownAnimation;

    [SerializeField]
    private Button[] PlayingButtons;

    public bool showScore;

    [SerializeField]
    private Animation FlashScreenAnimation;

    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private GameObject videoProp;

    [SerializeField]
    private Image progressBar;
    private int maxProgress;
    public int MaxProgress { set { maxProgress = value; } }
    private int startProgress;
    public int StartProgress { set { startProgress = value; } }

  
    // Use this for initialization
    void Start ()
    {
        if (!showScore)
        {
            scoreText.text = "Tutorial";
        }

        LevelManagerScript.Instance.LoosingEvent += Loosing;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(showScore)
            scoreText.text = scoreManager.Score.ToString(); 

        
	}
    

    public void NotifyLevelUp()
    {
        NotifyString("Level Up !");
        return;
    }

    public void NotifySpeedUp()
    {
        NotifyString("Speed Up !");
        return; 
    }

    public void NotifyString(string notificationMessage)
    {
        notificationText.text = notificationMessage;
        notificationAnimation.Play();

    }

    public IEnumerator CountDown()
    {
        countDownAnimation.Play();
        yield return new WaitUntil(() => !countDownAnimation.isPlaying);
        NotifyString("Go Go Go !");
        yield return null;
    }

    public void Pause()
    {
        foreach(Button btn in PlayingButtons)
        {
            btn.interactable = false;
        }
        pauseButton.interactable = false;
    }

    public void Restart()
    {
        foreach (Button btn in PlayingButtons)
        {
            btn.interactable = true;
        }
        pauseButton.interactable = true;
    }

    public void Loosing()
    {
        FlashScreenAnimation.Play();
        foreach (Button btn in PlayingButtons)
        {
            btn.interactable = false;
        }
        pauseButton.interactable = false;

    }

    public void ToggleGameObject(GameObject goToToggle)
    {
        if (goToToggle == null)
            return;

        goToToggle.SetActive(!goToToggle.activeSelf);

    }

    public void ToggleVideoProposition()
    {
        ToggleGameObject(videoProp);
    }

    public void ProgressBarUpdate(float currentProgress)
    {
       
        if (maxProgress == -1)
        {
            if (progressBar.fillAmount != 0)
            {
                progressBar.fillAmount = 0;
                progressBar.transform.parent.gameObject.SetActive(false); 
            }
            return;
        }

        progressBar.fillAmount =( currentProgress - startProgress )/ maxProgress;
    }

    
}
