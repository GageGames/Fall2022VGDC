using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Gun))]
public class GageProtoTetherEffect : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] GameObject tetherBeamPullEffectPrefab;
    [SerializeField] GameObject tetherBeamPushEffectPrefab;
    [SerializeField] GameObject tetherHitPullEffectPrefab;
    public GameObject currentTetherBeamEffect;
    // Update is called once per frame
    // private void OnEnable()
    // {
    //     gun.OnFire.AddListener(SpawnTetherHitEffect);
    // }

    // private void OnDisable()
    // {
    //     gun.OnFire.RemoveListener(SpawnTetherHitEffect);
    // }


    void FixedUpdate()
    {
        print(gun.ActiveTether);
        if (gun.ActiveTether != null)
		{
            if(currentTetherBeamEffect != null)
            {
                UpdateTetherEffectPoints();
            }
            else
            {
                if(gun.ActiveTether.Strength >= 0){
                    currentTetherBeamEffect = Instantiate(tetherBeamPullEffectPrefab,Vector3.zero,Quaternion.identity) as GameObject;
                }
                else
                {
                    currentTetherBeamEffect = Instantiate(tetherBeamPushEffectPrefab,Vector3.zero,Quaternion.identity) as GameObject;
                }
                UpdateTetherEffectPoints();
            }
        }
        else
        {
            if(currentTetherBeamEffect != null)
            {
                Destroy(currentTetherBeamEffect);
            }
        }
    }
    void UpdateTetherEffectPoints()
    {
        currentTetherBeamEffect.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(0, gun.ActiveTether.Sender.Position);
        currentTetherBeamEffect.transform.GetChild(0).GetComponent<LineRenderer>().SetPosition(1, gun.ActiveTether.Recipient.Position);
    }

    // void SpawnTetherHitEffect(FireResult result)
    // {
    //     if(result.)
    //     GameObject hitEffect = Instantiate(tetherHitPullEffectPrefab, result.SelectedTarget.Position, Quaternion.identity) as GameObject;
    //     var dir = transform.position - result.SelectedTarget.Position; //a vector pointing from pointA to pointB
    //     var rot = Quaternion.LookRotation(dir, Vector3.up); //calc a rotation that
    //     hitEffect.transform.rotation = rot;
    // }
}
