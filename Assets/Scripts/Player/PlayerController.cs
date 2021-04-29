using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public InputBase inputBase { get; private set; }

    private new Rigidbody rigidbody;
    private Gun gun;

    private float moveSpeed;
    private float damage;

    private Transform target;

    private bool isInitialized = false;

    public void Inititalize(float moveSpeed, float damage)
    {
        this.moveSpeed = moveSpeed;
        this.damage = damage;

        rigidbody = GetComponent<Rigidbody>();

        inputBase = GetComponent<InputBase>();
        if (inputBase == null)
            Debug.LogError("Input component is missing.");

        gun = GetComponent<Gun>();
        if (gun == null)
            Debug.LogError("Gun component is missing.");

        isInitialized = true;
    }

    public void Stop()
    {
        rigidbody.Sleep();
        enabled = false;
    }

    private void Update()
    {
        if (isInitialized)
        {
            if (rigidbody != null && inputBase != null)
            {
                var direction = new Vector3(inputBase.MoveDirection().x, 0, inputBase.MoveDirection().y);
                rigidbody.velocity = direction * moveSpeed;
            }

            target = EnemyManager.GetNearestEnemy(transform.position);

            if (rigidbody.velocity.sqrMagnitude > 0)
            {
                transform.LookAt(transform.position + rigidbody.velocity.normalized, Vector3.up);
            }
            else if (target != null && !inputBase.IsInput())
            {
                transform.LookAt(target.position, Vector3.up);
                gun.Shoot(damage);
            }
            else if (target == null)
            {
                transform.LookAt(transform.position + Vector3.forward, Vector3.up);
            }
        }
    }
}
