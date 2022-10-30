using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GageProtoExplodeable : MonoBehaviour
{
    [SerializeField] float explosionSize;
    [SerializeField] float explosionMaxDamage;
    [SerializeField] float explosionMaxForce;
    [SerializeField] AnimationCurve explosionForceFallOffGraph;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Explode()
    {
        //does not need to handle destruction of itself just the blast
        Collider[] colls = Physics.OverlapSphere(transform.position, explosionSize);
        foreach(var coll in colls)
        {
            if(coll.gameObject.GetComponent<GageProtoInteractsWithDamage>() != false)
            {
                ExplosionOnOne(coll.gameObject);
            }
        }
    }

    void ExplosionOnOne(GameObject victim)
    {
        //calc it
        float distance = Vector3.Distance(victim.transform.position, transform.position);
        float explosionForce = explosionForceFallOffGraph.Evaluate(distance/explosionSize) * explosionMaxForce;
        float explosionDamage = explosionForceFallOffGraph.Evaluate(distance/explosionSize) * explosionMaxDamage;
        Vector3 forceDir = victim.transform.position - transform.position;
        forceDir.y = 0.0f;
        forceDir = forceDir.normalized;

        //apply it
        victim.GetComponent<GageProtoInteractsWithDamage>().rb.AddForce(forceDir * explosionForce, ForceMode.Impulse);
        victim.GetComponent<GageProtoInteractsWithDamage>().health -= explosionDamage;
    }

}
