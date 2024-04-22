using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPlayScript : MonoBehaviour
{
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        string appId = "1e42a9565";

        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(appId);
        LoadBannerAd();
    }


    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void OnEnable()
    {

        //IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;
        IronSourceInterstitialEvents.onAdReadyEvent += ShowInterAd;

        IronSourceRewardedVideoEvents.onAdReadyEvent += RewardedVideoAdReadyEvent;
        IronSourceRewardedVideoEvents.onAdLoadFailedEvent += RewardedVideoAdLoadFailedEvent;
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoAdClickedEvent;

    }

    private void RewardedVideoAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo info)
    {
        throw new NotImplementedException();
    }

    private void RewardedVideoAdShowFailedEvent(IronSourceError error, IronSourceAdInfo info)
    {
        throw new NotImplementedException();
    }

    private void RewardedVideoAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo info)
    {
        gameManager.IncreaseScore(555);
    }

    private void RewardedVideoAdOpenedEvent(IronSourceAdInfo info)
    {
        throw new NotImplementedException();
    }

    private void RewardedVideoAdLoadFailedEvent(IronSourceError error)
    {
        throw new NotImplementedException();
    }

    private void RewardedVideoAdReadyEvent(IronSourceAdInfo info)
    {
        throw new NotImplementedException();
    }

    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }

    public void LoadBannerAd()
    {

        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.TOP);
    }

    public void ShowRewardAd()
    {
        if(IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
        
    }

    public void LoadInterAd()
    {
        Debug.Log("unity-script: LoadInterstitialButtonClicked");
        IronSource.Agent.loadInterstitial();

        // Register callback for when the interstitial ad is loaded
        
    }

    public void ShowInterAd(IronSourceAdInfo adInfo)
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
        else
        {
            Debug.Log("unity-script: IronSource.Agent.isInterstitialReady - False");
        }
    }

    private void SdkInitializationCompletedEvent() 
    {
        Debug.Log("unity-script: I got SdkInitializationCompletedEvent");
    }

    /************* Banner AdInfo Delegates *************/
    //Invoked once the banner has loaded
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("BannerOnAdLoadedEvent");
    }
    //Invoked when the banner loading process has failed.
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
    {

    }
    // Invoked when end user clicks on the banner ad
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Invoked when the user leaves the app
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
    {
    }

    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdReadyEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
    {
        Debug.Log("unity-script: I got InterstitialOnAdLoadFailed With Error " + ironSourceError);
    }

    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdOpenedEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClickedEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdShowSucceededEvent With AdInfo " + adInfo);
    }

    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdShowFailedEvent With Error " + ironSourceError + " And AdInfo " + adInfo);
    }

    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got InterstitialOnAdClosedEvent With AdInfo " + adInfo);
        gameManager.gamePaused = false;
    }

}
