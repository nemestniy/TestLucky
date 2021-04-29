using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicEnemy : EnemyBase
{
    private Gun gun;
    private float timer;
    private new Rigidbody rigidbody;

    private Transform player;

    private Vector3 pointToMove;
    private Vector3 previusPosition;
    private float traveledDistance;

    private bool isMoving;
    private bool turnFlag;
    private bool changeDirection;

    private bool isInitialized = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
            Debug.LogError("Rigidbody component is missing.");
        else
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.Sleep();
        }
    }

    public override void Initialize()
    {
        InitializeParams();

        if (rigidbody != null)
            rigidbody.WakeUp();

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
        turnFlag = true;
        changeDirection = false;

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
                    var velocity = (pointToMove - transform.position).normalized * moveSpeed;

                    rigidbody.velocity = velocity;

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

                    if(traveledDistance < maxMoveDistance && (pointToMove - transform.position).sqrMagnitude < 1.0f)
                    {
                        pointToMove = player.position;
                    }

                    if (changeDirection)
                    {
                        if (turnFlag)
                        {
                            pointToMove = transform.position + transform.right * moveSpeed;
                        }
                        else if (!turnFlag)
                        {
                            pointToMove = transform.position + transform.right * moveSpeed * -1;
                        }
                        turnFlag = !turnFlag;
                        changeDirection = false;
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

            if ((player.position - transform.position).sqrMagnitude < Mathf.Pow(maxMoveDistance*0.5f, 2))
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.Sleep();
                transform.LookAt(player.position, Vector3.up);
                Attack(player.position);
            }
        }
        else
        {
            if (rigidbody != null)
                rigidbody.velocity = Vector3.zero;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == GameManager.Player.tag)
        {
            GameManager.Player.Damage(damage);
        }
        else
        {
            changeDirection = true;
        }
    }
}
