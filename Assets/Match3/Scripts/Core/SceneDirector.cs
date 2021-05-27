using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Core
{
    public abstract class SceneDirector : MonoBehaviour
    {
        Dictionary<Type, BaseScreen> screenDict;
        Stack<BaseScreen> screensStack;

        protected abstract void OnScreenExit(Type _screenType, string _exitCode);


        protected virtual void Start()
        {
            screenDict = new Dictionary<Type, BaseScreen>();
            screensStack = new Stack<BaseScreen>();

            for (int i = 0; i < transform.childCount; i++)
            {
                var _screen = transform.GetChild(i).GetComponent<BaseScreen>();
                if (_screen.IsShow)
                    throw new Exception($"{_screen.name} must be disabled.");
                {
                    screenDict.Add(_screen.GetType(), _screen);
                    _screen.Init(OnScreenExit);
                }
                    
            }
        }

        protected T SetCurrentScreen<T>() where T : BaseScreen
        {
            BaseScreen _nextScreen = screenDict[typeof(T)];

            if (CurrentScreen)
            {
                CurrentScreen.Hide();
            }

            CurrentScreen = _nextScreen;

            return CurrentScreen as T;
        }

        protected BaseScreen CurrentScreen { get; private set; }
    }
}
