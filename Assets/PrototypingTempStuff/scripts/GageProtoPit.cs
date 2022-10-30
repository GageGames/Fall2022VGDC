using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GageProtoPit : MonoBehaviour
{
    [SerializeField]
    float pitDamagePerSecond;

    [SerializeField]
    float tickLength;

    float tickTime = 0.0f;

    List<GameObject> stuffInPit = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        tickTime += Time.fixedDeltaTime;
        if(tickTime > tickLength)
        {
            foreach(var obj in stuffInPit){
                if(obj != null){
                    obj.GetComponent<GageProtoInteractsWithDamage>().health -= tickTime * pitDamagePerSecond;
                    obj.GetComponent<GageProtoInteractsWithDamage>().renderer.material.DOColor(Color.black, tickTime/2.1f).SetLoops(2, LoopType.Yoyo);
                }
            }
            
            tickTime = 0.0f;
        }
    }
    void OnTriggerEnter(Collider other) {
        print(this.gameObject);
        print(other.gameObject);
        if (other.gameObject.GetComponent<GageProtoInteractsWithDamage>() != null)
        {
            stuffInPit.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other) {
        stuffInPit.Remove(other.gameObject);
    }
}
