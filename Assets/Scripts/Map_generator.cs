using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Exception = System.Exception;
using System.Linq;

public class Map_generator : MonoBehaviour
{
    public bool button = false;
    public float test;
    // Map settings
    public int roadsNum = 3;
    public int players = 1;
    public float carHeight;
    public float roadHeight;
    public float roadLength;
    public int startingLength = 5;
    public GameObject mark;


    public float ramp_angle;

    // Gameobjects used when generating the map
    public List<GameObject> roads;
    public List<GameObject> bridges_red;
    public List<GameObject> bridges_yellow;
    public List<GameObject> bridges_blue;
    public List<GameObject> bridges_green;
    public GameObject camara;
    public GameObject deathRay;
    public List<GameObject> players_list;
    public GameObject player;

    public List<List<GameObject>> bridges = new List<List<GameObject>>();

    // In-game options
    public int mapScene;

    private float road_position;
    public IEnumerable<float> road_heights;
    private List<int> last_colours = new List<int>();
    
    // Start is called before the first frame downdate
    void Start()
    {
        try
        {
            players = PlayerPrefs.GetInt("jugadors");
            roadsNum = PlayerPrefs.GetInt("carrils");
        }
        catch (Exception _) { }

        road_position = 0;

        road_heights = from roadnum in Enumerable.Range(0, roadsNum) select (carHeight + roadHeight) * roadnum;
        bridges.Add(bridges_blue);
        bridges.Add(bridges_red);
        bridges.Add(bridges_green);
        bridges.Add(bridges_yellow);


        for (int player_num = 0; player_num < players; player_num++)
        {
            GameObject new_player = GameObject.Instantiate(player);
            new_player.GetComponent<Car_control>().player_num = player_num;
            new_player.transform.position = new Vector3(road_position, road_heights.ElementAt(player_num) + carHeight, 0);
            players_list.Add(new_player);
        }

        camara = GameObject.Find("Main Camera");

        camara.GetComponent<camera_script>().players = players_list;
        camara.GetComponent<camera_script>().roadLength = roadLength;
        camara.transform.position = new Vector3(road_position + 2 * carHeight, (carHeight + roadHeight) * players_list.Count / 2, -(players_list.Count) * (carHeight + roadHeight - 2));

        deathRay = GameObject.Find("Death");
        deathRay.transform.position = new Vector3(-(road_position + 2 * carHeight + (carHeight + roadHeight) * players_list.Count / 2 + (players_list.Count) * (carHeight + roadHeight - 2)), -6, 0);

        for (int i = 0; i < startingLength; i++) {
            test++;
            for (int road = 0; road < roadsNum; road++){
                generate_asset(0, road, false);
            }
            road_position += roadLength;
        }

        ramp_angle = Mathf.Rad2Deg * Mathf.Atan((carHeight+roadHeight) / roadLength);

        button = true;
    }

    // Update is called once per frame
    void Update()
    {
        //test = road_position;
        if (button) {
            button = false;
            generate_map();
            generate_map();
            generate_map();
            generate_map();
            generate_map();
        }
    }

    // Generates one asset for each road
    public void generate_map()
    {
        GameObject new_mark = GameObject.Instantiate(mark);
        new_mark.transform.position = new Vector3(road_position, road_heights.ElementAt(0) - carHeight, 0);

        int[] perc_list = new int[] { 10, 1, 0, 4, 2, 0, 3, 0, 0, 0 };
        List<int> assets = new List<int>(new int[] {rand_perc(perc_list)});
        bool down = generate_asset(assets[0], 0, false);
        for (int road = 1; road < roadsNum; road++)
        {
            perc_list = new int[] {down ? 0:70, 5, 1, 20, 10, down ? 0:10, 15, down ? 0:10, down ? 0:10, down ? 0:10};
            assets.Add(rand_perc(perc_list));
            down = generate_asset(assets[assets.Count() - 1], road, down);
            //test++;
        }
        road_position += roadLength;
        last_colours = new List<int>();

    }

