using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class VideoAdMobScript : MonoBehaviour
{
    private static VideoAdMobScript instance; 
    public static VideoAdMobScript Instance { get { return instance; } }

    private RewardBasedVideoAd rewardBasedVideoAd;
    public  delegate void EmptyDelegate();
    public event EmptyDelegate FireReward;
    private bool rewardBasedEventHandlersSet;
    private bool reward;


    // Use this for initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }


    }
    void Start ()
    {
        rewardBasedVideoAd = RewardBasedVideoAd.Instance;
        LoadVideoAd();
        reward = false; 
        if (!rewardBasedEventHandlersSet)
        {
            // Ad event fired when the rewarded video ad
            // has been received.
            rewardBasedVideoAd.OnAdLoaded += HandleRewardBasedVideoLoaded;
            // has failed to load.
            rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            // is opened.
            rewardBasedVideoAd.OnAdOpening += HandleRewardBasedVideoOpened;
            // has started playing.
            rewardBasedVideoAd.OnAdStarted += HandleRewardBasedVideoStarted;
            // has rewarded the user.
            rewardBasedVideoAd.OnAdRewarded += HandleRewardBasedVideoRewarded;
            // is closed.
            rewardBasedVideoAd.OnAdClosed += HandleRewardBasedVideoClosed;
            // is leaving the application.
            rewardBasedVideoAd.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

            rewardBasedEventHandlersSet = true;
        }
    }

    private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
    {
    }

    private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
    {
        if (!reward)
        {
            LevelManagerScript.Instance.LoadLevel(5);
            Debug.Log("Not getting reward");

        } else
        {
            Debug.Log("Getting Reward ! ");
            LevelManagerScript.Instance.RestartPlaying(); 
        }
      

        //LevelManagerScript.Instance.UiManagerScript.ToggleVideoProposition();

        LoadVideoAd();

    }

    private void HandleRewardBasedVideoRewarded(object sender, Reward e)
    {
        Debug.Log("Rewarded");
        reward = true;

    }

    private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
    {
        Debug.Log("Started");
    }

    private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
    {
        Debug.Log("Opened");
    }

    private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("Failed");
        LoadVideoAd();
    }

    private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
    {
        Debug.Log("Loaded");
        reward = false; 
    }

    public void LoadVideoAd()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-1295346188584165/4412561932";
#elif UNITY_IPHONE
        string adUnitId = "unused";
#else
        string adUnitId = "unexpected_platform";
#endif  

        //AddTestDevice("16A2F8B8BC30D1D3B1C37B0FC7E50C2B")
        rewardBasedVideoAd.LoadAd(new AdRequest.Builder().Build(), adUnitId);

    }
	
    public void ShowVideoAd()
    {
        if (rewardBasedVideoAd.IsLoaded())
            rewardBasedVideoAd.Show();
        else
            Debug.Log("The video is not load");
    }

  
    public bool IsVideoReady()
    {
        return rewardBasedVideoAd.IsLoaded();
    }
}
