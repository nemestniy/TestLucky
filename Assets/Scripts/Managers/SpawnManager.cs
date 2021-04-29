using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Player:")]
    public GameObject PlayerObject;
    public Transform PlayerSpawnPlace;

    [Header("Enemies:")]
    public int EnemiesSpawnCountOnStart;
    public List<EnemyBase> EnemyObjects;

    private Rect spawnEnemyArea;

    public Player SpawnPlayer()
    {
        return Instantiate(PlayerObject, PlayerSpawnPlace.position, PlayerSpawnPlace.rotation).GetComponent<Player>();
    }

    public void SpawnEnemies()
    {
        ComputeSpawnEnemiesArea();

        for (int i = 0; i < EnemiesSpawnCountOnStart; i++)
        {
            GenerateEnemy();
        }
    }

    private void GenerateEnemy()
    {
        if (spawnEnemyArea == null) return;

        var enemy = Instantiate(GetRandomEnemy());
        enemy.transform.position = new Vector3(Random.Range(spawnEnemyArea.xMin, spawnEnemyArea.xMax),
                                               enemy.transform.position.y,
                                               Random.Range(spawnEnemyArea.yMin, spawnEnemyArea.yMax)); 
    }

    private void ComputeSpawnEnemiesArea()
    {
        var ground = GameObject.Find("Ground");
        if (ground == null)
        {
            Debug.LogError("Ground object with name \"Grount\" was not found.");
            return;
        }

        var size = ground.GetComponent<Collider>().bounds.extents;

        var maxPoint = ground.transform.position + size;
        var minPoint = ground.transform.position - size;
        var diff = maxPoint - minPoint;

        var minSpawnPoint = new Vector3(maxPoint.x - diff.x, maxPoint.y, maxPoint.z - diff.z * (2.0f / 3.0f));

        spawnEnemyArea = new Rect(minSpawnPoint.x, minSpawnPoint.z, diff.x, diff.z * (2.0f / 3.0f));
    }

    private EnemyBase GetRandomEnemy()
    {
        if (EnemyObjects == null || EnemyObjects.Count < 1) return null;

        int randomIndex = Random.Range(0, EnemyObjects.Count);

        return EnemyObjects[randomIndex];
    }
}
