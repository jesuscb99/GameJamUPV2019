using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject projectile;
    public Map_generator generator;

    public float shoot_freq = 5;

    private GameObject camara;

    private IDictionary<string, Color> color_dict = new Dictionary<string, Color>()
                                            {
                                                {"Red",Color.red},
                                                {"Yellow", Color.black},
                                                {"Green",Color.green},
                                                {"Blue", Color.blue}
                                            };

    private string color_name = "Red";
    private Material color;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Instantiate(enemy);

        camara = GameObject.Find("Main Camera");

        enemy.transform.parent = camara.transform;

        enemy.transform.position = new Vector3(camara.transform.position.x - camara.transform.position.z, 0, 0);

        InvokeRepeating("Shoot", shoot_freq, shoot_freq);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        print("Shot");

        float height = generator.road_heights.ElementAt(Random.Range(0,generator.roadsNum));

        projectile = GameObject.Instantiate(projectile);

        color = new Material(Shader.Find(" Diffuse"));

        projectile.GetComponent<Projectile_script>().color_name = color_name;

        projectile.GetComponent<SpriteRenderer>().material = color;

        projectile.transform.position = new Vector3(enemy.transform.position.x, height+generator.carHeight,0);
    }
        
        }
