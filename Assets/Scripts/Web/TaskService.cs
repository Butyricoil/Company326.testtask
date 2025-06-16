using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cifkor.Karpusha
{
    /// <summary>
    /// Manages a queue of tasks with priority support and type-based grouping.
    /// Ensures sequential execution of tasks while maintaining the ability to cancel and prioritize.
    /// </summary>
    public class TaskService
    {
        private ITask _currentTask;
        public ITask CurrentTask { get => _currentTask; }

        private List<ITask> _tasks = new List<ITask>();
        private Dictionary<RequestType, List<ITask>> _tasksByType = new Dictionary<RequestType, List<ITask>>();

        /// <summary>
        /// Adds a new task to the queue with optional priority and type.
        /// </summary>
        /// <param name="taskAction">The coroutine to execute</param>
        /// <param name="callback">Callback to be executed after task completion</param>
        /// <param name="isHighPriority">Whether this task should be prioritized in the queue</param>
        /// <param name="type">Optional type of the task for grouping and cancellation</param>
        public void AddTask(IEnumerator taskAction, Action callback, bool isHighPriority = false, RequestType? type = null)
        {
            var task = Task.Create(taskAction).Subscribe(callback);
            
            if (type.HasValue)
            {
                if (!_tasksByType.ContainsKey(type.Value))
                {
                    _tasksByType[type.Value] = new List<ITask>();
                }
                _tasksByType[type.Value].Add(task);
            }

            if (isHighPriority)
            {
                _tasks.Insert(0, task);
            }
            else
            {
                _tasks.Add(task);
            }

            if (_currentTask == null)
            {
                TaskQueueProcessing();
            }
        }

        /// <summary>
        /// Stops the currently executing task.
        /// </summary>
        public void StopCurrentTask()
        {
            if (_currentTask != null)
            {
                _currentTask.Stop();
                _currentTask = null;
            }
        }

        /// <summary>
        /// Cancels all tasks of a specific type.
        /// </summary>
        /// <param name="type">The type of tasks to cancel</param>
        public void CancelTasksByType(RequestType type)
        {
            if (_tasksByType.ContainsKey(type))
            {
                foreach (var task in _tasksByType[type])
                {
                    task.Stop();
                    _tasks.Remove(task);
                }
                _tasksByType[type].Clear();
            }
        }

        /// <summary>
        /// Resumes task queue processing after a pause.
        /// </summary>
        public void Restore()
        {
            TaskQueueProcessing();
        }

        /// <summary>
        /// Clears all pending tasks and stops the current task.
        /// </summary>
        public void Clear()
        {
            StopCurrentTask();
            foreach (var task in _tasks)
            {
                task.Stop();
            }
            _tasks.Clear();
            _tasksByType.Clear();
        }

        private ITask GetNextTask()
        {
            if (_tasks.Count > 0)
            {
                var returnValue = _tasks[0];
                _tasks.RemoveAt(0);

                if (returnValue.Type.HasValue && _tasksByType.ContainsKey(returnValue.Type.Value))
                {
                    _tasksByType[returnValue.Type.Value].Remove(returnValue);
                }

                return returnValue;
            }
            return null;
        }

        private void TaskQueueProcessing()
        {
            _currentTask = GetNextTask();

            if (_currentTask != null)
            {
                _currentTask.Subscribe(TaskQueueProcessing).Start();
            }
        }
    }
}
