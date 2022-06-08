
using HmsPlugin;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : SimpleMenu<GameOverMenu>
{
    [SerializeField]
    private TextMeshProUGUI bestScoreText;
    [SerializeField]
    private GameObject continueButton;

    private int continueClickCount;
    public const string PREF_BEST_SCORE = "BestScore";


    private bool rewarded = false;

    private void Start()
    {
       // transform.SetAsFirstSibling(); 
    }
     
    private void OnEnable()
    {
        bestScoreText.text = $"Best Score: {PlayerPrefs.GetInt(PREF_BEST_SCORE)}";
        MazeGameManager.runningGame = false;
        continueButton.SetActive(continueClickCount < 1);
    }

    public void OnContinueButtonClick()
    {
        continueClickCount++;

        HMSAdsKitManager.Instance.OnRewardAdCompleted = OnRewardAdCompleted;
        HMSAdsKitManager.Instance.OnRewardAdClosed = OnRewardAdClosed;
        HMSAdsKitManager.Instance.ShowRewardedAd();
    }

    private void OnRewardAdClosed()
    {
        Debug.Log("OnRewardAdClosed");
        if (rewarded)
        {
            Debug.Log("Rewarding user");
            //PlayerController.Instance.RestartPlayerFromContinue();
            rewarded = false;
            Hide();
        }
    }

    private void OnRewardAdCompleted()
    {
        Debug.Log("OnRewardAdCompleted");
        rewarded = true;
    }

    public void OnTryAgainClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
