using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetMaxVal(int val)
    {
        slider.maxValue = val;
    }

    public void SetValue(int val)
    {
        slider.value = val;
    }
}
