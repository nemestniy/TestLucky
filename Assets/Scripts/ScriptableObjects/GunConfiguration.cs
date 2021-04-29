using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunConfiguration", menuName = "Configurations/Gun configuration")]
public class GunConfiguration : ScriptableObject
{
    public float CoolDown;
    public GameObject Bullet;
    public int BulletsCountOnShoot;
    public float Power;
}