    // Generates a new asset for the road given,
    // returns false if hasnt a ramp going up
    bool generate_asset(int asset_num, int road, bool down)
    {
        GameObject asset;
        float road_height = road_heights.ElementAt(road);
        switch (asset_num)
        {
            case 0:
                // Straight road
                asset = generate_asset(roads, "Straight");
                asset.transform.position = new Vector3(road_position, road_height, 0);
                return false;
            case 1:
                // Up road
                asset = generate_asset(roads, "Up");
                asset.transform.position = new Vector3(road_position, road_height + (carHeight + roadHeight) / 2, 0);
                // Scale?
                return true;
            case 2:
                return false;
            case 3:
                // Straight bridge
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if(last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Straight");
                asset.transform.position = new Vector3(road_position, road_height, 0);
                return false;
            case 4:
                // Up bridge
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Up");
                asset.transform.position = new Vector3(road_position, road_height + (carHeight + roadHeight) / 2, 0);
                // Scale?
                return true;
            case 5:
                // Down bridge
                if (down) { break; }
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Down");
                asset.transform.position = new Vector3(road_position, road_height - (carHeight + roadHeight) / 2, 0);
                // Scale?
                return false;
            case 6:
                // Straight Up bridge
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Straight");
                asset.transform.position = new Vector3(road_position, road_height, 0);
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Up");
                asset.transform.position = new Vector3(road_position, road_height + (carHeight + roadHeight) / 2, 0);
                // Scale?
                return true;
            case 7:
                // Straight Down bridge
                if (down) { break; }
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Straight");
                asset.transform.position = new Vector3(road_position, road_height, 0);
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Down");
                asset.transform.position = new Vector3(road_position, road_height - (carHeight + roadHeight) / 2, 0);
                // Scale?
                return false;
            case 8:
                // Up Down Funk you down
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Up");
                asset.transform.position = new Vector3(road_position, road_height + (carHeight + roadHeight) / 2, 0);
                if (down)
                {
                    last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0 ? 0:1)));
                    if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                    {
                        last_colours[last_colours.Count() - 1] += 1;
                    }
                    asset = generate_asset(bridges[last_colours[last_colours.Count() - 1]], "Down");
                    asset.transform.position = new Vector3(road_position, road_height - (carHeight + roadHeight) / 2, 0);
                }
                return true;
            case 9:
                // Straight Up Down bridge
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Straight");
                asset.transform.position = new Vector3(road_position, road_height, 0);
                last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0? 0:1)));
                if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                {
                    last_colours[last_colours.Count() - 1]+=1;
                }
                asset = generate_asset(bridges[last_colours[last_colours.Count()-1]], "Up");
                asset.transform.position = new Vector3(road_position, road_height + (carHeight + roadHeight) / 2, 0);

                if (down)
                {
                    last_colours.Add(Random.Range(0, bridges.Count() - (last_colours.Count() == 0 ? 0:1)));
                    if (last_colours.Count > 1 && last_colours[last_colours.Count() - 1] == last_colours[last_colours.Count() - 2])
                    {
                        last_colours[last_colours.Count() - 1] += 1;
                    }
                    asset = generate_asset(bridges[last_colours[last_colours.Count() - 1]], "Down");
                    asset.transform.position = new Vector3(road_position, road_height - (carHeight + roadHeight) / 2, 0);
                }
                return true;
        }

        return true;
    }

    // que mal li he posat el mateix nom >.<
    GameObject generate_asset(List<GameObject> asset_type, string rotation)
    {
        GameObject asset = GameObject.Instantiate(asset_type[mapScene]) as GameObject;
        if(rotation.Equals("Up")) {
            asset.transform.Rotate(0, 0, ramp_angle);
            asset.transform.localScale = new Vector3((carHeight) / Mathf.Sin(ramp_angle * Mathf.Deg2Rad) * 1.3f, roadHeight, asset.transform.localScale.z);
        }
        else if(rotation.Equals("Down")) {
            asset.transform.Rotate(0, 0, -ramp_angle);
            asset.transform.localScale = new Vector3((carHeight) / Mathf.Sin(ramp_angle * Mathf.Deg2Rad) * 1.3f, roadHeight, asset.transform.localScale.z);
        }
        else { asset.transform.localScale = new Vector3(roadLength, roadHeight, asset.transform.localScale.z); }
        return asset;
    }

    int rand_perc(int[] perc_list)
    {
        int num = Random.Range(0, perc_list.Sum());
        for(int percent_num = 0; percent_num < perc_list.Count(); percent_num++)
        {
            num -= perc_list[percent_num];
            if(num <= 0) { return percent_num; }
        }
        return -1;
    }
}
