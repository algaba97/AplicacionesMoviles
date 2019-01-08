using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    GameManager gameManager;
   public  Text textrubies;
    int rubies;
    void Start()
    {
        gameManager = GameManager.getGM();
        rubies = gameManager.GetRubies();
        textrubies.text = "x " + rubies;
        
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
                rubies = gameManager.GetRubies();
                textrubies.text = "x " + rubies;

                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;

            case ShowResult.Skipped:
                gameManager.addRubies(1);
                rubies = gameManager.GetRubies();
                textrubies.text = "x " + rubies;
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
