using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Configurations/Player configuration")]
public class PlayerConfiguration : ScriptableObject
{
    public float HP;
    public float MoveSpeed;
    public float Damage;
    public GunConfiguration GunConfiguration;
}
