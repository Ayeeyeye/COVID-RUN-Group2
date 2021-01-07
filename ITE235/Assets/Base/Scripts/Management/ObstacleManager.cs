using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleManager : MonoBehaviour
{
    public static ObstacleManager OMInst;
    // public int Scorecather;
    
    [SerializeField] public GameObject[] Obstacles = null;
    // [SerializeField] public GameObject[] Obstacles2 = null;
    [SerializeField] private float DistanceOffset = 30.0F;

    [SerializeField]
    [Range(0.38F, 3.0F)] private float SpawnRate = 0.0F;
    [SerializeField] private float SpawnRateSpeed = 0.1F;

    private Vector3 Offset = default;
    private GameObject Player = null;
    private readonly int[] LANES = { -1, 0, 1 };                     // -> RANDOM LANE SPAWN POINTS FOR X

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating(nameof(SpawnObstacles), 0.0F, GameManager.ObstacleSpawnRate); //SpawnRate);
        StartCoroutine("SpawnObstacles");

        Player = GameObject.FindGameObjectWithTag("Player");

        
    }
    IEnumerator SpawnObstacles()
    //void SpawnObstacles()
    {
        while (!GameManager.GameOver)
        {
            yield return new WaitForSeconds(SpawnRate); // SMALLER VALUES -> FASTER SPAWN TIME

            Instantiate(Obstacles[Random.Range(0, Obstacles.Length)],
                    new Vector3(LANES[Random.Range(0, LANES.Length)], 0,
                    transform.position.z), Quaternion.Euler(0.0F, 90.0F, 0.0F));
        }

        while (GameManager.GameOver)
        {
            yield return new WaitForSeconds(SpawnRate); // SMALLER VALUES -> FASTER SPAWN TIME

            Instantiate(Obstacles[Random.Range(0, Obstacles.Length)],
                new Vector3(LANES[Random.Range(0, LANES.Length)], 0,
                    transform.position.z), Quaternion.Euler(0.0F, 90.0F, 0.0F));

        }
        
    }

    // UPDATE IS CALLED EVERY FRAME
    //
    private void Update()
    {
        Offset.z = DistanceOffset;
        transform.position = new Vector3(0, 0, Player.transform.position.z) + Offset;

        SpawnRate = Random.Range(0.0F, 2.0F);
        SpawnRate -= SpawnRateSpeed  * Time.deltaTime;
        SpawnRate = Mathf.Clamp(SpawnRate, 0.38F, 3.0F);
        
        
    }
}
