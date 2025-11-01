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
    [SerializeField] List<Tile> _tilelist;
    private int _turn = 0;
    private int _dirY = 0;
    public Tile tileSelected;
    public Piece pieceSelected;
    public List<GameObject> gameObjects;
    public PlayerColor currentPlayer = PlayerColor.red;
    public List<List<Tile>> grid;

    void Start()
    {
        _tilelist = FindObjectsByType<Tile>(FindObjectsSortMode.None).ToList();

        Tile _newTile = new Tile();

        grid = new List<List<Tile>>();

        List<Tile> _gridLine = Enumerable.Repeat(_newTile, 5).ToList();

        for (int i = 0; i < 5; i++)
        {
            grid.Add(new List<Tile>(new Tile[5]));
        }

        foreach (Tile tile in _tilelist)
        {
            if (tile.xPos >= 0 && tile.xPos <= 4 && tile.yPos >= 0 && tile.yPos <= 4)
            {
                grid[tile.yPos][tile.xPos] = tile;
            }
            else
            {
                Debug.LogError("Tile out of range");
            }
        }
        _player.setScorePlayer = 0;
        _ai.setScoreAI = 0;
        _ui.turnNB(_turn);
        _ui.PlayerChange(currentPlayer);
        _ui.scoreP1(_player.scorePlayer);
        _ui.scoreP2(_ai.scoreAI);
    }

    public void tileClicked(Tile tile)
    {
        tileSelected = tile;

        if (pieceSelected != null)
        {
            if (pieceSelected.isColorGet == currentPlayer && currentPlayer == PlayerColor.red)
            {
                movePiece();
            }
            else { movePiece(); }
        }
    }

    public void pieceClicked(Piece piece)
    {
        if (piece.isColorGet == currentPlayer)
        {
            pieceSelected = piece;
            _dirY = (currentPlayer == PlayerColor.red) ? -1 : 1;
        }
    }

    public void movePiece()
    {
        if (pieceSelected == null || tileSelected == null) return;

        Tile startTile = pieceSelected.tile;

        if (checkJump(pieceSelected, this, out _, tileSelected))
        {
            eat(pieceSelected, tileSelected);

            if (checkJump(pieceSelected, this, out Tile nextLanding))
            {
                return;
            }

            EndTurn();
        }
        else if (!checkEnnemy() && isValidMove(pieceSelected, startTile, tileSelected))
        {
            if (tileSelected._piece == null)
            {
                pieceSelected.transform.position = tileSelected.transform.position;
                startTile._piece = null;
                tileSelected._piece = pieceSelected;
                pieceSelected.tile = tileSelected;

                EndTurn();
            }
        }
    }

    public void eat(Piece piece, Tile tileSelected)
    {
        Tile startTile = piece.tile;

        int jumpedX = (startTile.xPos + tileSelected.xPos) / 2;
        int jumpedY = (startTile.yPos + tileSelected.yPos) / 2;

        Tile jumpedTile = grid[jumpedY][jumpedX];

        if (jumpedTile._piece != null)
        {
            Destroy(jumpedTile._piece.gameObject);
            jumpedTile._piece = null;
            startTile._piece = null;
            tileSelected._piece = piece;
            piece.tile = tileSelected;
            piece.transform.position = tileSelected.transform.position;

            if(currentPlayer == PlayerColor.red)
            {
                _player.newScorePlayer();
            }
            else
            {
                _ai.newScoreAI();
            }
        }

    }

    public bool checkJump(Piece piece, Board board, out Tile nextTile, Tile targetTile = null)
    {
        nextTile = null;
        if (piece == null) return false;

        Tile startTile = piece.tile;
        int[,] directions = piece.GetDirections(currentPlayer, _dirY);

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dirY = directions[i, 0];
            int dirX = directions[i, 1];

            int midX = startTile.xPos + dirX;
            int midY = startTile.yPos + dirY;
            int landingX = startTile.xPos + 2 * dirX;
            int landingY = startTile.yPos + 2 * dirY;

            if (landingY >= 0 && landingY < board.grid.Count && landingX >= 0 && landingX < board.grid[0].Count)
            {
                Tile midTile = board.grid[midY][midX];
                Tile landingTile = board.grid[landingY][landingX];

                if (midTile._piece != null && midTile._piece.isColorGet != currentPlayer && landingTile._piece == null)
                {
                    if (targetTile != null)
                    {
                        if (landingTile == targetTile)
                        {
                            nextTile = landingTile;
                            return true;
                        }
                    }
                    else
                    {
                        nextTile = landingTile;
                        return true;
                    }
                }

            }
        }
        return false;
    }

    public bool checkEnnemy()
    {
        foreach (var row in grid)
        {
            foreach (var tile in row)
            {
                Piece piece = tile._piece;
                if (piece == null) continue;
                if (piece.isColorGet != currentPlayer) continue;

                if (checkJump(piece, this, out _))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool isValidMove(Piece piece, Tile startTile, Tile targetTile)
    {
        int[,] directions = piece.GetDirections(currentPlayer, _dirY);

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dirY = directions[i, 0];
            int dirX = directions[i, 1];

            int currentX = startTile.xPos;
            int currentY = startTile.yPos;

            while (true)
            {
                currentX += dirX;
                currentY += dirY;

                if (currentY < 0 || currentY >= grid.Count || currentX < 0 || currentX >= grid[0].Count)
                {
                    break;
                }

                if (targetTile.xPos == currentX && targetTile.yPos == currentY)
                {
                    return true; 
                }

                Tile currentTile = grid[currentY][currentX];
                if (currentTile._piece != null)
                {
                    break; 
                }
            }
        }
        return false;
    }
    public void EndTurn()
    {
        pieceSelected.isQueen(currentPlayer, pieceSelected);
        currentPlayer = (currentPlayer == PlayerColor.red) ? PlayerColor.black : PlayerColor.red;
        _turn++;
        _ui.PlayerChange(currentPlayer);
        _ui.turnNB(_turn);
        _ui.scoreP1(_player.scorePlayer);
        _ui.scoreP2(_ai.scoreAI);
        pieceSelected = null;
        tileSelected = null;
    }

}