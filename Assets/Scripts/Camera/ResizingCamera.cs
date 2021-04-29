using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResizingCamera : MonoBehaviour
{
    public bool isOrthographic;

    private const float targetRatio = 9.0f / 16.0f;
    private new Camera camera;

    private void Awake()
    { 
        camera = GetComponent<Camera>();
        if (!isOrthographic)
            camera.aspect = targetRatio;
        else
        {
            var customWidth = camera.orthographicSize * targetRatio;
            var wantedSize = customWidth / camera.aspect;
            camera.orthographicSize = Mathf.Max(wantedSize, customWidth);
        }
    }
}
