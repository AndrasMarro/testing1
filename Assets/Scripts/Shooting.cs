using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public int damage = 40;
    public bool weaponCoolDown = true;
    public GameObject impactEffect;
    public LineRenderer lineRenderer;

    private bool wait;


    private void Start()
    {
        wait = weaponCoolDown;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && wait)
        {
            Shoot();
            Delay();
        }
        else if (Input.GetButtonDown("Fire1") && !weaponCoolDown)
            Shoot();
    }

    private async void Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            HiddenWall hd = hitInfo.transform.GetComponent<HiddenWall>();
            if (enemy != null)
                enemy.takeDamage(damage);
            if (hd != null)
                hd.takeDamage(damage);
            Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        } else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
        }

        lineRenderer.enabled = true;
        await Task.Delay(17);
        lineRenderer.enabled = false;
    }

    private async void Delay()
    {
        wait = !wait;
        await Task.Delay(300);
        wait = !wait;
    }
}
