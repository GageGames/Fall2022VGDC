using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum pullPushState
{
    none,
    pulling,
    pushing
}
public class playerMovementTest : MonoBehaviour
{
    public static pullPushState ppState;
    public GameObject pullPushTarget;
    private Rigidbody RB;

    [SerializeField] float pullStrength;
    [SerializeField] float pushStrength;

    [SerializeField] float baseDrag;
    [SerializeField] float dragWhilePulling;
    [SerializeField] float dragWhilePushing;
    // Start is called before the first frame update
    void Awake() {
        RB = GetComponent<Rigidbody>();  
    }
    void Start()
    {
        RB.drag = baseDrag;
    }

    // Update is called once per frame
    void Update()
    {
        
        HandleInputsUpdate();
        CheckAndExecutePushPullUpdate();
        
    }
    void HandleInputsUpdate()
    {
        //PULL
        if(Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                if(hit.transform.tag == "wall")
                {
                    print("pulling");
                    pullPushTarget = hit.transform.gameObject;
                    ppState = pullPushState.pulling;    
                    RB.drag = dragWhilePulling;
                }
            }
        }

        //PUSH
        else if(Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                
                if(hit.transform.tag == "wall")
                {
                    print("pushing");
                    pullPushTarget = hit.transform.gameObject;
                    ppState = pullPushState.pushing;
                    RB.drag = dragWhilePushing;
                }
            }
        }
    }

    void CheckAndExecutePushPullUpdate()
    {
        //check states
        if(ppState == pullPushState.pulling && Input.GetMouseButton(0))
        {
            //They clicked something they can pull, and are holding down, so pullll
            RB.AddForce(Vector3.Normalize(pullPushTarget.transform.position - transform.position)* pullStrength * Time.deltaTime);
        }
        else if(ppState == pullPushState.pushing && Input.GetMouseButton(1))
        {
            //They clicked something they can pull, and are holding down, so pullll
            RB.AddForce(Vector3.Normalize(transform.position - pullPushTarget.transform.position)* pushStrength * Time.deltaTime);
        }
        else
        {
            ppState = pullPushState.none;
            pullPushTarget = null;
            RB.drag = baseDrag;
        }
        
    }
}
