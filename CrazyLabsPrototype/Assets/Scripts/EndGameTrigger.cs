using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private CarSceneManagaer carSceneManagaer;

    private void Start()
    {
        carSceneManagaer = FindObjectOfType<CarSceneManagaer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            carSceneManagaer.GameEnd(true);
            Debug.Log("GameEnd");
        }
    }
}
