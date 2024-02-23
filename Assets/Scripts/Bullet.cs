using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] ParticleSystem hitPrefab;

    float _bulletLifetimeInSeconds = 4f;
    Rigidbody _rgbd;

    void Awake()
    {
        _rgbd = GetComponent<Rigidbody>();
    }

    public void Setup(Vector3 shootDir)
    {
        _rgbd.velocity = shootDir * bulletSpeed;
        Destroy(gameObject, _bulletLifetimeInSeconds);
    }

    void OnCollisionEnter(Collision collision)
    {

        print("Bullet!");
        if (collision.gameObject.CompareTag("Dragon"))
        {
            // Handle collision with dragon
            // Example: decrease dragon's health
            print("Bullet hit the dragon!");
            dragonHealth dragonHealth = collision.gameObject.GetComponent<dragonHealth>();
            if (dragonHealth != null)
            {
                int damageAmount = 50;
                dragonHealth.TakeDamage(damageAmount);
            }
        }

        Instantiate(hitPrefab, collision.GetContact(0).point, transform.rotation);
        Destroy(this.gameObject);
    }
}
