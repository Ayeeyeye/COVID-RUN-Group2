using UnityEngine;

public enum PLATFORM_MODES
{
    SPAWN,
    DESTRUCT
}

public class PlatformManager : MonoBehaviour
{
    public GameObject[] platforms;
    public GameObject platformSpawnPoint;
    public PLATFORM_MODES PlatformMode;

    private void OnTriggerEnter(Collider other)
    {
        if (PlatformMode == PLATFORM_MODES.SPAWN)
        {
            if (other.CompareTag("Player"))
            {
                Instantiate(platforms[Random.Range(0, platforms.Length)], platformSpawnPoint.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlatformMode == PLATFORM_MODES.DESTRUCT)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
        
       
        
    }
}
