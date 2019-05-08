using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_script : MonoBehaviour
{
    public string color_name = "";
    public float speed = 2;

    private IDictionary<string, int> layer_dict = new Dictionary<string, int>()
                                            {
                                                {"Red",9},
                                                {"Yellow", 10},
                                                {"Green",11},
                                                {"Blue", 12}
                                            };

    private IDictionary<string, Color> color_dict = new Dictionary<string, Color>()
                                            {
                                                {"Red",Color.red},
                                                {"Yellow", Color.black},
                                                {"Green",Color.green},
                                                {"Blue", Color.gray}
                                            };

    private Material color;

    private bool move = false;

    // Start is called before the first frame update
    void Start()
    {
        while(color_name.Equals(""))
        {
        }
        this.gameObject.layer = layer_dict[color_name];

        color = new Material(Shader.Find(" Diffuse"));

        color.SetColor("_Color", color_dict[color_name]);

        this.GetComponent<SpriteRenderer>().material = color;

        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }
    }

    void enable_colliders(bool enable, string tag)
    {
        GameObject[] lasers = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject laser in lasers)
        {
            Physics2D.IgnoreLayerCollision(layer_dict[color_name], layer_dict[tag], !enable);
        }
    }
}
