using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Transform parent;
    private float damage;

    private new Rigidbody rigidbody;
    private new Collider collider;

    public void Initialize(Transform parent, Vector3 force, float damage)
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        this.parent = parent;

        this.damage = damage;

        rigidbody.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (parent != null && collision.transform.tag == parent.tag)
        {
            Physics.IgnoreCollision(collider, collision.collider);
        }
        else
        {
            rigidbody.velocity = Vector3.zero;
            gameObject.SetActive(false);

            if (collision.transform.TryGetComponent<Player>(out var player))
            {
                player.Damage(damage);
            }
            else if (collision.transform.TryGetComponent<EnemyBase>(out var enemy))
            {
                enemy.Damage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(parent != null && other.tag != parent.tag)
        {
            rigidbody.velocity = Vector3.zero;
            gameObject.SetActive(false);

            if (other.transform.TryGetComponent<Player>(out var player))
            {
                player.Damage(damage);
            }
            else if (other.transform.TryGetComponent<EnemyBase>(out var enemy))
            {
                enemy.Damage(damage);
            }
        }
    }
}
