using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    
    float fAmaount = 1200f;
    float LastShootTime;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public void Shoot()
    {
        BulletInstantiate();
    }

    public void Shoot(float shootTime)
    {
        if ((LastShootTime += Time.deltaTime) >= 1f/ shootTime)
        {
            LastShootTime = 0;
            BulletInstantiate();
            
        }
    }


    private void BulletInstantiate()
    {
        GameObject bullet_new = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet_new.GetComponent<Rigidbody>().AddForce(fAmaount * transform.forward);
    }

}
