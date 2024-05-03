using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleAdMobScript : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitialAd;
    private GameManager gameManager;
    private RewardedAd rewardedAd;
    AdSize adSize = new AdSize(320, 50);

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        MobileAds.Initialize(initStatus => { LoadBanner(); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

        bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);

    }
    public void LoadBanner()
    {
        if(bannerView == null)
        {
            RequestBanner();
        }

        AdRequest request = new AdRequest();

        bannerView.LoadAd(request);
    }

    public void LoadInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;

                ShowInterstitialAd();
            });
    }

    public void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }

        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            gameManager.gamePaused = false;
        };
    }

    public void LoadReward()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";

        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;

                ShowRewardedAd();
            });
    }


    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {

                gameManager.RewardCoins();
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }


        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            gameManager.gamePaused = false;
        };
    }


}
