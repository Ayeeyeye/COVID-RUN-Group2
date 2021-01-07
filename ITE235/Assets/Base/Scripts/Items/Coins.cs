
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private float RotateSpeed = 40.0F;                    // -> HOW FAST DOES THIS COIN ROTATE PER SECOND ?
    
    private Vector3 LocalRotation = default;                               // -> GET THE ROTATION QUATERNION

    public Vector3 TurnDirection = Vector3.up;                             // -> BY DEFAULT, COIN ROTATES CLOCKWISE
    // private AudioSource CoinSound;

    
    
    // Start is called before the first frame update
    void Start()
    {
        // CoinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();// Finds CoinSound in the Unity Editor
    }

    // Update is called once per frame
    void Update()
    {
        LocalRotation = transform.localEulerAngles;                       // -> ROTATION IS STORED AS VECTOR NOT QUATERNION
        
        if (TurnDirection == Vector3.up)                                  // -> SPIN CLOCKWISE
            LocalRotation.y += (RotateSpeed * 10.0F) * Time.deltaTime;
        else if (TurnDirection == Vector3.down)                           // -> SPIN COUNTER CLOCKWISE
            LocalRotation.y -= (RotateSpeed * 10.0F) * Time.deltaTime;

        LocalRotation.z = 0.0F;                                           // -> NO ROTATION ON Z

        transform.localEulerAngles = LocalRotation;                       // -> UPDATE THE ROTATION
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.GetComponent<ObstacleManager>() != null)
        {
            Destroy(gameObject);
            return;
        }
        
        if (other.gameObject.name != "Player")            // Checks if the player has collided with the coin
        {
            Destroy(gameObject);
            // CoinSound.Play();
            return;
            

        }
        Destroy(gameObject);    // Destroy coins touch by player
        GameManager.Instant.IncrementScore();
        
        
        
        
        
    }
}
