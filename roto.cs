using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roto : MonoBehaviour
{
    public Transform r;
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, -10f, Space.Self);
    }
}
