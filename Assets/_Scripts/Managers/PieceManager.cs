using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{

    PieceType type;
    PieceColor color;

    private Vector3 mousePositionOffset;
    private string lastBoardPosition = "";
    private string currentBoardPosition = "";


    public string imagePath = "";

    private Vector3 GetMouseWorldPosition() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public void resetSprite() {
        this.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void setVariables(PieceType type, PieceColor color) {
        this.type = type;
        this.color = color;

        if ( this.type == PieceType.None ) {
            this.name = "None";
            this.resetSprite();
            return;
        }

        this.name = color.ToString() + " " + type.ToString();
        this.loadPiece();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        Debug.Log("Mouse down");

        mousePositionOffset = this.transform.position - GetMouseWorldPosition();
        lastBoardPosition = this.getPositionOnBoard();
    }

    private void OnMouseDrag() {
        Debug.Log("Mouse drag");

        this.transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUp() {
        Debug.Log("Mouse up: " + this.transform.position);

        GameObject logicGameObject = GameObject.Find("Logic");
        Board board = logicGameObject.GetComponent<Board>();
        string tileName = board.tileNameByCoordinates(this.transform.position.x, this.transform.position.y);

        Debug.Log("Tile name: " + tileName);
    }

    private void loadPiece() {
        string pieceName = this.color.ToString().ToLower().Substring(0, 1) + this.type.ToString().ToUpper().Substring(0, 1);

        this.imagePath = "Images/Pieces/pngs/" + pieceName;

        Texture2D texture = Resources.Load<Texture2D>(this.imagePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        // Set the position of the piece
        // this.transform.position = new Vector3(-0.5f, -0.5f, 0);
    }

    private string getPositionOnBoard() {
        return this.GetComponentInParent<TileManager>().getPosition();
    }
}

public enum PieceType {
    Pawn,
    Rook,
    Night,
    Bishop,
    Queen,
    King,
    None
}

public enum PieceColor {
    White,
    Black,
    None
}