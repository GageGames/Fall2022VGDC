using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegen : MonoBehaviour
{
    HealthEntity HE;
    [SerializeField] float healDelay;
    [SerializeField] float healPerSecond;
    float timeSinceDamage;
    void Awake()
    {
        HE = GetComponent<HealthEntity>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(timeSinceDamage > healDelay)
        {
            HE.ApplyHeal(healPerSecond*Time.deltaTime);
        }
        timeSinceDamage += Time.deltaTime;
    }
    private void OnEnable()
    {
        HE.OnDamage.AddListener(DamageTaken);
    }

    private void OnDisable()
    {
        HE.OnDamage.RemoveListener(DamageTaken);
    }

    public void DamageTaken()
    {
        timeSinceDamage = 0.0f;
    }
}
