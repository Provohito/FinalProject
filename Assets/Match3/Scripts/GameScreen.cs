using System.Collections;
using System.Collections.Generic;
using Match3.Base;
using Match3.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class GameScreen : BaseScreen
    {
        public const string Exit_Back = "Exit_Back";
        public const string Exit_EndGame = "Exit_EndGame";

        [SerializeField]
        TextMeshProUGUI stepsText;
        [SerializeField]
        TextMeshProUGUI matchesText;

        [SerializeField]
        TileGrid tileGrid;


        LevelConfig levelConfig;

        int stepsRemaining;
        int matchsRemaining;

        public void ShowAndStartGame()
        {
            Show();

            levelConfig = GameInfo.Instance.LevelConfig;
            stepsRemaining = levelConfig.StepsCount;
            matchsRemaining = levelConfig.MatchCount;

            RefreshInfo();

            tileGrid.Generate();
        }

        public void MakeStep()
        {
            stepsRemaining--;
            RefreshInfo();
            CheckForEndGame();
        }

        public void MakeMatch()
        {
            matchsRemaining--;
            RefreshInfo();
            CheckForEndGame();
        }

        void RefreshInfo()
        {
            stepsText.text = $"Steps: {stepsRemaining}";
            matchesText.text = $"Matches: {matchsRemaining}";
        }

        void CheckForEndGame()
        {
            if(stepsRemaining == 0 ||
                matchsRemaining == 0)
            {
                GameInfo.Instance.RegisterResult(stepsRemaining, matchsRemaining);
                Exit(Exit_EndGame);
            }
        }

        public void OnBackPressed()
        {
            Exit(Exit_Back);
        }
    }
}
