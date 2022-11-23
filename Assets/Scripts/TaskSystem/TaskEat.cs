using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TaskEat : Task
{
    public override void Process()
    {
        if (TaskCurrentProgress > 0)
        {
            TaskCurrentProgress--;
        }
        Invoker.Eat();
    }

    public override void Settlement()
    {
        throw new System.NotImplementedException();
    }

    public override void Terminate(bool success)
    {
        throw new System.NotImplementedException();
    }

    public override void Pause()
    {
        throw new System.NotImplementedException();
    }
}
