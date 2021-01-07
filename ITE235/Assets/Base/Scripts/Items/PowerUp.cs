using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public static PowerUp PUInst;
    public int scorecatch;
    
    public static float SlowMoVal = 0.7f; // Value of SlowMo
    public static float NormalTimeVal = 1.0f; // Value of time
    public static float PUEffectTime = 5.0f; // Duration of PowerUp Effect

    private AudioSource SlowSpeedSound;
    
    public Vector3 PUTurning = Vector3.up;
    private Vector3 LocalRotationPU;

    [SerializeField] private float RotateSpeed = 40.0F;
    

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().StartCoroutine(Countdown());
        Destroy(gameObject);
        SlowSpeedSound.Play();
    }

    public static IEnumerator Countdown() // PowerUp Effect Time
    {
        Time.timeScale = SlowMoVal;
        yield return new WaitForSeconds(PUEffectTime);
        Time.timeScale = NormalTimeVal;
    }

    public void Update()
    {
        SlowSpeedSound = GameObject.Find("PowerDownSound").GetComponent<AudioSource>();
        
            LocalRotationPU = transform.localEulerAngles;

            if (PUTurning == Vector3.up)
            {
                LocalRotationPU.y += (RotateSpeed * 10.0F) * Time.deltaTime;
            }
            else if (PUTurning == Vector3.down)
            {
                LocalRotationPU.y -= (RotateSpeed * 10.0F) * Time.deltaTime;
            }

            LocalRotationPU.z = 0.0F;
            transform.localEulerAngles = LocalRotationPU;
        
        

    }
}
