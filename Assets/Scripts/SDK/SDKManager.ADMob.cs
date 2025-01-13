using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public partial class SDKManager //ADMob
{

#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string _adUnitId = "unused";
#endif

    RewardedAd rewardedAd = null;

    Action<AdValue> adPaid = null;
    Action adImpressionRecorded = null;
    Action adClicked = null;
    Action adFullScreenContentOpened = null;
    Action adFullScreenContentClosed = null;
    Action adFullScreenContentFailed = null;

    public void ADMobInit()
    {
        MobileAds.Initialize(InitComplete);
    }

    void InitComplete(InitializationStatus initStatus)
    {
        Debug.Log("ADMob Initialize Complete");
        LoadRewardedAD();
    }

    public void LoadRewardedAD()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        var adRequest = new AdRequest();

        RewardedAd.Load(_adUnitId, adRequest, ADLoadCallBack);
    }

    void ADLoadCallBack(RewardedAd ad,LoadAdError error)
    {
        if (error != null || ad == null)
        {
            Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
            return;
        }

        Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

        rewardedAd = ad;
        RegisterEventHandlers(rewardedAd);
    }

    public bool ShowRewardedAD()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show(ShowCallback);
            return true;
        }
        else
        {
            LoadRewardedAD();
            return false;
        }
    }

    void ShowCallback(Reward reward)
    {
        const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
    }

    void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",adValue.Value,adValue.CurrencyCode));
            adPaid?.Invoke(adValue);
        };

        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
            adImpressionRecorded?.Invoke();
        };

        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
            adClicked?.Invoke();
        };

        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
            adFullScreenContentOpened?.Invoke();
        };

        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            adFullScreenContentClosed?.Invoke();
            LoadRewardedAD();
        };

        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            adFullScreenContentFailed?.Invoke();
            LoadRewardedAD();
        };
    }
}
