using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Exception = System.Exception;

public class Car_control : MonoBehaviour
{
    public Map_generator generator;
    public string color_name = "Red";

    public Material color;

    private GameObject[] lasers;

    public float punctuation; 

    private IDictionary<string, Color> color_dict = new Dictionary<string, Color>()
                                            {
                                                {"Red",Color.red},
                                                {"Yellow", Color.black},
                                                {"Green",Color.green},
                                                {"Blue", Color.magenta}
                                            };
    private IDictionary<string, int> layer_dict = new Dictionary<string, int>()
                                            {
                                                {"Red",9},
                                                {"Yellow", 10},
                                                {"Green",11},
                                                {"Blue", 12}
                                            };

    public List<string[]> players_contollers = new List<string[]>{ new string[] { "w", "a", "s", "d" },
                                                               new string[] { "up", "left", "down", "right"},
                                                               new string[] { "i", "j", "k", "l"} };

    public float positiony;

    public int player_num = -1;
    string player_str;

    // Start is called before the first frame update
    void Start()
    {
        color = new Material(Shader.Find(" Diffuse"));

        color.SetColor("_Color", color_dict[color_name]);

        this.transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>().material = color;

        while(player_num == -1) { }

        gameObject.layer = 13 + player_num;
        foreach(Transform child in transform)
        {
            child.gameObject.layer = 13 + player_num;
        } 

        if (player_num == 0) { player_str = ""; }
        else { player_str = (1 + player_num).ToString(); }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("P"+player_num.ToString(), this.transform.GetChild(2).transform.position.x);
        print(PlayerPrefs.GetFloat("P" + player_num.ToString()));
        if(transform.childCount < 2) { Destroy(this.gameObject); }

        foreach (string laser in color_dict.Keys)
        {
            enable_colliders(laser.Equals(color_name), laser);
        }

        if (Input.GetKeyDown(players_contollers[player_num][0]) && color_name != "Red")
        {
            color.SetColor("_Color", color_dict["Red"]);
            enable_colliders(false, color_name);
            color_name = "Red";
            enable_colliders(true, "Red");
        }
        else if (Input.GetKeyDown(players_contollers[player_num][2]) && color_name != "Blue")
        {
            color.SetColor("_Color", color_dict["Blue"]);
            enable_colliders(false, color_name);
            color_name = "Blue";
            enable_colliders(true, "Blue");
        }
        else if (Input.GetKeyDown(players_contollers[player_num][1]) && color_name != "Yellow")
        {
            color.SetColor("_Color", color_dict["Yellow"]);
            enable_colliders(false, color_name);
            color_name = "Yellow";
            enable_colliders(true, "Yellow");
        }
        else if (Input.GetKeyDown(players_contollers[player_num][3]) && color_name != "Green")
        {
            color.SetColor("_Color", color_dict["Green"]);
            enable_colliders(false, color_name);
            color_name = "Green";
            enable_colliders(true, "Green");
        }
    }

    void enable_colliders(bool enable, string tag)
    {
        lasers = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject laser in lasers)
        {
            Physics2D.IgnoreLayerCollision(13+player_num, layer_dict[tag], !enable);
        }
    }
}
