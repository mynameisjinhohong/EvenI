using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_HJH : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if(viewPos.x >=0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {

        }
    }

    IEnumerator MoveBat()
    {
        yield return null;
    }
}
