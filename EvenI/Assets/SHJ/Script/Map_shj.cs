using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_shj : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float speed;

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}

        