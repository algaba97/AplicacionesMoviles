using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameManager.getGM();
        
    }
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }

    }
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                gameManager.addRubies(10);

                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;

            case ShowResult.Skipped:
                gameManager.addRubies(1);
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
