using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class play : MonoBehaviour
{
    // Start is called before the first frame update
   // public Variables v;
    public Button boto;
    public Sprite boto2;

    public void openScene1()

    {
        boto.image.sprite = boto2;
        Invoke("obrir", 1f);
    }

    public void obrir()
    {
        //Debug.Log(v.getEscena());
        //if(v.getEscena() == 1)
        //{
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //}
        // else
        // {
        //      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        //  }

    }
}

