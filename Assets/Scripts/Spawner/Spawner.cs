using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnTokens;
    public List<GameObject> spawnPool;
    private GameObject quad;
    public List<GameObject> quads;
    public float spawnTime;
    public float spawnDelay;
    private int spawnNum;
    private int tempToken;
    private int spawnLocations;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("spawnEnemy", spawnTime, spawnDelay);
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void spawnEnemy()
    {
        tempToken = spawnTokens;
        if (spawnTokens <= 0)
        {
            CancelInvoke("spawnEnemy");
        }
        else
        {
            GameObject toSpawn;
            spawnLocations = Random.Range(0, quads.Count);
            quad = quads[spawnLocations];
            Transform c = quad.GetComponent<Transform>();

            spawnNum = Random.Range(0, spawnPool.Count);   
            toSpawn = spawnPool[spawnNum];

            Instantiate(toSpawn, c.position, c.rotation);

            spawnTokens = spawnTokens - (spawnNum + 1);
        }
        

            
        
    }
}

