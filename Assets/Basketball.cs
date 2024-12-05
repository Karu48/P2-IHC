using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Basketball : MonoBehaviour
{
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>().position;
    }

    void Update()
    {
        if (Vector3.Distance(pos, GetComponent<Transform>().position) > 20)
        {
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
