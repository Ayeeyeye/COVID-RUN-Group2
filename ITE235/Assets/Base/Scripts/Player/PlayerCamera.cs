using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("CAMERA FOLLOW")]
    [SerializeField] private GameObject Target = null;                  // TARGET TO FOLLOW
    [SerializeField] private float Height = 3.0F;                       // HOW HIGH IS THE CAMERA ABOVE THE TARGET
    [SerializeField] private float Distance = 5.0F;                     // HOW FAR IS THE CAMERA FROM THE TARGET
    [SerializeField] private float Angle = 40.0F;                       // ANGLE -> X TILT

    private Transform m_Target = null;                                  // REFERENCE TO THE TARGET'S TRANSFORM

    private Vector3 TargetPos;                                          // TARGET'S POSITION
    private Vector3 Offset;                                             // WILL BE USED FOR CAMERA LERP
    private Vector3 Euler;                                              // FOR CAMERA'S TILT

    [Header("CAMERA SHAKE")]
    [SerializeField] private float ShakeAmount = 0.7F;                  // -> SHAKE AMPLITUDE -> THE HIGHER THE AMOUNT, THE STRONGER THE SHAKE IS
    [SerializeField] private float ShakeDuration = 0.0F;                // -> HOW LONG DOES THE CAMERA SHAKES?
    [SerializeField] private float DecreaseFactor = 1.0F;               // -> SHAKE AMOUNT DECREASE

    private Vector3 OrignalPosition = default;                          // -> THIS OBJECT'S LOCAL POSITION

    public static bool StartShake = false;

    //
    // START IS CALLED ONCE PER FRAME
    //
    private void Start() => m_Target = Target.transform;                // GRAB A REFERENCE TO THE TARGET'S TRANSFORM

    //
    // OnEnable IS CALLED WHEN THE OBJECT BECOMES ENABLED AND ACTIVE
    //
    private void OnEnable() => OrignalPosition = transform.localPosition;   // -> GRAB THIS OBJECT'S LOCAL POSITION


    private void LateUpdate()
    {
        // TARGET'S POSITION
        //
        TargetPos = m_Target.position;
        TargetPos.x = 0.0F;

        // THIS TRANSFORM'S POSITION
        //
        Offset.x = 0.0F;
        Offset.y = -Height;
        Offset.z = Distance;

        // THIS TRANSFORM'S ROTATION
        //

        Euler = transform.eulerAngles;
        Euler.x = Angle;

        OrignalPosition = TargetPos - Offset;

        if (StartShake)
        {
            if (ShakeDuration > 0)
            {
                // -> RANDOM INSIDE UNIT SPHERE => RETURNS A RANDOM POINT INSIDE A SPHERE WITH RADIUS 1 MULTIPLIED BY SHAKE AMOUNT
                //
                transform.localPosition = OrignalPosition + Random.insideUnitSphere * ShakeAmount;

                // -> DECREASE SHAKE DURATION BY DECREASE FACTOR MULTIPLIED BY DELTA TIME
                //
                ShakeDuration -= Time.deltaTime * DecreaseFactor;
            }
            else
            {
                // -> NO sHAKE
                ShakeDuration = 0.0F;
            }
        }
        else
        {
            // -> FOLLOW THE PLAYER
            //
            transform.position = TargetPos - Offset;
        }
        // -> TILT THE CAMERA TO FACE DOWN AT GIVEN ANGLE DEGREES
        //
        transform.eulerAngles = Euler;
    }
}
