using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class InterstitialAdMobScript : MonoBehaviour
{
    private static InterstitialAdMobScript instance;
    public static InterstitialAdMobScript Instance { get { return instance; } }

    InterstitialAd interstitial;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);

        }
        //Request Ad
        LoadInterstitialAd();
        interstitial.OnAdFailedToLoad += InterstitialOnAdFailedLoading;
        interstitial.OnAdClosed += InterstitialOnAdClosed;

    }

    public void ShowInterstitialAd(object sender,System.EventArgs args)
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();

        }
    }

    public bool IsLoaded()
    {
        return interstitial.IsLoaded();
    }


    private void LoadInterstitialAd()
    {
#if UNITY_EDITOR
        string adUnitID = "unused";

#elif UNITY_ANDROID
        string adUnitID = "ca-app-pub-1295346188584165/3618656332";
#elif UNITY_IOS
        string adUnitID = "unuesed";
#else
        string adUnitID = "unexpected_platform";
#endif

        interstitial = new InterstitialAd(adUnitID);

        AdRequest request = new AdRequest.Builder().Build(); // For release
        //AdRequest request = new AdRequest.Builder().AddTestDevice("16A2F8B8BC30D1D3B1C37B0FC7E50C2B").Build();
        interstitial.LoadAd(request);


    }

    //Ad Close Event
    private void InterstitialOnAdFailedLoading(object sender, AdFailedToLoadEventArgs e)
    {
        LoadInterstitialAd();
    }

    public void InterstitialOnAdClosed(object sender, System.EventArgs e)
    {
        LoadInterstitialAd();
    }

}