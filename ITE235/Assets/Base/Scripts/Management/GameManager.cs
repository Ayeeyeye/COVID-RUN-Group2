using System;
using System.Collections;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    // powerup
    // public Transform powerupobj;
    // public static float SlowMoVal = 0.5f; // Value of SlowMo
    // public static float NormalTimeVal = 1.0f; // Value of time
    // public static float PUEffectTime = 5.0f; // Duration of PowerUp Effect
    // public Vector3 PUTurning = Vector3.up;
    // private Vector3 LocalRotationPU;
    // [SerializeField] private float RotateSpeed = 40.0F;
    private AudioSource CoinSound;
    private AudioSource GoldCoinSFX;
    private AudioSource SilverCoinSFX;
    private AudioSource BGSound;
    public int HighScore = 0;
    public Text txtHighScore;
    public int HighestScore;
    
    
    [Header("PLAYER PROPERTIES")] [SerializeField]
    private float StartingRunSpeed = 5.0F; // -> INITIAL RUN SPEED

    [SerializeField] private float MaxRunSpeed = 20.0F; // -> FASTEST RUN SPEED
    [SerializeField] private float RunSpeedRate = 0.1F; // -> SPEED RATE FOR CHANGING RUN SPEED
    public static float RunSpeed = 0.0F; // -> ADJUSTABLE RUN SPEED

    public static bool isGameStarted;

    
    //-Zane
    //-for instance, transfer or cointype for scoring purposes
    public static GameManager Instant;
    public int Score = 0;
    public Text ScoreText;
    public int typecapture = 0;

    
    
    [Header("GAME PROPERTIES")]
    // -> SPAWN FREQUENCY
    public static bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        HighestScore = PlayerPrefs.GetInt("HighScore", 0);
        CoinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>();// Finds CoinSound in the Unity Editor
        SilverCoinSFX = GameObject.Find("SilverCoinSound").GetComponent<AudioSource>();
        GoldCoinSFX = GameObject.Find("GoldCoinSound").GetComponent<AudioSource>();
        BGSound = GameObject.Find("BGSound").GetComponent<AudioSource>();
        
        isGameStarted = false;
        RunSpeed = StartingRunSpeed;
        // typecapture = CoinsManager.CoinTypeInst.CoinType;
        
    }

    // Update is called once per frame
    void Update()
    {
        txtHighScore.text = "Highest Score: " +  HighestScore;
        // Try lng power-up
        // LocalRotationPU = transform.localEulerAngles;
        //
        // if (PUTurning == Vector3.up)
        // {
        //     LocalRotationPU.y += (RotateSpeed * 10.0F) * Time.deltaTime;
        // }
        // else if (PUTurning == Vector3.down)
        // {
        //     LocalRotationPU.y -= (RotateSpeed * 10.0F) * Time.deltaTime;
        // }
        //
        // LocalRotationPU.z = 0.0F;
        // transform.localEulerAngles = LocalRotationPU;
        
        if (!GameOver)
        {
            RunSpeed += RunSpeedRate * Time.deltaTime; // -> INCREASE RUN SPEED OVERTIME
            RunSpeed = Mathf.Clamp(RunSpeed, StartingRunSpeed, MaxRunSpeed); // -> LIMIT RUN SPEED
        }

        if (GameOver)
        {
            RunSpeed += RunSpeedRate * Time.deltaTime; // -> INCREASE RUN SPEED OVERTIME
            RunSpeed = Mathf.Clamp(RunSpeed, StartingRunSpeed, MaxRunSpeed); // -> LIMIT RUN SPEED
        }
    }

    // private void OnGUI()
    // {
    //     // -> FOR DEBUGGING PURPOSES; DOESNT AFFECT THE GAME
    //     GUI.Label(new Rect(Screen.width - 200.0F, 60.0F, 200.0F, 50.0F),
    //         $"RUN SPEED -> {RunSpeed}"); //\nOBSTACLE RATE -> {ObstacleSpawnRate}");
    // }

    public void IncrementScore()
    {


        if (typecapture == 1)
        {
            Score += 100;
            // ObstacleManager.OMInst.Scorecather = Score;
            GoldCoinSFX.Play();
            // CoinSound.Play();
            ScoreText.text = "SCORE : " + Score;
        }

        if (typecapture == 2)
        {
            Score += 50;
            // ObstacleManager.OMInst.Scorecather = Score;
            SilverCoinSFX.Play();
            // CoinSound.Play();
            ScoreText.text = "SCORE : " + Score;
        }

        if (typecapture == 3)
        {
            Score += 30;
            // ObstacleManager.OMInst.Scorecather = Score;
            CoinSound.Play();
            ScoreText.text = "SCORE : " + Score;
        }
        if (Score >= HighestScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
        
        
        


    }

    private void Awake()
    {
        Instant = this;
    }
    
    // try lng powerup
    // private void OnTriggerEnter(Collider other)
    // {
    //     other.GetComponent<PlayerController>().StartCoroutine(Countdown());
    //     Destroy(gameObject);
    // }

    // IEnumerator Countdown() // PowerUp Effect Time
    // {
    //     Time.timeScale = SlowMoVal;
    //     yield return new WaitForSeconds(PUEffectTime);
    //     Time.timeScale = NormalTimeVal;
    // }

    
}
