using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FENNotations
{

    public static string defaultFENNotation = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

    public void ResetBoard(GameObject board) {
        int tilesCount = board.transform.childCount;

        for ( int i = 0; i < tilesCount; i++ ) {
            GameObject tile = board.transform.GetChild(i).gameObject;
            TileManager tileManager = tile.GetComponent<TileManager>();
            tileManager.setPiece(PieceColor.None, PieceType.None);
        }
    }

    public void FENNotationToBoard(GameObject board, string notationString) {
        string[] rows = notationString.Split('/');
        for (int i = 0; i < rows.Length; i++) {
            string row = rows[i];
            int j = 0;

            foreach (char c in row) {

                if (char.IsDigit(c)) {
                    j += (int) char.GetNumericValue(c);
                    continue;
                }

                PieceColor color = StaticHelpers.isUppercase(c.ToString()) ? PieceColor.White : PieceColor.Black;

                PieceType type = PieceType.Pawn;

                switch (c.ToString().ToLower()) {
                    case "r":
                        type = PieceType.Rook;
                        break;

                    case "n":
                        type = PieceType.Night;
                        break;

                    case "b":
                        type = PieceType.Bishop;
                        break;

                    case "q":  
                        type = PieceType.Queen;
                        break;

                    case "k":
                        type = PieceType.King;
                        break;

                    default:    
                        type = PieceType.Pawn;
                        break; 
                }

                GameObject tile = board.transform.GetChild(i * 8 + j).gameObject;
                TileManager tileManager = tile.GetComponent<TileManager>();
                tileManager.setPiece(color, type);
                
                j += 1;
            }
        }
    }

    


}
