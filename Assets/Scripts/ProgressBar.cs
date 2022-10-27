using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxValue(int _value)
    {
        slider.maxValue = _value;
    }

    public void SetMaxValue(float _value)
    {
        slider.maxValue = _value;
    }

    public void SetValue(int _value)
    {
        slider.value = _value;
    }

    public void SetValue(float _value)
    {
        slider.value = _value;
    }

    public void AddValue(int _value)
    {
        slider.value += _value;
    }

    public void AddValue(float _value)
    {
        slider.value += _value;
    }
}
