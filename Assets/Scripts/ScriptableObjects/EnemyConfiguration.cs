using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfiguration", menuName = "Configurations/Enemy configuration")]
public class EnemyConfiguration : ScriptableObject
{
    public float HP;
    public float MoveSpeed;
    public float MaxMoveDistance;
    public float ImmobilityTime;
    public float Damage;
    public int Score;
    public GunConfiguration GunConfiguration;
}
