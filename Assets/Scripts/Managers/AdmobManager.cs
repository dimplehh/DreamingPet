using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using TMPro;

public class AdmobManager
{
    private List<string> deviceId = new List<string>();
    
    private void TestId()
    {
        deviceId.Add("52d9018a2fd6eefd");
    }
    /*
    // Start is called before the first frame update
    void Start()
    {
        // SDK 초기
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        //test 디바이스 추가.
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
            .SetTestDeviceIds(testId.DeviceId)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        LoadBannerAd();
        LoadFrontAd();
        LoadRewardAd();
    }
    */
    public void init()
    {
        TestId();
        // SDK 초기
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        //test 디바이스 추가.
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
            .SetTestDeviceIds(deviceId)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        LoadBannerAd();
        LoadFrontAd();
        LoadRewardAd();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        //버튼 활성화 조절
        //FrontAdsBtn.interactable = frontAd.CanShowAd();
        //RewardAdsBtn.interactable = rewardAd.CanShowAd();

    }
    */

    //배너광고
    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "ca-app-pub-7040385188716427/6712117500";


    BannerView bannerAd;

    void LoadBannerAd()
    {
        
        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        bannerAd = new BannerView(bannerTestID, adSize, AdPosition.Bottom); // test용이니 실제 적용할 때는 bannerID 넣자
        AdRequest request = new AdRequest.Builder().Build();
        bannerAd.LoadAd(request);
        //배너 끄고 키기 
        ToggleBannerAd(false);
    }

    public void ToggleBannerAd(bool a)
    {
        if (a) bannerAd.Show();
        else bannerAd.Hide();
    }
    
    //전면 광고
    const string frontTestID = "ca-app-pub-3940256099942544/1033173712";
    const string frontId = "ca-app-pub-7040385188716427/9836628816";
    InterstitialAd frontAd;

    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(frontTestID);

        AdRequest request = new AdRequest.Builder().Build();
        frontAd.LoadAd(request);

        frontAd.OnAdClosed += (sender, e) =>
        {
            //여기에 광고가 닫힌 후 추가 작업을 넣어주면 된다.
            
        };
    }
    public void ShowFrontAd()
    {
        frontAd.Show();
        LoadFrontAd();
    }

    //리워드 광고
    const string rewardTestID = "ca-app-pub-3940256099942544/5224354917";
    const string rewardId = "ca-app-pub-7040385188716427/4105241249";
    RewardedAd rewardAd;

    void LoadRewardAd()
    {
        rewardAd = new RewardedAd(rewardTestID);
        AdRequest request = new AdRequest.Builder().Build();
        rewardAd.LoadAd(request);
        
    }
    public void ShowRewardAd(GameObject player,GameObject bone, GameManager gm)
    {
        rewardAd.OnUserEarnedReward += (sender, e) =>
        {
            player.GetComponent<Player>().life++;
            player.SetActive(true);
            bone.SetActive(true);
            player.GetComponent<Player>().StartScore();
            player.GetComponent<Player>().StartInvicible();
            gm.UpdateLife(1);
            gm.Back.GetComponent<Background2>().enabled = true;
            gm.gameObject.GetComponent<LevelManager>().stop = false;
            gm.gameObject.GetComponent<LevelManager>().StopEnemy();
            gm.gameObject.GetComponent<LevelManager>().StopFever();
            gm.gameObject.GetComponent<LevelManager>().StopRain();
            gm.gameObject.GetComponent<LevelManager>().StopSpaceShip();
        };
        rewardAd.Show();
        gm.OverPanel.SetActive(false);
        LoadRewardAd();
    }

    
}


