using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cifkor.Karpusha
{
    /// <summary>
    /// Represents a task that can be executed, stopped, and subscribed to for completion notification.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Starts the execution of the task.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the execution of the task and cleans up resources.
        /// </summary>
        void Stop();

        /// <summary>
        /// Subscribes a callback to be executed when the task completes.
        /// </summary>
        /// <param name="callback">The callback to be executed</param>
        /// <returns>The task instance for method chaining</returns>
        ITask Subscribe(Action callback);

        /// <summary>
        /// Gets or sets the type of the task for grouping and cancellation purposes.
        /// </summary>
        RequestType? Type { get; set; }
    }
}
