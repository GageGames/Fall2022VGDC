using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeOfScreenIndicators : MonoBehaviour
{
    [SerializeField] GameObject indicatorPrefab;
    [SerializeField] Canvas theCanvas;
    static Bounds viewBounds = new Bounds(new Vector2 (0.5f, 0.5f), Vector3.one * 1.2f);
    List<GameObject> allIndicatorsOut = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(PointOfInterest.AllPointsOfInterest[0]);
    }

    void UpdateIndicators()
    {

    }

    void CheckIfKill(GameObject objectOfInterest)    {
        Vector3 pos = objectOfInterest.transform.position;
		Vector2 projectedPos = Camera.main.WorldToViewportPoint(pos);
        if (!viewBounds.Contains(projectedPos))
        {
            
            
        }
    }
    
}
