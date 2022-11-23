using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBase : Creature
{
    private Queue<Task> Tasks = new Queue<Task>();
    private Task currentTask;

    private void Awake()
    {
        //Init Behavior Params.
        Initialize();
    }

    private void Start()
    {
        Tasks.Enqueue(new TaskEat());
        StartCoroutine(Behave());
    }

    private void Update()
    {
        Tasks.TryPeek(out currentTask);
    }
    
    IEnumerator Behave()
    {
        while (lifeState != CreatureLifeState.Dead)
        {
            //自身属性值更新
            TryConsumeLivingProperties();
            //环境评估 （饥渴值、安全性）
            EnvironmentEvaluate();
            
            if (behaviorState != CreatureBehaviorState.Idle)
            {
                bool processSuccess = currentTask.TryProcess();
                if (Tasks.Count > 0)
                {
                    continue;
                }
                else
                {
                    behaviorState = CreatureBehaviorState.Idle;
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public override void EnvironmentEvaluate()
    {
        Debug.Log("Evaluate Env");
    }

    public override void Eat()
    {
        hungerVal = (hungerVal + 1 >= maxHungerVal) ? maxHungerVal : hungerVal + 1;
    }

    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override void Deliver()
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    public override void Ill()
    {
        throw new System.NotImplementedException();
    }

    public override void ReportTaskDone(Task task, bool success)
    {
        // normal task.
        if (task == currentTask)
        {
            if(success)
            {
                Tasks.Dequeue();
                currentTask.Settlement();
                Tasks.TryPeek(out currentTask);
                if (currentTask == null)
                {
                    behaviorState = CreatureBehaviorState.Idle;
                }
            }
            else
            {
                Tasks.Dequeue();
                Tasks.TryPeek(out currentTask);
                if (currentTask == null)
                {
                    behaviorState = CreatureBehaviorState.Idle;
                }
            }
        }
        // urgent task
        else
        {
            Tasks.TryPeek(out currentTask);
            if (currentTask == null)
            {
                behaviorState = CreatureBehaviorState.Idle;
            }
        }
    }

}
