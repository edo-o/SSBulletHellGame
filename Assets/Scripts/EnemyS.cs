using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyS : MonoBehaviour


{
    public Transform player;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float fireRate = 1.0f;

   
    void Start()
    {
         player= GameObject.FindWithTag("Player").transform;

        InvokeRepeating("Shoot", 1.0f, fireRate);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //bullet.layer = LayerMask.NameToLayer("PlayerBullet");

        Vector2 direction = (player.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;


    }
   
}
