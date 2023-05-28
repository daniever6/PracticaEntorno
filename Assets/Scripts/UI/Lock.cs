using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    void Update()
    {
        //Ignore parent scale
        transform.localScale = new Vector3(0.00626f / transform.parent.localScale.x, 0.00626f / transform.parent.localScale.y, 0.00626f / transform.parent.localScale.z);
    }
}
