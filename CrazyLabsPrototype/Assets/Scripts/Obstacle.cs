using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerManager playerManager;
    private AudioSource audioSource;
    [SerializeField] private ParticleSystem[] collide;
    private cam cameraCtrl;
    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
        cameraCtrl = FindObjectOfType<cam>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerManager.DecreaseSpeed();
            audioSource.Play();
            foreach (ParticleSystem fx in collide)
            {
                fx.Play();
            }
        }
    }
}
