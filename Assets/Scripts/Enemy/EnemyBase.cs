using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyConfiguration Configuration;

    protected float hp;
    protected float moveSpeed;
    protected float maxMoveDistance;
    protected float immobilityTime;
    protected float damage;
    protected int score;

    public abstract void Initialize();

    public abstract void Damage(float damage);
    public abstract void Die();
     
    protected abstract void Attack(Vector3 target);

    protected virtual void InitializeParams()
    {
        hp = Configuration.HP;
        moveSpeed = Configuration.MoveSpeed;
        maxMoveDistance = Configuration.MaxMoveDistance;
        immobilityTime = Configuration.ImmobilityTime;
        damage = Configuration.Damage;
        score = Configuration.Score;
    }
}
