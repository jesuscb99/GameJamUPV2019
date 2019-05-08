using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    public  int escena;
    public int jugadors;
    public int carrils;
  

    void Start()
    {
        escena = 2;
        jugadors = 1;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("carrils", carrils);
        PlayerPrefs.SetInt("jugadors", jugadors);
    }
    
    public void setEscena(int e)
    {
        escena = e;

    }

    public void setJugadors(int n)
    {
        jugadors = n;
    }

    public int getEscena()
    {
        return escena;
    }

    public int getJugadors()
    {
        return jugadors;
    }

    public void setCarrils(int n)
    {
        carrils = n;
    }

    public int getCarrils()
    {
        return carrils;
    }

}
