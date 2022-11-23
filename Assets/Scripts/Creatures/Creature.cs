using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum CreatureLifeState
{
    Dead = 0,
    Alive = 1,
    Ill = 2,
}

public enum CreatureBehaviorState
{
    Idle,
    Moving,
    Eating,
    Drinking,
    Delivering,
    Dying,
}

public enum CreatureTypes
{
    Bunny,
    Wolf,
}

public abstract class Creature : MonoBehaviour
{
    public struct FoodPreference
    {
        public CreatureTypes TargetCreature;
        public float PreferenceWeight;
    }

    #region Properties

    public CreatureLifeState lifeState;
    public CreatureBehaviorState behaviorState;
        
    public float health;
    public float maxHealth;

    public float hungerVal;
    public float hungerConsumeRatePerSecond;
    public float maxHungerVal;

    public float thirstVal;
    public float thirstConsumeRatePerSecond;
    public float maxThirstVal;

    public float age;
    public float maxAge;

    //需要的时间为原先的多少倍
    public float illBehaviorTimeMultiplier = 2.0f;
    
    //植食性
    public bool herbivore = true;
    
    public List<FoodPreference> FoodTypes = new List<FoodPreference>();

    public float moveSpeed;
    public float runSpeed;

    public float sightRadius;
    public float sightHalfAngle;

    public float hearingRadius;

    #endregion

    #region Behaviors

    public abstract void EnvironmentEvaluate();
    public abstract void Eat();
    public abstract void Move();
    public abstract void Deliver();
    public abstract void Die();
    public abstract void Ill();

    public abstract void ReportTaskDone(Task task,bool success);

    public virtual void ConsumeLivingProperties()
    {
        hungerVal -= hungerConsumeRatePerSecond;
        thirstVal -= thirstConsumeRatePerSecond;
        // hard coded 100 seconds for 1 year
        age += 0.01f;

        if (hungerVal < 0|| thirstVal <0 || age>maxAge)
        {
            Debug.Log(this + "Died.");
            lifeState = CreatureLifeState.Dead;
        }
    }

    private float _consumeLivingPropertiesTimer = 0;
    public virtual void TryConsumeLivingProperties()
    {
        _consumeLivingPropertiesTimer += Time.deltaTime;
        if (_consumeLivingPropertiesTimer >= 1)
        {
            _consumeLivingPropertiesTimer -= 1;
            ConsumeLivingProperties();
        }
    }

    public virtual void Initialize()
    {
        lifeState = CreatureLifeState.Alive;
        behaviorState = CreatureBehaviorState.Idle;
        health = maxHealth = 100;
        hungerVal = maxHungerVal = 50;
        thirstVal = maxThirstVal = 50;
        hungerConsumeRatePerSecond = thirstConsumeRatePerSecond = 1;
        age = 0;
        maxAge = 100;
        FoodPreference bunnyCfg = new FoodPreference()
        {
            PreferenceWeight = 0.6f,
            TargetCreature = CreatureTypes.Bunny
        };
        FoodTypes.Add(bunnyCfg);
        moveSpeed = 3;
        runSpeed = 5;
        sightRadius = 15;
        sightHalfAngle = 60;//in degrees
        hearingRadius = 10;
    }
    
    #endregion

}

