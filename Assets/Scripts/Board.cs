using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] private UI _ui;
    public Tile tileSelected;
    public Piece pieceSelected;
    public List<GameObject> gameObjects;
    private string _currentPlayer = "P1";

    void Start()
    {
        _ui.PlayerChange(_currentPlayer);
    }

    void Update()
    {

    }

    public void tileClicked(Tile tile)
    {
        tileSelected = tile;

        if (pieceSelected != null)
        {
            moveBitch();
        }
    }

    public void pieceClicked(Piece piece)
    {
        if (piece.tag == _currentPlayer)
        {
            pieceSelected = piece;
        }
    }

    public void moveBitch()
    {
        if (tileSelected.isFreeGet)
        {
           if (pieceSelected.tag == "P1" && tileSelected.yPos == pieceSelected._tile.yPos - 1 && (tileSelected.xPos == pieceSelected._tile.xPos - 1 || tileSelected.xPos == pieceSelected._tile.xPos + 1))
            {
                pieceSelected.transform.position = tileSelected.transform.position;
                pieceSelected._tile.isFreeSet = true;
                tileSelected.isFreeSet = false;
                _currentPlayer = _currentPlayer == "P1" ? "P2" : "P1";
            }
            if (pieceSelected.tag == "P2" && tileSelected.yPos == pieceSelected._tile.yPos + 1 && (tileSelected.xPos == pieceSelected._tile.xPos - 1 || tileSelected.xPos == pieceSelected._tile.xPos + 1))
            {
                pieceSelected.transform.position = tileSelected.transform.position;
                pieceSelected._tile.isFreeSet = true;
                tileSelected.isFreeSet = false;
                _currentPlayer = _currentPlayer == "P1" ? "P2" : "P1";
            }
        }
        pieceSelected = null;
        _ui.PlayerChange(_currentPlayer);
    }
}
