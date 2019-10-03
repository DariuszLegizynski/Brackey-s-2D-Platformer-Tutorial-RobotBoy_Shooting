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
    public LineRenderer lineRenderer;

    bool ifReloaded = true;

    private float timeToFire = 0;

    Transform muzle;
    public GameObject bulletPrefab;

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
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("ifReloaded is " + ifReloaded);
                //Play sound "Gun Aim"
            }

            else if (Input.GetKeyUp(KeyCode.D) && ifReloaded == true)
            {
                StartCoroutine(Shoot());
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
                StartCoroutine(Shoot());
            }
        }

    }

    IEnumerator Shoot()
    {
        Vector2 muzlePos = new Vector2(muzle.transform.position.x, muzle.transform.position.y);

        Instantiate(bulletPrefab, muzle.position, muzle.rotation);

        RaycastHit2D hitInfo = Physics2D.Raycast(muzlePos, muzle.transform.up, 100f, whatToHit);
        Debug.LogError("PifPaf!");

        if (hitInfo.collider != null)
        {
            Debug.DrawRay(muzlePos, muzle.transform.up * 100f, Color.red);
            Debug.LogWarning("We hit " + hitInfo.collider.name + " and did " + damage + " damage!");

            lineRenderer.SetPosition(0, muzle.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }

        else
        {
            Debug.DrawRay(muzlePos, muzle.transform.up * 100f, Color.yellow);
            lineRenderer.SetPosition(0, muzle.position);
            lineRenderer.SetPosition(1, muzle.transform.up * 100f);
        }

        lineRenderer.enabled = true;

        //wait for one frame
        yield return new WaitForSeconds(0.02f);

        lineRenderer.enabled = false;
    }

    void Reload()
    {
        ifReloaded = true;
        Debug.Log("Reloading");
    }
}
