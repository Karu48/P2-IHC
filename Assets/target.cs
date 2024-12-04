using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public GameObject spawner;
    public GameObject self;

    void OnTriggerEnter()
    {
        spawner.GetComponent<TargetSpawner>().hitTarget(self);
    }

}
