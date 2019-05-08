using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderCarrils : MonoBehaviour
{
    public Variables v;
    public Slider slider;
    public Text texto;

    void Update()
    {
        int valor = (int)(slider.value);
        texto.text = valor.ToString();
        v.setCarrils(valor);
    }
}
