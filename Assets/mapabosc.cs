using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapabosc : MonoBehaviour
{
    public Variables v;
    public RawImage este;
    public RawImage otro;

    // Start is called before the first frame update
   
    public void Click()
    {
        este.color = Color.green;
        otro.color = Color.grey;

        v.setEscena(2);


    }
}

