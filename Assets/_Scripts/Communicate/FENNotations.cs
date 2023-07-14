using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FENNotations
{

    GameVars gameVars = new GameVars();

    public static string defaultFENNotation = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR";

    public void ResetBoard(GameObject board) {
        int tilesCount = board.transform.childCount;

        for ( int i = 0; i < tilesCount; i++ ) {
            GameObject tile = board.transform.GetChild(i).gameObject;
            TileManager tileManager = tile.GetComponent<TileManager>();
            tileManager.setPiece(PieceColor.None, PieceType.None);
        }
    }

    public void setGameVars(GameVars gameVars) {
        this.gameVars = gameVars;
    }

    public string toFENNotation(GameObject board) {
        string FENNotation = "";

        for ( int i = 0; i < board.transform.childCount; i++ ) {
            GameObject tile = board.transform.GetChild(i).gameObject;
            GameObject piece = tile.transform.GetChild(0).gameObject;
            PieceManager pieceManager = piece.GetComponent<PieceManager>();

            if ( pieceManager.color == PieceColor.None ) {
                FENNotation += "1";
                continue;
            }

            string pieceNotation = "";

            switch (pieceManager.type) {
                case PieceType.Pawn:
                    pieceNotation = "p";
                    break;

                case PieceType.Rook:
                    pieceNotation = "r";
                    break;

                case PieceType.Night:
                    pieceNotation = "n";
                    break;

                case PieceType.Bishop:
                    pieceNotation = "b";
                    break;

                case PieceType.Queen:
                    pieceNotation = "q";
                    break;

                case PieceType.King:
                    pieceNotation = "k";
                    break;

                default:
                    pieceNotation = "1";
                    break;
            }

            if ( pieceManager.color == PieceColor.White ) {
                pieceNotation = pieceNotation.ToUpper();
            }

            FENNotation += pieceNotation;
        }

        /**
         * after 8 characters, add a slash
         */
        for ( int i = 8; i < FENNotation.Length; i += 9 ) {
            FENNotation = FENNotation.Insert(i, "/");
        }

        /**
         * count all 1's and replace them with the number of 1's
         */
        for ( int i = 8; i > 1; i-- ) {
            FENNotation = FENNotation.Replace(new string('1', i), i.ToString());
        }

        // Add gamevars into picture
        FENNotation += " " + ((gameVars.nextTurn == PieceColor.White) ? "w" : "b");
        FENNotation += " " + gameVars.castlingAvailablity();
        FENNotation += " " + gameVars.enPassantTarget;
        FENNotation += " " + gameVars.halfMoveClock;
        FENNotation += " " + gameVars.fullMoveNumber;


        return FENNotation;
    }

    public void FENNotationToBoard(GameObject board, string fullNotation) {
        string piecesNotation = fullNotation.Split(' ')[0];

        string[] rows = piecesNotation.Split('/');

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

        string[] gameVarsNotation = fullNotation.Split(' ');
        for ( int i = 1; i < gameVarsNotation.Length; i ++ ) {

            switch (i) {
                case 1:
                    gameVars.nextTurn = gameVarsNotation[i] == "w" ? PieceColor.White : PieceColor.Black;
                    break;

                case 2:
                    gameVars.isWhiteKingMoved = gameVarsNotation[i].Contains("K");
                    gameVars.isWhiteLeftRookMoved = gameVarsNotation[i].Contains("Q");
                    gameVars.isBlackKingMoved = gameVarsNotation[i].Contains("k");
                    gameVars.isBlackLeftRookMoved = gameVarsNotation[i].Contains("q");
                    break;

                case 3:
                    gameVars.enPassantTarget = gameVarsNotation[i];
                    break;

                case 4:
                    gameVars.halfMoveClock = int.Parse(gameVarsNotation[i]);
                    break;

                case 5:
                    gameVars.fullMoveNumber = int.Parse(gameVarsNotation[i]);
                    break;

                default:    
                    break;
            }

        }
    }

    


}
