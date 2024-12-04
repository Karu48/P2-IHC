using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit.SceneDecorator;
using UnityEngine;

public class VRShoot : MonoBehaviour
{
    public OVRInput.Button shootButton;
    public GameObject bulletPrefab;
    public float shootForce;

    public float timeBetweenShots;

    bool shooting;
    bool readyToShoot;

    public Transform firePoint;


    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(shootButton))
        {
            if (readyToShoot)
            {
                Shoot();
                shooting = true;
                readyToShoot = false;

                resetShot();
            }
        }
    }

    void Shoot()
    {
        Vector3 direction = firePoint.forward;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(direction.normalized * shootForce, ForceMode.Impulse);
    }

    void resetShot()
    {
        shooting = false;
        readyToShoot = true;
    }
}