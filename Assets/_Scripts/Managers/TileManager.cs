using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public int x = 0;
    public int y = 0;
    public bool isDark = false;

    private Color highLightColor;
    private Color orignalColor;

    // Start is called before the first frame update
    void Start()
    {
        
        highLightColor = StaticHelpers.fromHex(Constants.Color_tileHighLight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter() {
        Debug.Log("Mouse enter");

        this.orignalColor = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = highLightColor;
    }

    void OnMouseExit() {
        Debug.Log("Mouse exit");
        this.GetComponent<SpriteRenderer>().color = this.orignalColor;
    }
}
