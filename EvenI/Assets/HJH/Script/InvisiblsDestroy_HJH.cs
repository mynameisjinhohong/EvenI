using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblsDestroy_HJH : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Camera cam = Camera.main;
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if (viewPos.x <0)
        {
            gameObject.SetActive(false);
        }
    }
}
