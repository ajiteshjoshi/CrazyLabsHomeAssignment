using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float fuel, fuelMaxAmt;
    private Transform cardist;
    private float startdist;
    private CarSceneManagaer carSceneManagaer;
    private CarController carController;
    void Start()
    {
        carSceneManagaer = FindObjectOfType<CarSceneManagaer>();
        carController = FindObjectOfType<CarController>();
        cardist = FindObjectOfType<CarController>().transform;
        fuelMaxAmt = UIManager.Instance.FuelTankSlider.value;
        startdist = cardist.position.x;
    }

    
    void Update()
    {
        fuel -= Time.deltaTime;
        UIManager.Instance.UpdateFuelBar(fuel / fuelMaxAmt);
        UIManager.Instance.UpdateDistBar((cardist.position.x - startdist) / 1800f);
        if (fuel < 0)
        {
            carSceneManagaer.GameEnd(false);
        }
    }

    public void AddFuel(float fuelAmount)
    {
        if(fuel+fuelAmount>=fuelMaxAmt)
        {
            fuel = fuelMaxAmt;
        }
        else
        {
            fuel += fuelAmount;

        }
    }

    public void IncreaseSpeed()
    {
        carController.changeForceMultiplier(2);
    }

    public void DecreaseSpeed()
    {
        carController.changeForceMultiplier(0);
    }
}
