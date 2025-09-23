using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [SerializeField] List<Tile> _tilelist;
    private List<List<Tile>> _grid;

    void Start()
    {
        _tilelist = FindObjectsByType<Tile>(FindObjectsSortMode.None).ToList();

        Tile _newTile = new Tile();
        List<List<Tile>> _grid = new List<List<Tile>>();
        List<Tile> _gridLine = Enumerable.Repeat(_newTile, 5).ToList();

        for (int i = 0; i < 5; i++)
        {
            _grid.Add(_gridLine);
        }

        foreach (Tile tile in _tilelist)
        {
            _grid[tile.xPos - 1][tile.yPos] = tile;
        }


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
            movePiece();
        }
    }

    public void pieceClicked(Piece piece)
    {
        if (piece.tag == _currentPlayer)
        {
            pieceSelected = piece;
        }
    }

    public void movePiece()
    {

        if (pieceSelected.tag == "P1")
        {
            if ((_grid[tileSelected.xPos + 1][tileSelected.yPos + 1].tag == "P2" || _grid[tileSelected.xPos - 1][tileSelected.yPos + 1].tag == "P2"))
            {
                if(tileSelected.yPos == pieceSelected._tile.yPos - 2 && (tileSelected.xPos == pieceSelected._tile.xPos - 2 || tileSelected.xPos == pieceSelected._tile.xPos + 2))
                {
                    pieceSelected.transform.position = tileSelected.transform.position;
                    //_currentPlayer = _currentPlayer == "P1" ? "P2" : "P1";
                }
            }
        }











        /*if (pieceSelected.tag == "P1" && tileSelected.yPos == pieceSelected._tile.yPos - 1 && (tileSelected.xPos == pieceSelected._tile.xPos - 1 || tileSelected.xPos == pieceSelected._tile.xPos + 1))
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
         }*/

        pieceSelected = null;
        _ui.PlayerChange(_currentPlayer);
    }
}
