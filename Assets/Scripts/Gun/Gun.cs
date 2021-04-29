using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform Barrel;

    private int bulletsCountOnShoot;
    private float coolDown;
    private GameObject currentBullet;
    private float power;

    private PoolObjects<Bullet> pool;

    private float timer = 0;

    public void Initialize(GunConfiguration gun)
    {
        bulletsCountOnShoot = gun.BulletsCountOnShoot;
        coolDown = gun.CoolDown;
        currentBullet = gun.Bullet;
        power = gun.Power;

        timer = 0.0f;

        if (currentBullet == null)
        {
            Debug.LogError("Bullet object is missing.");
        }

        if (Barrel == null)
        {
            Debug.LogError("Barrel is missing.");
        }

        pool = new PoolObjects<Bullet>();
    }

    public void Shoot(float damage)
    {
        if (timer > 0 || currentBullet == null || Barrel == null) return;

        for (int i = 0; i < bulletsCountOnShoot; i++)
        {
            var bullet = pool.GetFreeObject();
            if(bullet == null)
            {
                bullet = Instantiate(currentBullet).GetComponent<Bullet>();
                pool.AddObject(bullet);
            }

            if (bullet != null)
            {
                bullet.transform.position = Barrel.position;
                bullet.transform.rotation = Barrel.rotation;
                bullet.Initialize(transform, bullet.transform.forward * power, damage);
            }
        }

        timer = coolDown;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
    }
}
