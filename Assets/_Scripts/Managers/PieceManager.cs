using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PieceManager : MonoBehaviour
{

    public PieceType type;
    public PieceColor color;

    private Vector3 mousePositionOffset;
    private Vector3 previousPosition;
    private Vector3 startPosition;
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
        if ( type == this.type && color == this.color) {
            return;
        }

        if ( type != this.type || color != this.color) {
            this.resetSprite();
        }

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
        this.startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        Debug.Log("Mouse down");

        mousePositionOffset = this.transform.position - GetMouseWorldPosition();
        lastBoardPosition = this.getPositionOnBoard();

        this.previousPosition = this.transform.position;
    }

    private void OnMouseDrag() {
        this.transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    private void OnMouseUp() {
        Debug.Log("Mouse up: " + this.transform.position);

        GameObject logicGameObject = GameObject.Find("Logic");
        Board board = logicGameObject.GetComponent<Board>();
        string tileName = board.tileNameByCoordinates(this.transform.position.x, this.transform.position.y);

        GameObject parentTile = this.transform.parent.gameObject;

        if ( tileName == parentTile.name) {
            this.resetPosition();
            return;
        }

        this.transform.position = this.previousPosition;

        MoveMaker moveMaker = board.getMoveMaker();
        moveMaker.makeMove(lastBoardPosition + "-" + tileName);

        board.updatePieceOnTile(tileName, this.gameObject);
        TileManager tileManager = this.transform.parent.gameObject.GetComponent<TileManager>();
        tileManager.setPiece(PieceColor.None, PieceType.None);
    }

    private void loadPiece() {
        string pieceName = this.color.ToString().ToLower().Substring(0, 1) + this.type.ToString().ToUpper().Substring(0, 1);

        this.imagePath = "Images/Pieces/pngs/" + pieceName;

        Texture2D texture = Resources.Load<Texture2D>(this.imagePath);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    private string getPositionOnBoard() {
        return this.GetComponentInParent<TileManager>().getPosition();
    }

    private void resetPosition() {
        this.transform.position = this.previousPosition;
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(PieceManager))]

public class PieceManagerEditor: Editor {
    public override void OnInspectorGUI() {
        PieceManager pieceManager = (PieceManager)target;

        DrawDefaultInspector();

        if ( GUILayout.Button("Set Piece") ) {
            pieceManager.setVariables(pieceManager.type, pieceManager.color);
        }
    }
}

#endif

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