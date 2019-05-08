using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

using Exception = System.Exception;

public class camera_script : MonoBehaviour
{
    public Map_generator generator;
    public float speed;

    public List<GameObject> players = new List<GameObject>();
    public float roadLength;

    private bool secure = true;

    private GameObject[] death = new GameObject[] { };
    // Start is called before the first frame update
    void Start()
    {
        while (players.Count == 0)
        {
            players = generator.players_list;
        }
        death = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.name == "Death").ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        foreach (GameObject death_obj in death) { death_obj.transform.position += new Vector3(speed * Time.deltaTime, 0, 0); }
        if (secure && Mathf.Round(this.transform.position.x) % roadLength == 0)
        {
            secure = false;
            generator.generate_map();
        }
        else { secure = true; }

        List<float> ypos = new List<float>();

        List<GameObject> tmp = new List<GameObject>();
        foreach (GameObject player in players)
        {
            try
            {
                if (player == null) { continue; }
                else if (player.transform.GetChild(2).transform.position.x > this.transform.position.x)
                {
                    this.transform.position = new Vector3(player.transform.GetChild(2).transform.position.x,
                                                this.transform.position.y, this.transform.position.z);
                }
                ypos.Add(player.transform.GetChild(2).transform.position.y);
                tmp.Add(player);
            }
            catch (Exception _) { continue; }
        }
        players = tmp;
        tmp = new List<GameObject>();
        if (ypos.Count > 1)
        {
            this.transform.position = new Vector3(this.transform.position.x, ypos.Sum() / ypos.Count,
                                                   ypos.Max() - ypos.Min() > 5? - (ypos.Max() - ypos.Min()) * 2.5f:-12);
        }
        else if (ypos.Count == 1)
        {

            this.transform.position = new Vector3(this.transform.position.x, players[0].transform.GetChild(2).transform.position.y, -12);
        }
        else { SceneManager.LoadScene(2); }
    }  
}
