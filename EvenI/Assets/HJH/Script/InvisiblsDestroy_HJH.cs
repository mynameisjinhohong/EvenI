using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblsDestroy_HJH : MonoBehaviour
{
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0)
        {
            Destroy(gameObject);
        }
    }
}
