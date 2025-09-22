using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public Tile tileSelected;
    public Piece pieceSelected;
    private string _currentPlayer = "P1";
    public List<GameObject> gameObjects;

    void Start()
    {

    }

    void Update()
    {

    }

    public void tileClicked(Tile tile)
    {
        Debug.Log("Tile Cliquer");
        tileSelected = tile;

        if (pieceSelected != null)
        {
            moveBitch();
        }
    }

    public void pieceClicked(Piece piece)
    {
        if (piece.tag == _currentPlayer && tileSelected != null)
        {
            Debug.Log("Piece Cliquer");
            pieceSelected = piece;
        }
    }

    public void moveBitch()
    {
        if (tileSelected.isFreeGet)
        {
            if (pieceSelected.tag == "P1" && tileSelected.yPos == pieceSelected._tile.yPos - 1 && tileSelected.xPos == pieceSelected._tile.xPos - 1 || tileSelected.xPos == pieceSelected._tile.xPos + 1)
            {
                pieceSelected.transform.position = tileSelected.transform.position;
                pieceSelected._tile.isFreeSet = true;
                tileSelected.isFreeSet = false;
                
            }
            if (pieceSelected.tag == "P2" && tileSelected.yPos == pieceSelected._tile.yPos + 1 && tileSelected.xPos == pieceSelected._tile.xPos - 1 || tileSelected.xPos == pieceSelected._tile.xPos + 1)
            {
                pieceSelected.transform.position = tileSelected.transform.position;
                pieceSelected._tile.isFreeSet = true;
                tileSelected.isFreeSet = false;
            }
        }
        _currentPlayer = _currentPlayer == "P1" ? "P2" : "P1";
        pieceSelected = null;

    }
}
