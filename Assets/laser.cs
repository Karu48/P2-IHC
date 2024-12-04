using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] 
    private Transform startPoint;

    // Update is called once per frame
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        lr.SetPosition(1, startPoint.position + startPoint.forward * 5000);

    }
}
