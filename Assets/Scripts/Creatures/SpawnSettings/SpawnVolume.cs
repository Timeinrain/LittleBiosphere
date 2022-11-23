using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class SpawnVolume : MonoBehaviour
{
    public UnityEvent TriggerEvents;

    public Creature SpawnedCreature;

    public float DensityPerSquared100Unit = 5;
    // Start is called before the first frame update
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(gameObject.transform.position,gameObject.transform.localScale);
    }
    
}
