using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EdgeOfScreenIndicators : MonoBehaviour
{
    [SerializeField] float maxDistanceToSeeIndicators;
    [SerializeField] GameObject indicatorPrefab;
    [SerializeField] GameObject thePlayer;
    [SerializeField] Camera theCamera;
    [SerializeField] Canvas theCanvas;
    static Bounds viewBounds = new Bounds(new Vector2 (0.5f, 0.5f), Vector3.one * 1.2f);
    List<EOSIndicator> allIndicators = new List<EOSIndicator>();
    // Start is called before the first frame update
    void Start()
    {
        maxDistanceToSeeIndicators = Mathf.Clamp(maxDistanceToSeeIndicators,1,800);
    }

    // Update is called once per frame
    void Update()
    {
        if(thePlayer != null)
        {
            UpdateIndicators();
        }
    }
    void CreateIndicator(PointOfInterest POI)
    {
        GameObject indicatorObj = Instantiate(indicatorPrefab, transform.position, Quaternion.identity) as GameObject;
        indicatorObj.transform.SetParent(theCanvas.transform);
        indicatorObj.transform.localScale = Vector3.one;
        indicatorObj.GetComponent<EOSIndicator>().correspondingPointOfInterest = POI;
        print(POI.Type);
        if(POI.Type == POIType.Key)
        {
            indicatorObj.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            indicatorObj.GetComponent<Image>().color = new Color(.46f,.86f,.46f,1);
        }
        allIndicators.Add(indicatorObj.GetComponent<EOSIndicator>());
    }
    void UpdateIndicators()
    {    
         //remove indicators if object is gone or if object is on screen
        List<EOSIndicator> indicatorsToRemove = new List<EOSIndicator>();
        foreach (var indicator in allIndicators)
        {
            if(indicator.correspondingPointOfInterest == null || PointOfInterest.AllPointsOfInterest.Contains(indicator.correspondingPointOfInterest) == false || CheckIfKill(indicator.correspondingPointOfInterest.gameObject))
            {
                indicatorsToRemove.Add(indicator);
            }
        }
        foreach(var indicator1 in indicatorsToRemove)
        {
            allIndicators.Remove(indicator1);
            Destroy(indicator1.gameObject);
        }
        //add indicators if new objects not in the list
        foreach(var POI in PointOfInterest.AllPointsOfInterest)
        {
            
            //print(allIndicators.Count);
            bool doesExist = false;
            foreach(var indicator in allIndicators)
            {
                if(indicator.correspondingPointOfInterest.gameObject == POI.gameObject)
                {
                    doesExist = true;
                }
            }
            if (doesExist == false && CheckIfKill(POI.gameObject) == false)
            {
                CreateIndicator(POI);
            }
        }
        //update position of indicators
        foreach (var indicator in allIndicators)
        {
            setIndicatorPositionAndTransparency(indicator);
        }
    }
    void setIndicatorPositionAndTransparency(EOSIndicator indicator)
    {
        float dist = Vector3.Distance(indicator.correspondingPointOfInterest.transform.position, thePlayer.transform.position);
        

        if(dist> maxDistanceToSeeIndicators)
        {
            indicator.GetComponent<Image>().color = new Color(indicator.GetComponent<Image>().color.r,indicator.GetComponent<Image>().color.g,indicator.GetComponent<Image>().color.b, 0);
        }
        else
        {
            indicator.GetComponent<Image>().color = new Color(indicator.GetComponent<Image>().color.r,indicator.GetComponent<Image>().color.g,indicator.GetComponent<Image>().color.b, (maxDistanceToSeeIndicators-dist)/maxDistanceToSeeIndicators);
            RectTransform RT = indicator.GetComponent<RectTransform>();
            Vector3 POIPos = calculateWorldPosition(indicator.correspondingPointOfInterest.transform.position, theCamera);
            Vector2 projectedPos = Camera.main.WorldToViewportPoint(POIPos);
            float rotation = 0.0f;
            float y = projectedPos.y - 0.5f;
            float x = projectedPos.x - 0.5f;
            float theX = 0.0f;
            float theY = 0.0f;
            float m = (y)/(x);
            if(y > 0.5f)//TEST ABOVE
            {
                float testX = 0.5f/m;
                if(testX > 0.5f || testX < -0.5f)
                {
                    if(x > 0)
                    {
                        //then it is offscreen to the right, not above
                        theX = 0.5f;
                        theY = m*0.5f;
                        rotation = 270;
                        
                    }
                    else
                    {
                        theX = -0.5f;
                        theY = m*(-0.5f);
                        rotation = 90;
                    }
                }
                else
                {
                    theY = 0.5f;
                    theX = testX;
                    rotation = 0;
                }
            }
            else//TEST BELOW
            {
                float testX = -0.5f/m;
                if(testX > 0.5f || testX < -0.5f)
                {
                    if(x> 0)
                    {
                        //then it is offscreen to the right, not below
                        theX = 0.5f;
                        theY = m*0.5f;
                        rotation = 270;
                    }
                    else
                    {
                        theX = -0.5f;
                        theY = m*(-0.5f);
                        rotation = 90;
                    }
                }
                else
                {
                    theY = -0.5f;
                    theX = testX;
                    rotation  = 180;
                }
            }
            //reset offset
            theX += 0.5f;
            theY += 0.5f;
            //set pos
            RT.rotation = Quaternion.Euler(new Vector3(0,0,rotation));
            RT.anchoredPosition = theCanvas.GetComponent<RectTransform>().sizeDelta*new Vector2(theX,theY);
        }
        

        
    }
    bool CheckIfKill(GameObject objectOfInterest)
    {
        Vector3 pos = objectOfInterest.transform.position;
		Vector2 projectedPos = Camera.main.WorldToViewportPoint(pos);
        if (viewBounds.Contains(projectedPos))
        {   
            return true;
        }
        else
        {
            return false;
        }
    }
    private Vector3 calculateWorldPosition(Vector3 position, Camera camera) 
    {  
        //if the point is behind the camera then project it onto the camera plane
        Vector3 camNormal = camera.transform.forward;
        Vector3 vectorFromCam = position - camera.transform.position;
        float camNormDot = Vector3.Dot (camNormal, vectorFromCam.normalized);
        if (camNormDot <= 0f) {
            //we are beind the camera, project the position on the camera plane
            float camDot = Vector3.Dot (camNormal, vectorFromCam);
            Vector3 proj = (camNormal * camDot * 1.01f);   //small epsilon to keep the position infront of the camera
            position = camera.transform.position + (vectorFromCam - proj);
        }

        return position;
    }
}
