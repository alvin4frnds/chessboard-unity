using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public int x = 0;
    public int y = 0;
    public bool isDark = false;

    private Color highLightColor;
    private Color orignalColor;

    public Constants Constants = new Constants();

    // Start is called before the first frame update
    void Start()
    {
        
        highLightColor = StaticHelpers.fromHex(Constants.Color_tileHighLight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getPosition() {
        return StaticHelpers.getTileName(x, y);
    }

    public void setVariables(int x, int y, bool isDark) {
        this.x = x;
        this.y = y;
        this.isDark = isDark;
    }

    public void setPiece(PieceColor color, PieceType type) {
        PieceManager pieceManager = this.GetComponentInChildren<PieceManager>();
        pieceManager.setVariables(type, color);
    }

    void OnMouseEnter() {
        Debug.Log("Mouse enter");

        this.orignalColor = this.GetComponent<SpriteRenderer>().color;
        this.GetComponent<SpriteRenderer>().color = highLightColor;
    }

    void OnMouseExit() {
        Debug.Log("Mouse leave");
        this.GetComponent<SpriteRenderer>().color = this.orignalColor;
    }
}
