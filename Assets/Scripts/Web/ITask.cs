using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cifkor.Karpusha
{
    public interface ITask
    {
        void Start();
        ITask Subscribe(Action feedback);
        void Stop();
    }
}
