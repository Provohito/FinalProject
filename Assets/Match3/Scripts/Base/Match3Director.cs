using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Match3.Core;

namespace Match3.Base
{
    public class Match3Director : AppDirector
    {
        private void Start()
        {
            GameInfo.Instance.Setup();

            SceneManager.LoadScene("Menu");
        }
    }
}
