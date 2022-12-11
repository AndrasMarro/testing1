using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool salve = false;
    public bool weaponCoolDown = true;

    private bool wait;

    private void Start()
    {
        wait = weaponCoolDown;
    }

    // Update is called once per frame
    async void Update()
    {
        if (Input.GetButtonDown("Fire1") && wait)
        {
            Shoot();
            wait = !wait;
            if (salve)
                await Task.Delay(450);
            else
                await Task.Delay(300);
            wait = !wait;
        }
        else if (Input.GetButtonDown("Fire1") && !weaponCoolDown)
            Shoot();
    }

    async void Shoot ()
    {
        if (salve)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            await Task.Delay(150);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        }
        else
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
