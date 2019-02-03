using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static Slider slider;

    private void Awake()
    {
        slider = this.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
    }

    public static void ChangeSliderValue(float value)
    {
        slider.value = value;
    }
    
}
