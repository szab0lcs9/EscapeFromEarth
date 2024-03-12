using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustYAxis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = gameObject.transform.position.x;
        float z = gameObject.transform.position.z;

        Vector3 adjustedPosition = new Vector3 (x, 0.0f, z);

        gameObject.transform.position = adjustedPosition;
    }
}
