﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public float fireRate = 0f;
    //for future use
    //public int ammoRate = 0;
    public int damage = 10;
    public LayerMask whatToHit;
    public LineRenderer lineRenderer;

    bool ifReloaded = true;

    private float timeToFire = 0;

    public Transform muzle;
    public GameObject hitEffect;
    public Transform muzleFlashPrefab;
    public GameObject weaponSmokePrefab;
    public GameObject muzzleSmokePrefab;

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
                WeaponFXEffects();
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

        RaycastHit2D hitInfo = Physics2D.Raycast(muzlePos, muzle.up, 100f, whatToHit);
        Debug.LogError("PifPaf!");

        if (hitInfo)
        {
            EnemyBehaviour enemy = hitInfo.transform.GetComponent<EnemyBehaviour>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            Instantiate(hitEffect, hitInfo.point, Quaternion.identity);

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

    void WeaponFXEffects()
    {
        Vector2 muzleRot = muzle.rotation.eulerAngles;
        muzleRot = new Vector2(muzle.rotation.x, muzle.rotation.y + 90);
        //muzle.rotation *= Quaternion.Euler(0, 90f, 0);
        //Transform cloneMuzleFlash = (Transform)Instantiate(muzleFlashPrefab, muzle.position, muzle.rotation);    //flash in the rightdirection, and stays there
        Transform cloneMuzleFlash = (Transform)Instantiate(muzleFlashPrefab, muzle.position, Quaternion.Euler(muzleRot));
        //cloneMuzleFlash.transform.parent = muzle;                 // <- muzzle flash not appearing after this line is in the code. After erase, the flash shows only in the right direction
        float size = Random.Range(1.6f, 1.9f);
        cloneMuzleFlash.transform.localScale = new Vector3(size, size / 2, size);
        Destroy(cloneMuzleFlash.gameObject, 0.02f);

        Instantiate(weaponSmokePrefab, transform.position, transform.rotation);

        Instantiate(muzzleSmokePrefab, muzle.position, muzle.rotation);
    }
}
