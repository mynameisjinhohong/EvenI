using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblsDestroy_HJH : MonoBehaviour
{

    private void Start()
    {
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
