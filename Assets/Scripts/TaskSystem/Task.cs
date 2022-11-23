using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Task
{
    [NotNull]
    public Creature Invoker;
    public Int16 TaskCurrentProgress;
    public Int16 TaskCompleteNeedSecondCnt;
    public Int16 TaskPriority = 1;
    private float _lastProcessTimer = 0;
    public virtual void Process()
    {
        if (TaskCurrentProgress > 0)
        {
            TaskCurrentProgress--;
        }
    }

    public virtual bool TryProcess()
    {
        _lastProcessTimer += Time.deltaTime;
        if (_lastProcessTimer >= 1)
        {
            _lastProcessTimer -= 1;
            Process();
            return true;
        }

        return false;
    }

    public abstract void Settlement();

    public virtual bool TryInterrupt(Int16 inPriority)
    {
        if (inPriority > TaskPriority)
        {
            Terminate(false);
            return true;
        }
        return false;
    }

    public virtual void Terminate(bool success)
    {
        Invoker.ReportTaskDone(this,success);
    }
    public abstract void Pause();
}
