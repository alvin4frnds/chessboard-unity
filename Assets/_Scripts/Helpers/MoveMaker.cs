using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveMaker
{
    GameObject board;
    GameVars gameVars;
    TMP_Text titleText;

    public void setBoard(GameObject board) {
        this.board = board;
    }

    public void setTitleText(TMP_Text titleText) {
        this.titleText = titleText;
    }

    public void setGameVars(GameVars gameVars) {
        this.gameVars = gameVars;
    }

    public void makeMove(string move, Piece piece) {
        // Handle game vars based on piece type
        gameVars.nextTurn = piece.color == PieceColor.White ? PieceColor.Black : PieceColor.White;
        if ( piece.type == PieceType.King ) {
            if ( piece.color == PieceColor.White ) {
                gameVars.isWhiteKingMoved = true;
            } else {
                gameVars.isBlackKingMoved = true;
            }
        } else if ( piece.type == PieceType.Rook ) {
            if ( piece.color == PieceColor.White ) {
                if ( move.Contains("a1")) {
                    gameVars.isWhiteLeftRookMoved = true;
                } else if ( move.Contains("h1")) {
                    gameVars.isWhiteRightRookMoved = true;
                }
            } else {
                if ( move.Contains("a8")) {
                    gameVars.isBlackLeftRookMoved = true;
                } else if ( move.Contains("h8")) {
                    gameVars.isBlackRightRookMoved = true;
                }
            }
        }
        if ( move.Contains("x") || piece.type == PieceType.Pawn) {
            gameVars.halfMoveClock = 0;
        } else {
            gameVars.halfMoveClock += 1;
        }

        if ( piece.type == PieceType.Pawn ) {
            
            Debug.Log("En passant: " + piece.color + move);

            if ( move.Contains("x") ) {
                gameVars.enPassantTarget = "-";
            } else if ( move.Contains("2") && (piece.color == PieceColor.White) && move.Contains("4") ) {
                gameVars.enPassantTarget = move[move.Length - 2] + "4";
            } else if ( move.Contains("7") && (piece.color == PieceColor.Black) && move.Contains("5") ) {
                gameVars.enPassantTarget = move[move.Length - 2] + "5";
            }
        } else {
            gameVars.enPassantTarget = "-";
        }

        if ( piece.color == PieceColor.Black ) {
            gameVars.fullMoveNumber += 1;
        }


        string from = "" + move[0] + move[1];
        string to = "" + move[move.Length - 2] + move[move.Length - 1];

        Debug.Log("Move from " + from + " to " + to);
        titleText.text = "Move from " + from + " to " + to;
    }
}