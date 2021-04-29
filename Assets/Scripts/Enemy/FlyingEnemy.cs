using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{
    private Gun gun;
    private float timer;
    private new Rigidbody rigidbody;

    private Transform player;

    private Vector3 pointToMove;
    private Vector3 previusPosition;
    private float traveledDistance;

    private bool isMoving;

    private bool isInitialized = false;

    public override void Initialize()
    {
        InitializeParams();

        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
            Debug.LogError("Rigidbody component is missing.");

        gun = GetComponent<Gun>();
        if (gun == null)
            Debug.LogError("Gun component is missing.");

        gun.Initialize(Configuration.GunConfiguration);

        if (GameManager.Player != null)
            player = GameManager.Player.transform;
        else
            Debug.LogError("Player object is missing.");

        timer = 0.0f;
        pointToMove = GameManager.Player.transform.position;
        previusPosition = transform.position;
        traveledDistance = 0.0f;
        isMoving = false;

        isInitialized = true;
    }

    protected override void Attack(Vector3 target)
    {
        gun.Shoot(damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialized)
        {
            if (timer <= 0.0f)
            {
                if (!isMoving && (player.position - transform.position).sqrMagnitude >= Mathf.Pow(maxMoveDistance, 2))
                {
                    pointToMove = player.position;
                    traveledDistance = 0.0f;
                    isMoving = true;
                    rigidbody.WakeUp();
                }
                if (isMoving)
                {
                    rigidbody.velocity = (pointToMove - transform.position).normalized * moveSpeed;
                    transform.rotation = Quaternion.LookRotation(rigidbody.velocity.normalized, Vector3.up);

                    if (previusPosition != transform.position)
                    {
                        traveledDistance += (transform.position - previusPosition).magnitude;
                        previusPosition = transform.position;
                    }

                    if (traveledDistance >= maxMoveDistance || (player.position - transform.position).sqrMagnitude < Mathf.Pow(maxMoveDistance, 2))
                    {
                        rigidbody.velocity = Vector3.zero;
                        rigidbody.Sleep();
                        isMoving = false;
                        traveledDistance = 0.0f;
                        timer = immobilityTime;
                    }
                }
            }
            else if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                rigidbody.velocity = Vector3.zero;
                rigidbody.Sleep();
                transform.LookAt(player.position, Vector3.up);
                Attack(player.position);
            }
        }
    }

    public override void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    public override void Die()
    {
        EnemyManager.Enemies.Remove(transform);
        Player.Score += score;
        Destroy(gameObject);
    }
}
