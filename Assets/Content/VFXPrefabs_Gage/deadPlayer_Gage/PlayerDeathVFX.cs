using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathVFX : MonoBehaviour
{
    [SerializeField] GameObject playerVisualStuff;
    [SerializeField] GameObject playerCollider;
    [SerializeField] GameObject playerModel;
    [SerializeField] GameObject deathVFXPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGameEnd()
    {
        playerVisualStuff.SetActive(false);
        playerCollider.SetActive(false);
        Instantiate(deathVFXPrefab, transform.position, playerModel.transform.rotation);
    }
}
