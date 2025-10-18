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
    public PlayerColor currentPlayer = PlayerColor.red;
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
        _ui.PlayerChange(currentPlayer);
        _ui.scoreP1(_player.scorePlayer);
        _ui.scoreP2(_ai._scoreAI);
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
            else { _ai.moveAI(tile, pieceSelected, this, _ui); }
        }
    }

    public void pieceClicked(Piece piece)
    {
        if (piece.isColorGet == currentPlayer)
        {
            pieceSelected = piece;
        }
    }

    public void movePiece()
    {
        if (pieceSelected == null || tileSelected == null && _turn < 101) return;

        Tile startTile = pieceSelected.tile;

        if (checkJump(pieceSelected, this, out _, tileSelected))
        {
            move(pieceSelected, tileSelected);

            if (checkJump(pieceSelected, this, out Tile nextLanding))
            {
                return;
            }

            EndTurn();
        }
        else if (!checkEnnemy() && tileSelected.yPos == startTile.yPos - 1 && (tileSelected.xPos == startTile.xPos - 1 || tileSelected.xPos == startTile.xPos + 1))
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

    public void move(Piece piece, Tile lastTile)
    {
        Tile startTile = piece.tile;

        int jumpedX = (startTile.xPos + lastTile.xPos) / 2;
        int jumpedY = (startTile.yPos + lastTile.yPos) / 2;

        Tile jumpedTile = _grid[jumpedY][jumpedX];

        if (jumpedTile._piece != null)
        {
            Destroy(jumpedTile._piece.gameObject);
            jumpedTile._piece = null;
            startTile._piece = null;
            lastTile._piece = piece;
            piece.tile = lastTile;
            if (currentPlayer == PlayerColor.red)
            {
                _player.newScorePlayer();
                _ui.scoreP1(_player.scorePlayer);
            }
            else
            {
                _ai._scoreAI++;
                _ui.scoreP2(_ai._scoreAI);
            }
            piece.transform.position = lastTile.transform.position;
        }

    }

    public bool checkJump(Piece piece, Board board, out Tile nextTile, Tile targetTile = null)
    {
        nextTile = null;
        if (piece == null) return false;

        Tile startTile = piece.tile;
        int[,] directions = new int[,] { { -1, -1 }, { -1, 1 } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dirY = directions[i, 0];
            int dirX = directions[i, 1];

            int midX = startTile.xPos + dirX;
            int midY = startTile.yPos + dirY;
            int landingX = startTile.xPos + 2 * dirX;
            int landingY = startTile.yPos + 2 * dirY;

            if (landingY >= 0 && landingY < board._grid.Count && landingX >= 0 && landingX < board._grid[0].Count)
            {
                Tile midTile = board._grid[midY][midX];
                Tile landingTile = board._grid[landingY][landingX];

                // V�rifie si on saute par-dessus un ennemi et qu�on peut atterrir
                if (midTile._piece != null && midTile._piece.isColorGet != currentPlayer && landingTile._piece == null)
                {
                    // Cas d�un saut cibl� (clic du joueur)
                    if (targetTile != null)
                    {
                        if (landingTile == targetTile)
                        {
                            nextTile = landingTile;
                            return true;
                        }
                    }
                    else // Cas d�une recherche automatique (double saut possible)
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
        foreach (List<Tile> row in _grid)
        {
            foreach (Tile tile in row)
            {
                Piece piece = tile._piece;

                if (piece == null)
                {
                    if (piece.isColorGet != currentPlayer)
                    {

                        int[,] directions = new int[,] { { -1, -1 }, { -1, 1 } };

                        for (int i = 0; i < directions.GetLength(0); i++)
                        {
                            int dirY = directions[i, 0];
                            int dirX = directions[i, 1];

                            int midX = tile.xPos + dirX;
                            int midY = tile.yPos + dirY;
                            int landingX = tile.xPos + 2 * dirX;
                            int landingY = tile.yPos + 2 * dirY;

                            if (landingY < 0 || landingY >= _grid.Count || landingX < 0 || landingX >= _grid[0].Count)
                            {

                                Tile midTile = _grid[midY][midX];
                                Tile landingTile = _grid[landingY][landingX];

                                if (midTile._piece != null && midTile._piece.isColorGet != currentPlayer && landingTile._piece == null)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    public void EndTurn()
    {
        currentPlayer = (currentPlayer == PlayerColor.red) ? PlayerColor.black : PlayerColor.red;
        _turn++;
        _ui.PlayerChange(currentPlayer);
        _ui.turnNB(_turn);
    }
}