using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

using Exception = System.Exception;

public class Final : MonoBehaviour
{
    public Dictionary<string,float> dict = new Dictionary<string,float>();
    // Start is called before the first frame update
    void Start()
    {
        try
        { 
            for (int player_num = 0; player_num < 2; player_num++)
            {
                dict.Add("Player"+player_num.ToString(), PlayerPrefs.GetFloat("P"+player_num.ToString()));
            }
        }catch(Exception _){ }

        var l = dict.OrderBy(value => value.Value);
        var dic = l.ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);

        KeyValuePair<string, float>[] item = dic.ToArray();

        CreateText(this.transform, -130f, -80f, item[0].ToString().Replace("[","").Replace("]",""), 18, Color.white);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject CreateText(Transform canvas_transform, float x, float y, string text_to_print, int font_size, Color text_color)
    {
        GameObject UItextGO = new GameObject("Text2");
        UItextGO.transform.SetParent(canvas_transform);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();
        trans.anchoredPosition = new Vector2(x, y);

        Text text = UItextGO.AddComponent<Text>();
        text.text = text_to_print;
        text.fontSize = font_size;
        text.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        text.color = text_color;

        return UItextGO;
    }
}
