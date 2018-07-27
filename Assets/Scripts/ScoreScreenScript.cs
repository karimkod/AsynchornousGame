using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenScript : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text bestText;
    [SerializeField]
    private ParticleSystem bestScoreFest;


    void Start()
    {
        InitText();
        StartCoroutine(CheckingScore());
    }

    private IEnumerator CheckingScore()
    {
        if (DataManager.Instance.PlayerData.IsNewBest(DataManager.Instance.TemporaryScore))
        {
            GooglePlayServices.Instance.ReportBestScore(DataManager.Instance.TemporaryScore);
            DataManager.Instance.SaveData();
            scoreText.text = DataManager.Instance.TemporaryScore.ToString();
            bestText.text = DataManager.Instance.PlayerData.BestScore.ToString();
            yield return new WaitForSeconds(1);
            bestScoreFest.Play();
        }

        ShowAd();
    }

    private void InitText()
    {
        scoreText.text = DataManager.Instance.TemporaryScore.ToString();

        bestText.text = DataManager.Instance.PlayerData.BestScore.ToString();   
    }

    public void ShowAd()
    {
        InterstitialAdMobScript.Instance.ShowInterstitialAd(null,null);
    }
    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            LoadLevel(1);
        }
    }
    
    public void LoadLevel(int i)
    {
        GameManagerScript.Instance.LoadLevel(i);
    }

    public void ShowLeaderBoard()
    {
        GooglePlayServices.Instance.ShowLeaderBoard();
    }


}
