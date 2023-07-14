using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveMaker
{
    GameObject board;
    TMP_Text titleText;

    public void setBoard(GameObject board) {
        this.board = board;
    }

    public void setTitleText(TMP_Text titleText) {
        this.titleText = titleText;
    }

    public void makeMove(string move) {
        string[] moveParts = move.Split('-');

        string from = moveParts[0];
        string to = moveParts[1];

        if ( from.Contains("#")) from = from.Split("#")[1];
        if ( to.Contains("#")) to = to.Split("#")[1];

        Debug.Log("Move from " + from + " to " + to);
        titleText.text = "Move from " + from + " to " + to;
    }
}