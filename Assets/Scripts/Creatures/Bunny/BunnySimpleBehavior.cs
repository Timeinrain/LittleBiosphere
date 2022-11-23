using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnySimpleBehavior : MonoBehaviour
{
    public float JumpStepLength;
    public float JumpStepFrequency;
    public Vector2 TargetPoint;
    private bool Alive = true;
    void Start()
    {
        StartCoroutine("Behavior");
    }
    
    void Move()
    {
        Debug.Log("Move");
    }

    IEnumerator Behavior()
    {
        while (Alive)
        {
            Move();
            yield return new WaitForSeconds(JumpStepFrequency);
        }
    }
}
