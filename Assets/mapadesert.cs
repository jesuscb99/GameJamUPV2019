using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapadesert : MonoBehaviour
{
    public Variables v;
    public RawImage este;
    public RawImage otro;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        este.color = Color.green;
        otro.color = Color.grey;
        v.setEscena(1);
    }
}
