using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int spawnTokens;
    public List<GameObject> spawnPool;
    public GameObject quad;
    public float spawnTime;
    public float spawnDelay;
    public int spawnNum;
    

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
        
        if(spawnTokens <= 0)
        {
            CancelInvoke("spawnEnemy");
        }
        else
        {
            GameObject toSpawn;
            MeshCollider c = quad.GetComponent<MeshCollider>();

            float spawnGridX, spawnGridY;
            Vector2 pos;


            spawnNum = Random.Range(0, spawnPool.Count);

            toSpawn = spawnPool[spawnNum];


            spawnGridX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            spawnGridY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(spawnGridX, spawnGridY);

            //Instantiate(toSpawn, pos, toSpawn.transform.rotation);
            Instantiate(toSpawn, transform.position, transform.rotation);

            spawnTokens = spawnTokens - (spawnNum + 1);
        }
        

            
        
    }
}

