using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.Core;
using System;

namespace Match3.Base
{
    public class GameInfo : BaseManager<GameInfo>
    {
        [SerializeField]
        List<LevelConfig> levelsConfigs;

        public void Setup()
        {
            if (GetLevelState(0) == LevelState.Locked)
                SetLevelState(0, LevelState.Unlocked);
        }

        public List<LevelConfig> LevelsConfigs => levelsConfigs;

        public void RegisterResult(int _stepsRemaining, int _matchsRemaining)
        {
            LevelResultInfo = new LevelResultInfo();

            if (_matchsRemaining == 0)
                LevelResultInfo.Scores = GlobalConfig.SCORES_FOR_STEP * _stepsRemaining;

            Scores += LevelResultInfo.Scores;

            if(LevelResultInfo.Scores > 0)
            {
                int _nextLevelIndex = LevelIndex + 1;
                if(_nextLevelIndex < LevelsConfigs.Count &&
                    GetLevelState(_nextLevelIndex) == LevelState.Locked)
                {
                    SetLevelState(_nextLevelIndex, LevelState.NeedUnlock);
                }
            }

            PlayerPrefs.Save();
        }

        public LevelState GetLevelState(int _levelIndex)
        {
            return (LevelState)PlayerPrefs.GetInt(PrefsKeys.Level_ + _levelIndex);
        }

        public void SetLevelState(int _levelIndex, LevelState _levelState)
        {
            PlayerPrefs.SetInt(PrefsKeys.Level_ + _levelIndex, (int)_levelState);
        }
        public int Scores
        {
            get => PlayerPrefs.GetInt(PrefsKeys.Scores);

            set => PlayerPrefs.SetInt(PrefsKeys.Scores, value);
        }

        public int LevelIndex { get; set; }
        public LevelConfig LevelConfig => LevelsConfigs[LevelIndex];

        public LevelResultInfo LevelResultInfo { get; private set; }
    }

    public class LevelResultInfo
    {
        public int Scores { get; set; }
    }

    [Serializable]
    public class LevelConfig
    {
        [SerializeField]
        int stepsCount;
        [SerializeField]
        int matchCount;


        public int StepsCount => stepsCount;
        public int MatchCount => matchCount;

    }
}

