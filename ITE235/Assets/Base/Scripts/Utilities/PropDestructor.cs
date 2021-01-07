using UnityEngine;

public class PropDestructor : MonoBehaviour
{
    [SerializeField] private GameObject MainCamera = null;      // -> THE MAIN CAMERA'S GAME OBJECT (NOT COMPONENT!)
    [SerializeField] private float Distance = 3.0F;             // -> DISTANCE BETWEEN THIS OBJECT AND THE CAMERA

    private Vector3 CamPos = default;                           // -> MAIN CAMERA'S POSITION
    private Vector3 Offset = default;                           // -> DISTANCE OFFSET
    private string DBG_OBJ = null;                              // -> FOR DEBUGING, TELL WHAT IS DESTROYED

    private void LateUpdate()
    {
        CamPos = MainCamera.gameObject.transform.position;      // -> GET A REFERENCE TO CAMERA'S POSITION

        Offset.x = 0.0F;                                        // -> MAKE SURE THIS STAYS AT THE CENTER
        CamPos.y = 1.5F;                                        // -> MAKE SURE THIS DOESN'T GO UP OR DOWN
        Offset.z = Distance;                                    // -> ASSIGN Z = DISTANCE BETWEEN CAMERA AND THIS OBJECT

        // FOLLOW THE CAMERA'S POSITION
        //
        transform.position = CamPos - Offset;
    }

    // -> DESTROY EVERYTHING THAT INTERSECTS INTO THIS OBJECT GIVEN ITS TAG NAME
    //
    private void OnTriggerEnter(Collider other)
    {
        string props = "Obstacle";                         // -> ALL POSSIBLE TAGS

        if (other.CompareTag(props))
        {
            DBG_OBJ = other.gameObject.name;
            Destroy(gameObject);
        }

    }

    // private void OnGUI()
    // {
    //     GUI.Label(new Rect(Screen.width - 200.0F, 10.0F, 200.0F, 50.0F), $"DESTROYED OBJECT => {DBG_OBJ}");
    // }
}
