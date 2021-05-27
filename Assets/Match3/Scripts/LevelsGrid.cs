using Match3.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class LevelsGrid : MonoBehaviour
    {
        [SerializeField]
        GameObject levelBtnPrefab;

        public void ShowLevels(List<LevelConfig> _levelConfig, List<LevelState> _levelsStates)
        {
            for(int i = 0; i < _levelConfig.Count; i++)
            {
                var _levelBtn = Instantiate(levelBtnPrefab, transform).GetComponent<LevelBtn>();
                _levelBtn.Setup(i, _levelsStates[i]);
                _levelBtn.LevelPressed += OnLevelSelected;
            }
        }

        void OnLevelSelected(int _levelIndex)
        {
            Debug.Log("OnLevelSelected: "+ _levelIndex);

            LevelSelected.Invoke(_levelIndex);
        }

        public Action<int> LevelSelected { get; set; }
    }
}
