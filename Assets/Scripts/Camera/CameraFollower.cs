using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float MoveSpeed = 10;

    private Transform target;

    private float minZ;
    private float maxZ;

    private string groundObjectName = "Ground";

    public void Initialize(Transform target)
    {
        this.target = target;

        var ground = GameObject.Find(groundObjectName);
        if (ground == null)
        {
            Debug.LogError("Ground object with name \"Grount\" was not found.");
            return;
        }

        var size = ground.GetComponent<Collider>().bounds.extents;
        float min = (Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad) * (transform.position.y - ground.transform.position.y));
        maxZ = size.z - min + ground.transform.position.z;
        minZ = min - size.z + ground.transform.position.z;
    }

    private void Update()
    {
        if (target != null)
        {
            var targetPosition = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(target.position.z, minZ, maxZ));
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * MoveSpeed);
        }
    }
}
