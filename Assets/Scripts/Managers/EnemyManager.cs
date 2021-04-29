using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static List<Transform> Enemies;


    public void Initialize()
    {
        Enemies = new List<Transform>();
        var enemiesObjects = FindObjectsOfType<EnemyBase>();

        foreach(var item in enemiesObjects)
        {
            Enemies.Add(item.transform);
        }
    }


    public static Transform GetNearestEnemy(Vector3 position)
    {
        if (Enemies == null || Enemies.Count < 1) return null;
        if (Enemies.Count == 1) return Enemies[0];

        var minDist = (Enemies[0].position - position).sqrMagnitude;
        var index = 0;
        
        for(int i = 1; i < Enemies.Count; i++)
        {
            var dist = (Enemies[i].position - position).sqrMagnitude;
            if(dist < minDist)
            {
                minDist = dist;
                index = i;
            }
        }

        return Enemies[index];
    }
}
