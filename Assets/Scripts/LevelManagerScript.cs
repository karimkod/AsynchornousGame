using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class LevelManagerScript : MonoBehaviour
{ 
    private static LevelManagerScript instance; 
    public static LevelManagerScript Instance { get { return instance; } }

    public delegate void EmptyDelegate();

    public event EmptyDelegate LoosingEvent;
    public event EmptyDelegate ChangeContextEvent;

    [SerializeField]
    private GameObject[] gameObjectsContainingIPauseRestart;
    private IPauseRestart[] ObjectToPause;  // All Objects that needs to pause

    public Transform leadPlayer;

    [SerializeField]
    private GameObject pausePanel; 

    public CubeMechanism[] PlayersBallMechanism;

    [SerializeField]
    private ScoreManager scoreManager; 

    //private CameraMovement mainCameraMovement;
    private GameObject obstacleTouched;
    private bool restarted; // if this level was already restarted when loosing, if yes we don't restart again.
    [SerializeField]
    private float timeToWait; 


    public bool isPaused; 
    [SerializeField]
    private UIMangerScript uiManagerScript;
    public UIMangerScript UiManagerScript { get { return uiManagerScript; } }

    [SerializeField]
    private LevelGenerator levelGenerator;

    public bool IsTutorial;


    // player initial speed 
    [SerializeField]
    private float initSpeed; 

    //This speed is the players and the camera speed too.

    public float GameSpeed
    {
        get
        {
            return gameSpeed;
        }

        set
        {
            gameSpeed = value;
            UpdateSpeed(value);
        }
    }

    [Space]
    [Tooltip("This speed is the player and camera speed")]
    private float gameSpeed;

    [Space]
    [Tooltip("This speed is the speed taking by the cameara and the players after loosing.")]
    [SerializeField]
    private float loosingSpeed; 
    public float LoosingSpeed { get { return loosingSpeed;} }
    

    

	void Awake ()
    {
        instance = this;
        
        PlayersBallMechanism = FindObjectsOfType<CubeMechanism>();
        //mainCameraMovement = Camera.main.GetComponent<CameraMovement>();
        StartCoroutine(PlayerTracking());

        ObjectToPause = new IPauseRestart[gameObjectsContainingIPauseRestart.Length];
        for (int i =0; i < gameObjectsContainingIPauseRestart.Length; i++)
                ObjectToPause[i] = gameObjectsContainingIPauseRestart[i].GetComponent<IPauseRestart>();

       

	}

    private void Start()
    {
        if (!IsTutorial)
            VideoAdMobScript.Instance.FireReward += RestartPlaying; 

        GameSpeed = initSpeed;
        Pausing();
        StartCoroutine(Play());



    }

    private void Update()
    {
        levelProgress = leadPlayer.position.y;

        if(!IsTutorial)
            uiManagerScript.ProgressBarUpdate(levelProgress);

        if (Input.GetButton("Cancel"))
        {
            if (IsTutorial)
            {
                LoadLevel(1);
            }else
            {
                if (!isPaused)
                {
                    if (!pausePanel.activeSelf)
                        uiManagerScript.ToggleGameObject(pausePanel);
                    Pausing();
                }
            }
           
        }
    }

    

    public void Loosing(GameObject obstacleTouched)
    {
        if (LoosingEvent != null)
            LoosingEvent();
        isPaused = true;
        if (!IsTutorial && !restarted)
        {
            if (VideoAdMobScript.Instance.IsVideoReady())
            {
                this.obstacleTouched = obstacleTouched;
                uiManagerScript.ToggleVideoProposition();


            }
            else
            {
               LoadLevelDelayed(5);
            }


        }
        else
        {
            LoadLevelDelayed(5);
            
        }
           
    }

    public void LoadLevel(int i)
    {
        if (i == 5)
        {
            DataManager.Instance.TemporaryScore = scoreManager.Score;
        }

        GameManagerScript.Instance.LoadLevel(i);
    }

    public void LoadLevelDelayed(int i)
    {
        StartCoroutine(LoadLevelCoroutine(i));
    }

    private IEnumerator LoadLevelCoroutine(int i)
    {
        yield return new WaitForSeconds(timeToWait); 
        if (i == 5)
        {
            DataManager.Instance.TemporaryScore = scoreManager.Score;
        }

        GameManagerScript.Instance.LoadLevel(i);

    }


    public void ShowRewardVideo()
    {
        VideoAdMobScript.Instance.ShowVideoAd(); 
    }

    public void RestartPlaying()
    {
        restarted = true;
        //Pausing();
        uiManagerScript.ToggleVideoProposition();
        RestartObjectsPosition(obstacleTouched.transform.position);
        levelGenerator.ReplaceWithEmpty(obstacleTouched);
        StartCoroutine(Play());
    }

    private void RestartObjectsPosition(Vector3 obstaclePosition)
    {
        foreach (CubeMechanism bl in PlayersBallMechanism)
        {
            bl.ResetPosition(obstaclePosition);
        }

        //mainCameraMovement.ResetPosition(obstaclePosition + new Vector3(0,49,-10));
    }

    public IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void ReloadLevelHigh()
    {
        StartCoroutine(ReloadLevel());
        return;
    }
    
    private void UpdateSpeed(float newSpeed)
    {
        foreach (CubeMechanism bl in PlayersBallMechanism)
        {
            bl.UpdateSpeed(newSpeed);
        }

        //mainCameraMovement.UpdateSpeed(newSpeed);
    }

    public void ChangingContext()
    {

        uiManagerScript.NotifyLevelUp();
        difficulty++;
        ReinitializeSpeed();
        if (ChangeContextEvent != null)
            ChangeContextEvent();
    }

    private void ReinitializeSpeed()
    {
        GameSpeed = PhasesDetails[difficulty].InitSpeed;
    }

    public void Pausing()
    {
        isPaused = true;
        foreach (IPauseRestart pr in ObjectToPause)
            pr.Pause();

    }

    public void Playing()
    {
        StartCoroutine(Play());
    }

    public void MuteRestore()
    {
        BackGroundMusicManager.Instance.MuteRestore();
    }

    private IEnumerator Play()
    {
        yield return StartCoroutine(uiManagerScript.CountDown());
        foreach (IPauseRestart pr in ObjectToPause)
            pr.Restart();
       
        isPaused = false;

    }


    #region DifficulyManagement // this region contains everything related to the difficulty management to the level.

    [Space]
    [Space]
    [Header("Level phases management")]

    [SerializeField] private PhaseDetail[] PhasesDetails;
    private float levelProgress;

    // speed by score management 
    [SerializeField]
    private float speedIncreaseAmount;

    private int difficulty;
    public int Difficulty { get { return difficulty; } }

    private IEnumerator PlayerTracking()
    {
        foreach(PhaseDetail phase in PhasesDetails)
        {
            uiManagerScript.MaxProgress = (int)phase.distanceToEndLevel; 

            //accessing One player transform and compare it to the barrier. 
            for (int i = 0; i < phase.distanceToSpeed.Length; i++)
            {
                yield return new WaitUntil(() => levelProgress >= phase.distanceToSpeed[i]);
                SpeedUp();
            }

            if (phase.distanceToEndLevel != -1)
            {
                yield return new WaitUntil(() => levelProgress >= phase.distanceToEndLevel);
                uiManagerScript.StartProgress = (int)phase.distanceToEndLevel; 
                ChangingContext();
            }

        }

        yield return null;
       


    }

    private void SpeedUp()
    {
        uiManagerScript.NotifySpeedUp();
        GameSpeed += speedIncreaseAmount;
        return;
    }

    public GameObject[] GetPathComponents()
    {
        return PhasesDetails[difficulty].pathComponents;
    }
    #endregion /DifficultyManagement

}
