using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [SerializeField] public UI _ui;
    [SerializeField] private AI _ai;
    [SerializeField] private Player _player;
    private int _turn = 0;
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
            if (tile.xPos >= 0 && tile.xPos <= 4 && tile.yPos >= 0 && tile.yPos <= 4)
            {
                _grid[tile.yPos][tile.xPos] = tile;
            }
            else 
            { 
                Debug.LogError("Tile out of range"); 
            }
        }
        _ui.turnNB(_turn);
        _ui.PlayerChange(_currentPlayer);
        _ui.scoreP1(_player._scorePlayer);
        _ui.scoreP2(_ai._scoreAI);
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

        if (pieceSelected == null || tileSelected == null && _turn < 101) return;

        if (_currentPlayer == "P1")
        {
            _player.movePlayer(tileSelected, pieceSelected, this, _ui);
        }
        if (_currentPlayer == "P2")
        {
            _ai.moveAI(tileSelected, pieceSelected, this, _ui);
        }
        _turn++;
        _ui.PlayerChange(_currentPlayer);
        _ui.turnNB(_turn);
    }


}
