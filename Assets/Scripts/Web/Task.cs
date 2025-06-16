using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cifkor.Karpusha
{
    public class Task : ITask
    {
        private Action _feedBack;
        private CorutinesInScene _corutinesInScene;
        private Coroutine _coroutine;
        private IEnumerator _taskAction;

        public Task(IEnumerator taskAction)
        {
            _corutinesInScene = new CorutinesInScene();
            _taskAction = taskAction;
        }

        public static Task Create(IEnumerator taskAction)
        {
            return new Task(taskAction);
        }

        private void CallSubscribe()
        {
            if (_feedBack != null)
                _feedBack();
        }

        private IEnumerator RunTask()
        {
            yield return _taskAction;

            CallSubscribe();
        }

        public void Start()
        {
            if (_coroutine == null)
                _coroutine = _corutinesInScene.StartCoroutine(RunTask());
        }

        public void Stop()
        {
            if (_coroutine != null)
            {
                _corutinesInScene.StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        public ITask Subscribe(Action feedback)
        {
            _feedBack += feedback;

            return this;
        }
    }
}
