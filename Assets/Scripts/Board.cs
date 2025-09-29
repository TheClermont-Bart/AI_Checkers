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
    [SerializeField] private AI _ai;
    public Tile tileSelected;
    public Piece pieceSelected;
    public List<GameObject> gameObjects;
    public string _currentPlayer = "P1";
    [SerializeField] List<Tile> _tilelist;
    public List<List<Tile>> _grid;

    void Start()
    {
        _tilelist = FindObjectsByType<Tile>(FindObjectsSortMode.None).ToList();

        Tile _newTile = new Tile();

        _grid = new List<List<Tile>>();

        List<Tile> _gridLine = Enumerable.Repeat(_newTile, 5).ToList();

        for (int i = 0; i < 5; i++)
        {
            _grid.Add(new List<Tile>(new Tile[5]));
        }

        foreach (Tile tile in _tilelist)
        {
            if (tile.xPos > 0 && tile.xPos <= 5 && tile.yPos >= 0 && tile.yPos < 5)
            {
                _grid[tile.yPos][tile.xPos - 1] = tile;
            }
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

        if (pieceSelected == null || tileSelected == null) return;

        Tile startTile = pieceSelected._tile;
        
        if (pieceSelected.tag == "P1" && checkDoubleJump(pieceSelected))
        {
            int jumpedX = (startTile.xPos + tileSelected.xPos) / 2;
            int jumpedY = (startTile.yPos + tileSelected.yPos) / 2;

            Tile jumpedTile = _grid[jumpedY][jumpedX - 1];

            if (jumpedTile != null && jumpedTile._piece != null && jumpedTile._piece.tag == "P2")
            {

                Destroy(jumpedTile._piece.gameObject);
                jumpedTile._piece = null;
                startTile._piece = null;

                tileSelected._piece = pieceSelected;
                pieceSelected._tile = tileSelected;

                pieceSelected.transform.position = tileSelected.transform.position;
            }
        }
        else if (pieceSelected.tag == "P1" && tileSelected.yPos == startTile.yPos - 1 && (tileSelected.xPos == startTile.xPos - 1 || tileSelected.xPos == startTile.xPos + 1))
        {
            if (tileSelected._piece == null) 
            {
                startTile._piece = null;
                tileSelected._piece = pieceSelected;
                pieceSelected._tile = tileSelected;
                pieceSelected.transform.position = tileSelected.transform.position;
                _currentPlayer = "P2";
            }
        }
        _ui.PlayerChange(_currentPlayer);
        _ai.moveAI(tileSelected, pieceSelected, this, _ui);
    }

    public bool checkDoubleJump(Piece pieceSelected)
    {
        if (pieceSelected == null || tileSelected == null) return false;

        Tile startTile = pieceSelected._tile;

        if (pieceSelected.tag == _currentPlayer && tileSelected.yPos == startTile.yPos - 2 && (tileSelected.xPos == startTile.xPos - 2 || tileSelected.xPos == startTile.xPos + 2))
        {
            int jumpedX = (startTile.xPos + tileSelected.xPos) / 2;
            int jumpedY = (startTile.yPos + tileSelected.yPos) / 2;

            Tile jumpedTile = _grid[jumpedY][jumpedX - 1];

            if (jumpedTile != null && jumpedTile._piece != null && jumpedTile._piece.tag == "P2")
            {
                return true;
            }
        }
        return false;
    }
}
