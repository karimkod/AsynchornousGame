using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;


public class GooglePlayServices : MonoBehaviour
{
    private static GooglePlayServices instance; 
    public  static GooglePlayServices Instance { get { return instance; } }
        

    [SerializeField]
    private GameObject ShowError; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else
        {
            if (instance != this)
                Destroy(gameObject);
        }

    }


    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        AuthentificateToGooglePS();

    }

    private void AuthentificateToGooglePS()
    {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    GooglePlayServices.Instance.ReportBestScore(DataManager.Instance.PlayerData.BestScore);
                }
            });
    }


    public void ShowLeaderBoard()
    {
        if (Social.localUser.authenticated)
        {
        Social.ShowLeaderboardUI(); 
        }
        else
        {
            ShowError.SetActive(true);
        }
        

    }

    public void ReportBestScore(int newScore)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(newScore, GPGSIds.leaderboard_best_score, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }

}
