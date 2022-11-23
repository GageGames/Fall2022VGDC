using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageBorder : MonoBehaviour
{
    [SerializeField] HealthData PlayerHD;
    [SerializeField] GameObject damageBorder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetAlpha = 0.0f;
        if(PlayerHD.CurrentHealth < PlayerHD.MaxHealth/2.0f)
        {
            targetAlpha = ((0.5f-(PlayerHD.CurrentHealth/PlayerHD.MaxHealth)) * 2);
        }
        damageBorder.GetComponent<Image>().color = new Color(damageBorder.GetComponent<Image>().color.r,damageBorder.GetComponent<Image>().color.g,damageBorder.GetComponent<Image>().color.b, targetAlpha);
    }
}
