using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // spawning is equally likely to occur in the entire area between the two circles defined by maxRadius and minRadius.
    [SerializeField]
    private float maxRadius;
    [SerializeField]
    private float minRadius;

    // spawns this many enemies at a time
    [SerializeField]
    private int spawnQuantity;
    
    // store an object somewhere in the scene for this spawner to make copies of. This object's position does not
    // matter.
    public GameObject posterChild;

    // for manual spawning: if you set this to true, it will spawn in one tick and then set itself to false
    public bool spawnNextTick;

    public Spawner(GameObject posterChild, int spawnQuantity, float minRadius, float maxRadius)
    {
        this.posterChild = posterChild;
    }

    void Spawn()
    {
        Vector3 pos = gameObject.transform.position;
        for (int i = 0; i < spawnQuantity; ++i)
        {
            float r = Mathf.Sqrt(Random.Range(minRadius * minRadius, maxRadius * maxRadius));
            float theta = Random.Range(0.0f, 2.0f * Mathf.PI);

            float relativeY = r * Mathf.Sin(theta);
            float relativeX = r * Mathf.Cos(theta);
            
            GameObject newObject = Object.Instantiate(posterChild);
            newObject.transform.position = new Vector3(pos.x + relativeX, pos.y, pos.z + relativeY);
        }
    }

    // this is only used to manually spawn
    void Update()
    {
        if (spawnNextTick)
        {
            Spawn();
            spawnNextTick = false;
        }
    }
}
