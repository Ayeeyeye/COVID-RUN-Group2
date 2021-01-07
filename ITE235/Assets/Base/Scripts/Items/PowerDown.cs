using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDown : MonoBehaviour
{
    public static PowerUp PUInst;
    public int scorecatch;
    
    public static float SlowMoVal = 2.0f; // Value of SlowMo
    public static float NormalTimeVal = 1.0f; // Value of time
    public static float PUEffectTime = 5.0f; // Duration of PowerUp Effect
    
    public Vector3 PUTurning = Vector3.up;
    private Vector3 LocalRotationPU;
    private AudioSource FastSpeedSound;

    [SerializeField] private float RotateSpeed = 40.0F;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerController>().StartCoroutine(Countdown());
        Destroy(gameObject);
        FastSpeedSound.Play();
    }

    public static IEnumerator Countdown() // PowerUp Effect Time
    {
        Time.timeScale = SlowMoVal;
        yield return new WaitForSeconds(PUEffectTime);
        Time.timeScale = NormalTimeVal;
    }

    // Update is called once per frame
    void Update()
    {
        FastSpeedSound = GameObject.Find("PowerUpSound").GetComponent<AudioSource>();
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
