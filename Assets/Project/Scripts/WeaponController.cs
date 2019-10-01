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

    Transform muzle;
    //public Transform rifle;

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
        Vector2 muzlePos = new Vector2(muzle.transform.position.x, muzle.transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(muzlePos, muzle.transform.up, 100f, whatToHit);
        Debug.DrawRay(muzlePos, muzle.transform.up * 100f, Color.yellow);
        Debug.LogError("PifPaf!");

        if (hit.collider != null)
        {
            Debug.DrawRay(muzlePos, muzle.transform.up * 100f, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage!");
        }
    }

    void Reload()
    {
        ifReloaded = true;
        Debug.Log("Reloading");
    }
}
