using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float fireRate = 0f;
    public float damage = 10f;
    public LayerMask notToHit;

    bool reloadNeeded;

    private float timeToFire = 0;
    Transform muzle;

    // Start is called before the first frame update
    void Awake()
    {
        muzle = transform.FindChild("Muzle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
