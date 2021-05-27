using System.Collections;
using System.Collections.Generic;
using Match3.Base;
using Match3.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class ResultsScreen : BaseScreen
    {
        public const string Exit_Menu = "Exit_Menu";
        public const string Exit_Replay = "Exit_Replay";

        [SerializeField]
        TextMeshProUGUI scoresText;
        public override void Show()
        {
            base.Show();

            scoresText.text = "Scores: " + GameInfo.Instance.LevelResultInfo.Scores;
        }

        public void OnRestartPressed()
        {
            Exit(Exit_Replay);
        }

        public void OnMenuPressed()
        {
            Exit(Exit_Menu);
        }
    }
}
