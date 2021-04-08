using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NitroBoost : MonoBehaviour
{
    private PlayerManager playerManager;
    private AudioSource audioSource;
    [SerializeField] private ParticleSystem nitro;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerManager.IncreaseSpeed();
            audioSource.Play();
            nitro.Play();
        }
    }
}
