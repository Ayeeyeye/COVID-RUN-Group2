using System;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerController : MonoBehaviour
{

    public AudioSource JumpSound;
    public AudioSource DeathSound;
    public AudioSource DodgeSound;
    
    //
    // LANE PROPERTIES 
    //
    [Header("LANE PROPERTIES")] [SerializeField]
    private int StartingLane = 0; // -> STARTINGLANE = THE CENTER (0) OF STARTING LANE

    [SerializeField] private int LaneX = 1; // -> LANEX REPRESENTS BOTH LEFT AND RIGHT LANES

    private Vector3 LaneOffset = default; // -> WILL BE USED FOR LERPING PLAYER'S POSITION

    //
    // PLAYER TRANSFORMS & PROPERTIES
    //
    [Header("PLAYER PROPERTIES")] [SerializeField]
    private float LaneSwitchSpeed = 5.0F; // -> HOW FAST CAN WE SWITCH LANES 

    [SerializeField] private float JumpAmount = 10.0F; // -> HOW HIGH CAN WE JUMP?
    [SerializeField] private float MaxJumpHeight = 1.0F; // -> JUMP HEIGHT CAN'T BE HIGHER THAN THIS

    private Vector3 Position = default; // -> THE POSITION OF PLAYER IN VECTOR
    private float RunSpeed = 0.0F; // -> HOW FAST DOES THE PLAYER RUNS 

    private Rigidbody m_Rigidbody = null; // -> REQUIRED FOR PHYSICS MOVEMENTS
    private PlayerAnimation playerAnimation = null; // -> PLAYER ANIMATION CONTROLLER SCRIPT

    //
    // GROUND CHECK & GRAVITY
    //
    [Header("GROUNDED PROPERTIES")] [SerializeField]
    private bool isGrounded = false; // -> ARE WE STEPPING ON THE GROUND 

    [SerializeField] private float RayLength = 0.5F; // -> LENGTH OF RAYCAST
    [SerializeField] private float RayOffsetY = 1.0F; // -> RAYCAST POSITION Y

    private readonly float FAKE_GRAVITY = 9.8F; // -> VIRTUAL GRAVITY

    //
    // NON-DESKTOP INPUTS (TAPS & SWIPES)
    // 
    private bool isSwiping = false;
    public Vector2 startingTouch;

    //
    // FOR INITIALIZATION, BEFORE ANYTHING IS CALLED
    //
    private void Awake()
    {

        m_Rigidbody = GetComponent<Rigidbody>(); // -> GET A REFERENCE TO THE PLAYER'S RIGIDBODY
        playerAnimation = GetComponent<PlayerAnimation>(); // -> GET A REFERENCE TO THE PLAYER ANIMATION SCRIPT
    }

    //
    // FOR INITIALIZATION, CALLED BEFORE FIRST FRAME UPDATE
    //
    private void Start()
    {
        Position.x = StartingLane; // -> MAKE SURE PLAYER'S X POSITION IS AT THE CENTER (0)

    }

    //
    // UPDATE IS CALLED EVERY FRAME
    //
     private void Update()
     {
// #if !UNITY_ANDROID || (UNITY_EDITOR || UNITY_STANDALONE)
//         // LEFT ARROW or A KEY IS PRESSED 
//         //
//         // -> MATHF CLAMP IS USED TO LIMIT / RESTRICT VALUES BETWEEN MIN AND MAX
//         //
//         if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
//         {
//             LaneOffset.x -= LaneX;
//             LaneOffset.x = Mathf.Clamp(LaneOffset.x, -LaneX, LaneX);
//             DodgeSound.Play();
//         }
//
//         // RIGHT ARROW or D KEY IS PRESSED
//         //
//         // -> MATHF CLAMP IS USED TO LIMIT / RESTRICT VALUES BETWEEN MIN AND MAX
//         //
//         if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
//         {
//             LaneOffset.x += LaneX;
//             LaneOffset.x = Mathf.Clamp(LaneOffset.x, -LaneX, LaneX);
//             DodgeSound.Play();
//         }

#if !UNITY_ANDROID || (!UNITY_EDITOR || !UNITY_STANDALONE )


        if (Input.touchCount == 1)
        {
            if (isSwiping)
            {
                Vector2 diff = Input.GetTouch(0).position - startingTouch;

                diff = new Vector2(diff.x / Screen.width, diff.y / Screen.width);

                if (diff.magnitude > 0.01F)
                {
                    if (Mathf.Abs(diff.y) > Mathf.Abs(diff.x))
                    {
                        if (diff.y < 0)
                        {
                            // Down
                        }
                        else
                        {
                            // Up
                            if (isGrounded)
                            {
                                playerAnimation.Jump();
                                m_Rigidbody.AddRelativeForce(Vector3.up * JumpAmount, ForceMode.Impulse);

                            }
                            else
                            {
                                // FALL FASTER IF WE ARE JUMPING / ON AIR
                                //
                                var vel = m_Rigidbody.velocity;
                                vel.y -= FAKE_GRAVITY * Time.deltaTime;
                                m_Rigidbody.velocity = vel;

                                playerAnimation.ClearJump();
                                playerAnimation.Run();
                                JumpSound.Play();
                            }
                        }
                    }
                    else
                    {
                        if (diff.x < 0)
                        {
                            // Left
                            LaneOffset.x -= LaneX;
                            LaneOffset.x = Mathf.Clamp(LaneOffset.x, -LaneX, LaneX);
                            DodgeSound.Play();
                        }
                        else
                        {
                            // Right
                            LaneOffset.x += LaneX;
                            LaneOffset.x = Mathf.Clamp(LaneOffset.x, -LaneX, LaneX);
                            DodgeSound.Play();
                        }
                    }

                    isSwiping = false;
                }

            }
            else
            {
                // TAP
                //playerAnim.BeginAnimation();
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startingTouch = Input.GetTouch(0).position;
                isSwiping = true;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isSwiping = false;
            }
        }

#endif



        //
        // RECREATE THE POSITION 
        //
        Position = transform.localPosition;

        // -> LERP => FOR LINEARLY INTERPOLATING BETWEEN PointA and PointB WITH SMOOTHING APPLIED
        //
        Position.x = Mathf.Lerp(Position.x, LaneOffset.x, LaneSwitchSpeed * Time.deltaTime);

        // -> GET THE Y POSITION AND LIMIT ITS JUMP HEIGHT
        //
        Position.y = transform.localPosition.y;
        Position.y = Mathf.Clamp(Position.y, 0.0F, MaxJumpHeight);

        // -> MOVE FORWARD; RUN SPEED IS CONTROLLED BY GAME MANAGER
        //
        RunSpeed = GameManager.RunSpeed;
        Position.z += RunSpeed * Time.deltaTime;

        //
        // UPDATE THIS TRANSFORM'S POSITION 
        //
        transform.position = Position;

    }

    //
    // FIXED UPDATE IS FOR HEAVY PHYSICS
    //
    private void FixedUpdate()
    {
        // USE PHYSICS TO CHECK IF WE ARE GROUNDED OR NOT
        //
        isGrounded = (Physics.Raycast(
            new Vector3(transform.position.x, transform.position.y + RayOffsetY, transform.position.z),
            Vector3.down, RayLength));

        if (isGrounded)
        {
            // JUMP IF WE PRESSED SPACEBAR WHILE WE ARE GROUNDED
            //
            if (Input.GetAxis("Jump") != 0)
            {
                playerAnimation.Jump();
                m_Rigidbody.AddRelativeForce(Vector3.up * JumpAmount, ForceMode.Impulse);
                JumpSound.Play();
            }
            // WE HAVE LANDED, SO CLEAR OUT JUMP ANIMATIONS AND SWITCH BACK TO RUN ANIMATION
            //
            else
            {
                playerAnimation.ClearJump();
                playerAnimation.Run();
            }

            // --> JUST FOR DEBUGGING PURPOSES; DOESNT AFFECT THE GAME 
            //
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + RayOffsetY, transform.position.z),
                Vector3.down * RayLength, Color.green);
        }
        else
        {
            // FALL FASTER IF WE ARE JUMPING / ON AIR
            //
            var vel = m_Rigidbody.velocity;
            vel.y -= FAKE_GRAVITY * Time.deltaTime;
            m_Rigidbody.velocity = vel;

            // --> JUST FOR DEBUGGING PURPOSES; DOESNT AFFECT THE GAME 
            //
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + RayOffsetY, transform.position.z),
                Vector3.down * RayLength, Color.red);
        }
    }

    //
    // CHECK FOR COLLISION
    //
    private void OnCollisionEnter(Collision collision)
    {
        // IF WE HAVE COLLIDED ON ANY OBJECT WITH TAG NAME "Obstacle" THEN STOP RUNNING; PLAY THE "Fall" ANIMATION
        //
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // -> APPLY CAMERA SHAKE
            playerAnimation.Fall();
            // PlayerCamera.StartShake = true;
            DeathSound.Play();
            
            
            GameManager.RunSpeed = 0.0F;
            GameManager.GameOver = true;
            PlayerManager.gameOver = true;
            
        }

        
    }

    //
    // NATIVE GUI -> BASIC UI; KAPAG GUSTO NYO GUMAWA NG UI, USE THE NEW UI SYSTEM (Drag n Drop) INSTEAD
    //
    // private void OnGUI()
    // {
    //     // WE'LL USE THIS FOR DEBUGGING... AGAIN, IT DOESNT AFFECT THE GAME!
    //     GUI.Label(new Rect(10, 10, 200, 150),
    //         $"isGrounded = {isGrounded}\n\nPOSITION:\n\tX: {Position.x:F}\n\tY: {Position.y:F}\n\tZ: {Position.z:F}");
    // }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
// #endif

