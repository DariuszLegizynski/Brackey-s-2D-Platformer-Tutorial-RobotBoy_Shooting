using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float fireRate = 0f;
    //for future use
    //public int ammoRate = 0;
    public float damage = 10f;
    public LayerMask whatToHit;

    bool ifReloaded = true;

    private float timeToFire = 0;
    Transform muzzle;

    // Start is called before the first frame update
    void Awake()
    {
        muzzle = transform.Find("Muzle");
        
        if(muzzle == null)
        {
            Debug.LogError("No muzle object found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        GunFire();

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void GunFire()
    {
        if (fireRate == 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                Debug.Log("ifReloaded is " + ifReloaded);
                //Play sound "Gun Aim"
            }

            else if (Input.GetKeyUp(KeyCode.D) && ifReloaded == true)
            {
                Shoot();
                ifReloaded = false;
            }

            else if ((Input.GetKeyUp(KeyCode.D) && ifReloaded == false))
            {
                //PlaySound.EmptyMagazine("Click");
                Debug.Log("Reload needed!");
            }

            /*
            else
            {
                //Play sound. no more aiming
                Debug.Log("Not aiming anymore");
            }
            */
        }

        else
        {
            if (Input.GetKey(KeyCode.D) && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }

    }

    void Shoot()
    {
        Vector2 muzzle = new Vector2(transform.position.x, transform.position.y);
        Vector2 rifle = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector2 rayDir = rifle + muzzle;
        //Vector2 muzzle = transform.position;
        //Vector2 muzzle = new Vector2(transform.position.x, transform.position.y);
        //muzzle.transform.SetParent(transform.parent);
        //muzzle.transform.position = transform.position;
        //Vector2 muzzle = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(muzzle, rayDir, 100f, whatToHit);
        Debug.DrawLine(muzzle, rayDir * 100f, Color.cyan);
        Debug.LogError("PifPaf!");
        Debug.Log("rayDir " + rayDir);
        Debug.Log("muzzle " + muzzle);
        Debug.Log("rifle " + rifle);
        

        if (hit.collider != null)
        {
            Debug.DrawRay(muzzle, rayDir * 100f, Color.red);
        }
    }

    void Reload()
    {
        ifReloaded = true;
        Debug.Log("Reloading");
    }
}
