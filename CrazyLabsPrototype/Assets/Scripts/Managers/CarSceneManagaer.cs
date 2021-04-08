using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSceneManagaer : MonoBehaviour
{

    public void GameEnd(bool won)
    {
        GameManager.Instance.UnLoadLevel(1, won);
    }
}
