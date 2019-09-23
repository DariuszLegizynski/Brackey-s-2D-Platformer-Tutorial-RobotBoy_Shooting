using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float fireRate = 0f;
    //for future use
    //public int ammoRate = 0;
    public float damage = 10f;
    public LayerMask notToHit;

    bool ifReloaded = true;

    private float timeToFire = 0;
    Transform muzle;

    // Start is called before the first frame update
    void Awake()
    {
        muzle = transform.Find("Muzle");
        
        if(muzle == null)
        {
            Debug.LogError("No muzle object found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GunFire();
    }

    void GunFire()
    {
        if (fireRate == 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                //Play sound "Gun Aim"

                if (Input.GetKeyUp(KeyCode.D) && ifReloaded == true)
                {
                    Shoot();
                }

                else if((Input.GetKeyUp(KeyCode.D) && ifReloaded == true))
                {
                    //PlaySound.EmptyMagazine("Click");
                    Debug.Log("Reload!");
                }

                else 
                {
                    Debug.LogError("Something wrong with the shooting logic!");
                }
            }

            else
            {
                //Play sound. no more aiming
                Debug.Log("Not aiming anymore");
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.D) && Time.time > timeToFire);
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }
}
