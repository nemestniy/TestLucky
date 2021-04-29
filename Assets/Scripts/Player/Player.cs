using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public static int Score;
    public event Action OnDie;

    public PlayerConfiguration Configuration;

    public PlayerController playerController { get; private set; }

    private Gun gun;

    public float HP { get; private set; }

    public void Initialize(int initialScore = 0)
    {
        HP = Configuration.HP;
        Score = initialScore;
    }

    public void StartPlayer()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
            Debug.LogError("PlayerController is missing.");

        playerController.Inititalize(Configuration.MoveSpeed, Configuration.Damage);

        gun = GetComponent<Gun>();
        if (gun != null)
            gun.Initialize(Configuration.GunConfiguration);
        else
            Debug.LogError("Gun component is missing.");
    }

    public void StopPlayer()
    {
        playerController.enabled = false;
    }

    public void Die()
    {
        OnDie?.Invoke();
        gameObject.SetActive(false);
    }
    
    public void Damage(float damage)
    {
        HP = Mathf.Max(0, HP - damage);
        if (HP == 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Exit" && EnemyManager.Enemies.Count == 0)
        {
            GameManager.Instance.Finish();
        }
    }
}
