using System;
using System.Collections;
using UnityEngine;

namespace Cifkor.Karpusha
{
    /// <summary>
    /// Implementation of ITask that manages coroutine execution and resource cleanup.
    /// </summary>
    public class Task : ITask
    {
        private IEnumerator _taskAction;
        private Action _callback;
        private MonoBehaviour _coroutineRunner;
        private Coroutine _coroutine;
        public RequestType? Type { get; set; }

        /// <summary>
        /// Private constructor to enforce factory method pattern.
        /// </summary>
        /// <param name="taskAction">The coroutine to execute</param>
        private Task(IEnumerator taskAction)
        {
            _taskAction = taskAction;
            _coroutineRunner = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
            GameObject.DontDestroyOnLoad(_coroutineRunner.gameObject);
        }

        /// <summary>
        /// Factory method to create a new Task instance.
        /// </summary>
        /// <param name="taskAction">The coroutine to execute</param>
        /// <returns>A new Task instance</returns>
        public static Task Create(IEnumerator taskAction)
        {
            return new Task(taskAction);
        }

        /// <summary>
        /// Starts the execution of the task if it hasn't been started already.
        /// </summary>
        public void Start()
        {
            if (_coroutine == null)
            {
                _coroutine = _coroutineRunner.StartCoroutine(ExecuteTask());
            }
        }

        /// <summary>
        /// Stops the execution of the task and cleans up resources.
        /// </summary>
        public void Stop()
        {
            if (_coroutine != null)
            {
                _coroutineRunner.StopCoroutine(_coroutine);
                _coroutine = null;
            }

            if (_coroutineRunner != null)
            {
                GameObject.Destroy(_coroutineRunner.gameObject);
            }
        }

        /// <summary>
        /// Subscribes a callback to be executed when the task completes.
        /// </summary>
        /// <param name="callback">The callback to be executed</param>
        /// <returns>The task instance for method chaining</returns>
        public ITask Subscribe(Action callback)
        {
            _callback = callback;
            return this;
        }

        /// <summary>
        /// Executes the task and invokes the callback upon completion.
        /// </summary>
        private IEnumerator ExecuteTask()
        {
            yield return _taskAction;
            _callback?.Invoke();
        }

        /// <summary>
        /// Internal MonoBehaviour class for coroutine execution.
        /// </summary>
        private class CoroutineRunner : MonoBehaviour { }
    }
}

