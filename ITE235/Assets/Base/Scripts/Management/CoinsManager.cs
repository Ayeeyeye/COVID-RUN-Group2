using System;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI; // For Text Display!!!

public class CoinsManager : MonoBehaviour
{
    [Header("COIN TYPES")]
    [SerializeField] public GameObject GoldCoin = null;                // -> THE GOLD COIN
    [SerializeField] public GameObject SilverCoin = null;              // -> THE SILVER COIN
    [SerializeField] public GameObject BronzeCoin = null;              // -> THE BRONZE COIN

    [Header("COIN PROPERTIES")]
    [SerializeField] private float CoinDistance = 1.0F;                 // -> THE DISTANCE BETWEEN COINS
    [SerializeField] private int MinCoinCount = 3;                      // -> MINIMUM AMOUNT OF GOLD TO GENERATE
    [SerializeField] private int MaxCoinCount = 7;                     // -> MAXIMUM AMOUNT OF GOLD TO GENERATE

    private readonly int[] CoinTypes = { 1, 2, 3};                      // -> 1 = Gold, 2 = Silver, 3 = Bronze
    public int CoinType = 0;                                            // -> Sends cointype to GameManager.cs to add scores
                                                                        // -> depending on cointype
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        CoinType = CoinTypes[Random.Range(0, CoinTypes.Length)];        // -> RANDOMIZE COIN TYPES
        GameManager.Instant.typecapture = CoinType;                     // -> TRANSFERS COIN TYPE TO GAMEMANAGER.CS 
        switch (CoinType)                                               // -> DETERMINES WHAT COIN TO SPAWN
        {
            case 1:
                
                SpawnGolds();
               
                break;
            case 2:
                
                SpawnSilver();
                
                break;
            case 3:
                
                SpawnBronze();
                
                break;
        }
        
        
    }
    
    
    //
    // SPAWN GOLD COINS
    //
    private void SpawnGolds()
    {
        var pos = transform.position;                                    // -> GETS A POSITION IN VECTOR
        int coinCount = Random.Range(MinCoinCount, MaxCoinCount);               // -> RANDOMIZED COIN COUNT

        
        
        for (int i = 0; i < coinCount; i++)                                     // -> CREATES MANY COPIES OF COINS
        {
            var c = Instantiate(GoldCoin, 
                new Vector3(pos.x, pos.y, pos.z + i * CoinDistance), Quaternion.identity) as GameObject;
            
            // Coin Rotation
            if (i % 2 == 0)
            {
                c.GetComponentInChildren<Coins>().TurnDirection = Vector3.up;
            }
            else
            {
                c.GetComponentInChildren<Coins>().TurnDirection = Vector3.down;
            }

            

            if (i == coinCount - 1) Destroy(gameObject);
            
            
        }
        
    }

    

    //
    // SPAWN SILVER COINS
    //
    private void SpawnSilver()
    {
        var pos = transform.position;                                   // -> GET A POSITION IN VECTOR
        int coinCount = Random.Range(MinCoinCount, MaxCoinCount);       // -> RANDOMIZED COIN COUNT
        // int s = 3;

        for (int i = 0; i < coinCount; i++)                             // -> CREATE MANY COPIES OF COINS
        {
            var c = Instantiate(SilverCoin, 
                new Vector3(pos.x, pos.y, pos.z + i * CoinDistance), Quaternion.identity) as GameObject;

            if (i % 2 == 0)
            {
                c.GetComponentInChildren<Coins>().TurnDirection = Vector3.up;
            }
            else
            {
                c.GetComponentInChildren<Coins>().TurnDirection = Vector3.down;
            }

            

            if (i == coinCount - 1) Destroy(gameObject);
            
            
        }
    }
    //
    // SPAWN BRONZE
    //
    private void SpawnBronze()
    {
        var pos = transform.position;                                   // -> GET A POSITION IN VECTOR
        int coinCount = Random.Range(MinCoinCount, MaxCoinCount);       // -> RANDOMIZED COIN COUNT

        for (int i = 0; i < coinCount; i++)                             // -> CREATE MANY COPIES OF COINS
        {
            var c = Instantiate(BronzeCoin, 
                new Vector3(pos.x, pos.y, pos.z + i * CoinDistance), Quaternion.identity) as GameObject;

            if (i % 2 == 0)
            {
                c.GetComponentInChildren<Coins>().TurnDirection = Vector3.up;
            }
            else
            {
                c.GetComponentInChildren<Coins>().TurnDirection = Vector3.down;
            }

            

            if (i == coinCount - 1) Destroy(gameObject);
            
            
        }
    }

    
}
