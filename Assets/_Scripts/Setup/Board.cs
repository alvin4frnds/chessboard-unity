using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public GameObject boardPrefab;
    public GameObject tilePrefab;

    private GameObject board;

    private Color greenColor;

    // Start is called before the first frame update
    void Start()
    {
        
        this.initiateVariables();
        this.drawBoard();

        this.setInitialPieces();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setInitialPieces() {
        FENNotations fen = new FENNotations();

        fen.FENNotationToBoard(board, FENNotations.defaultFENNotation);
    }

    void initiateVariables() {
        greenColor = StaticHelpers.fromHex(Constants.Color_boardGreen);
    }

    public string tileNameByCoordinates(float x, float y) {
        string tileName = "";


        for ( int i = 0; i < board.transform.childCount; i++ ) {
            GameObject tile = board.transform.GetChild(i).gameObject;
            if ( x > tile.transform.position.x - Constants.TileSize / 2 && x < tile.transform.position.x + Constants.TileSize / 2 && y > tile.transform.position.y - Constants.TileSize / 2 && y < tile.transform.position.y + Constants.TileSize / 2 ) {
                tileName = tile.name;
                break;
            }
        }

        return tileName;
    }

    void drawBoard() {
        Debug.Log("Drawing board");

        board = Instantiate(boardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        board.transform.position = new Vector3(0, 0, 2);
        board.name = "Board";
        board.SetActive(true);
        board.transform.localScale = new Vector3(Constants.BoardSize, Constants.BoardSize, 1);

        for ( int x = 0; x < 8; x++ ) {
            for ( int y = 0; y < 8; y++ ) {
                // calculate the position of the tile
                float xPos = (float)(-Constants.TileOffset + (x * Constants.TileSize));
                float yPos = (float)(-Constants.TileOffset + (y * Constants.TileSize));

                GameObject tile = Instantiate(tilePrefab, new Vector3(xPos, yPos, 0), Quaternion.identity);

                tile.name = "Tile#" + x + "," + y;
                tile.tag = "Tile";
                tile.SetActive(true);
                bool isDark =  (x + y) % 2 == 0;
                tile.layer = LayerMask.NameToLayer("Board");
                tile.transform.localScale = new Vector3 (Constants.TileSize, Constants.TileSize, 1);

                Renderer tileRenderer = tile.GetComponent<Renderer>();
                SpriteRenderer sprite = tile.GetComponent<SpriteRenderer>();
                tile.transform.parent = board.transform;

                TileManager tileManager = tile.GetComponent<TileManager>();
                tileManager.setVariables(x, y, isDark);

                sprite.color = isDark ? Color.white : greenColor;
            }
        }

    }
}
