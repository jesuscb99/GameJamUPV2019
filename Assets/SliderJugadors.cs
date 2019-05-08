using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderJugadors : MonoBehaviour
{
    public Variables v;
    public Slider slider;
    public Text texto;
   
    void Update()
    {
        texto.text = slider.value.ToString();
        int valor =  (int) (slider.value); 

        v.setJugadors(valor);
    }
}
