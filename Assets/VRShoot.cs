using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRShoot : MonoBehaviour
{
    public SimpleShoot simpleShoot;
    public OVRInput.Button shootButton;

    private OVRGrabbable Grabbable;


    // Start is called before the first frame update
    void Start()
    {
        Grabbable = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(shootButton))
        {
            simpleShoot.StartShoot();
            Debug.Log("Shoot");
        }
    }
}
