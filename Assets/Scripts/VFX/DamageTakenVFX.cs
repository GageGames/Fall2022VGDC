using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(HealthEntity))]
public class DamageTakenVFX : MonoBehaviour
{
    static float damageEffectDuration = 0.15f;
    HealthEntity HE;
    Sequence damageSequenceGroup;
    bool isRunning = false;

    void Awake()
    {
        HE = this.gameObject.GetComponent<HealthEntity>();
        damageSequenceGroup = DOTween.Sequence().SetAutoKill(false).OnComplete(DamageEffectComplete);
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            if(renderer != null && renderer.material.HasProperty("_Color"))
            {
                damageSequenceGroup.Join(renderer.material.DOColor(Color.red, damageEffectDuration).SetEase(Ease.InOutSine).SetLoops(2,LoopType.Yoyo));
            }
        }
        damageSequenceGroup.Pause();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        HE.OnDamage.AddListener(DamageEffect);
    }

    private void OnDisable()
    {
        HE.OnDamage.RemoveListener(DamageEffect);
    }

    void DamageEffect()
    {
        if(isRunning == false)//only do the tween if a previous bit of damage isnt already running the effect
        {
            print("Start Tween");
            isRunning = true;
            damageSequenceGroup.Restart();
        }
    }
    public void DamageEffectComplete()
    {
        isRunning = false;
        print("tweenFinished");
    }
}
