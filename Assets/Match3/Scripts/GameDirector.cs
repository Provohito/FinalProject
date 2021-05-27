using System;
using System.Collections;
using System.Collections.Generic;
using Match3.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class GameDirector : SceneDirector
    {
        protected override void Start()
        {
            base.Start();

            SetCurrentScreen<GameScreen>().ShowAndStartGame();
        }
        protected override void OnScreenExit(Type _screenType, string _exitCode)
        {
            if(_screenType == typeof(GameScreen))
            {
                 if (_exitCode == GameScreen.Exit_EndGame)
                    SetCurrentScreen<ResultsScreen>().Show();
                 else if(_exitCode == GameScreen.Exit_Back)
                    SceneManager.LoadScene(Scenesids.Menu);
            }
            else if (_screenType == typeof(ResultsScreen))
            {
                if (_exitCode == ResultsScreen.Exit_Menu)
                    SceneManager.LoadScene(Scenesids.Menu);
                else if (_exitCode == ResultsScreen.Exit_Replay)
                    SetCurrentScreen<GameScreen>().ShowAndStartGame();
            }
        }
    }
}
