using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    public GameObject spawner;
    public GameObject self;

    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Hit");
        spawner.GetComponent<TargetSpawner>().hitTarget(self);
        Destroy(other.gameObject);
    }

    public void hit()
    {
        spawner.GetComponent<TargetSpawner>().hitTarget(self);
    }

}
