using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.layer == 11)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
