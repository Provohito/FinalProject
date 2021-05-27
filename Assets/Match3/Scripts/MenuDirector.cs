using System;
using System.Collections;
using System.Collections.Generic;
using Match3.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3
{
    public class MenuDirector : SceneDirector
    {

        protected override void Start()
        {
            base.Start();

            SetCurrentScreen<MenuScreen>().Show();
        }
        protected override void OnScreenExit(Type _screenType, string _exitCode)
        {
            if(_screenType == typeof(MenuScreen))
            {
                if (_exitCode == MenuScreen.Exit_Game)
                    SceneManager.LoadScene(Scenesids.Game);
            }

        }
    }
}
