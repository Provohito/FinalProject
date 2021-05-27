using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.Core
{
    public abstract class BaseScreen : MonoBehaviour
    {
        Action<Type, string> exitAction;

        public virtual void Init(Action<Type, string> _exitAction)
        {
            Debug.Log(name + ": Init");
            exitAction = _exitAction;
        }
        public virtual void Show()
        {
            Debug.Log(name + ": Show");
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            Debug.Log(name + ": Hide");
            gameObject.SetActive(false);
        }

        

        protected virtual void Exit(string _exitCode)
        {
            exitAction.Invoke(GetType(), _exitCode);
        }

        public bool IsShow => gameObject.activeSelf;
    }
}
