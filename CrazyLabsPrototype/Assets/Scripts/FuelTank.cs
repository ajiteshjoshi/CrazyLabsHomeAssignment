using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelTank : MonoBehaviour
{
    private PlayerManager playerManager;
    private AudioSource audioSource;
    [SerializeField] private float fuelAmt;
    [SerializeField] private ParticleSystem fuel;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            playerManager.AddFuel(fuelAmt);
            audioSource.Play();
            fuel.Play();
        }
    }
}
