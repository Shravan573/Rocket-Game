using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25,0,0);
    private float startdelay = 2;
    private float repeatdelay = 2;
    private PlayerController playerControllerScript;

     void Start()
    {
        InvokeRepeating("SpawnObstacle", startdelay, repeatdelay);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    void SpawnObstacle()
    {
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
        
    }

}
