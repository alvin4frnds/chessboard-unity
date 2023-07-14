using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVars
{

    public PieceColor nextTurn = PieceColor.White;
    
    public bool isWhiteKingMoved = false;
    public bool isWhiteLeftRookMoved = false;
    public bool isWhiteRightRookMoved = false;
    public bool isBlackKingMoved = false;
    public bool isBlackLeftRookMoved = false;
    public bool isBlackRightRookMoved = false;

    public string enPassantTarget = "-";

    public int halfMoveClock = 0;
    public int fullMoveNumber = 1;

    public string castlingAvailablity() {
        return ((isWhiteKingMoved || isWhiteLeftRookMoved) ? "" : "K") +
        ((isWhiteKingMoved || isWhiteRightRookMoved) ? "" : "Q") +
        ((isBlackKingMoved || isBlackLeftRookMoved) ? "" : "q") +
        ((isBlackKingMoved || isBlackRightRookMoved) ? "" : "k");
    }
}
