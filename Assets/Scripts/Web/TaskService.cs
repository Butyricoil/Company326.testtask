using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cifkor.Karpusha
{
    public class TaskService
    {
        private ITask _currentTask;
        public ITask CurrentTask { get => _currentTask; }

        private List<ITask> _tasks = new List<ITask>();

        public void AddTask(IEnumerator taskAction, Action callback)
        {
            var task = Task.Create(taskAction).Subscribe(callback);
            ProcessingAddedTask(task);
        }

        public void StopCurrentTask()
        {
            if (_currentTask != null)
                _currentTask.Stop();
        }

        public void Restore()
        {
            TaskQueueProcessing();
        }

        public void Clear()
        {
            StopCurrentTask();
            _tasks.Clear();
        }

        private ITask GetNextTask()
        {
            if (_tasks.Count > 0)
            {
                var returnValue = _tasks[0];
                _tasks.RemoveAt(0);

                return returnValue;
            }
            else
                return null;
        }

        private void TaskQueueProcessing()
        {
            _currentTask = GetNextTask();

            if (_currentTask != null)
                _currentTask.Subscribe(TaskQueueProcessing).Start();
        }

        private void ProcessingAddedTask(ITask task)
        {
            _tasks.Add(task);

            if (_currentTask == null)
                TaskQueueProcessing();
        }        
    }
}
