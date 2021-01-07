using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Covid : MonoBehaviour
{
    public Vector3 PUTurning = Vector3.up;
    private Vector3 LocalRotationPU;
    private Vector3 Altitude;
    [SerializeField] private float RotateSpeed = 40.0F;
    
    private void OnTriggerEnter(Collider other)
    {
        
    }
    
    public void Update()
    {
        if (PUTurning == Vector3.up)
        {
            
            LocalRotationPU.y += (RotateSpeed * 10.0F) * Time.deltaTime;
        }
        else if (PUTurning == Vector3.down)
        {
            LocalRotationPU.y -= (RotateSpeed * 10.0F) * Time.deltaTime;
        }

        LocalRotationPU.z = 0.2F;
        transform.localEulerAngles = LocalRotationPU;
        
        

    }
}
