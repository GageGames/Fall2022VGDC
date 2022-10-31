using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GageProtoPlayer : MonoBehaviour
{
    public int keysNeeded;
    public int keysCount = 0;
    [SerializeField] TextMeshProUGUI keysText;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        keysText.text= "Keys: "+keysCount+"/"+keysNeeded;
    }

    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.tag == "key")
        {
            keysCount += 1;
            Destroy(other.gameObject);
        }
    }
}
