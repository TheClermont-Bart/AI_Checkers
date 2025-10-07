using UnityEngine;

public class Player : MonoBehaviour
{
    public int _scorePlayer;
    void Start()
    {
        _scorePlayer = 0;
    }

    void Update()
    {

    }

    public void movePlayer(Tile tileSelected, Piece pieceSelected, Board _board, UI _ui)
    {
        if (pieceSelected == null || tileSelected == null) return;

        Tile startTile = pieceSelected._tile;

        if (checkJump(pieceSelected, tileSelected, _board))
        {
            move(pieceSelected, tileSelected, _board);

            while (checkDoubleJump(pieceSelected, _board, out Tile nextLanding))
            {
                move(pieceSelected, nextLanding, _board);
            }

            _board._currentPlayer = "P2";
        }
        else if (tileSelected.yPos == startTile.yPos - 1 && (tileSelected.xPos == startTile.xPos - 1 || tileSelected.xPos == startTile.xPos + 1))
        {
            if (tileSelected._piece == null)
            {
                pieceSelected.transform.position = tileSelected.transform.position;
                startTile._piece = null;
                tileSelected._piece = pieceSelected;
                pieceSelected._tile = tileSelected;

                _board._currentPlayer = "P2";
            }
        }
        if (pieceSelected._tile.yPos == 0)
        {
            pieceSelected.isQueenSet = true;
        }
        _ui.PlayerChange(_board._currentPlayer);
    }

    private void move(Piece piece, Tile lastTile, Board board)
    {
        Tile startTile = piece._tile;

        int jumpedX = (startTile.xPos + lastTile.xPos) / 2;
        int jumpedY = (startTile.yPos + lastTile.yPos) / 2;

        Tile jumpedTile = board._grid[jumpedY][jumpedX];

        if (jumpedTile._piece != null)
        {
            Destroy(jumpedTile._piece.gameObject);
            jumpedTile._piece = null;
            startTile._piece = null;
            lastTile._piece = piece;
            piece._tile = lastTile;
            _scorePlayer++;
            board._ui.scoreP1(_scorePlayer);
            piece.transform.position = lastTile.transform.position;
        }

    }

    public bool checkJump(Piece pieceSelected, Tile tileSelected, Board board)
    {
        if (pieceSelected == null || tileSelected == null) return false;

        Tile startTile = pieceSelected._tile;

        if (tileSelected.yPos == startTile.yPos - 2 && (tileSelected.xPos == startTile.xPos - 2 || tileSelected.xPos == startTile.xPos + 2))
        {
            int jumpedX = (startTile.xPos + tileSelected.xPos) / 2;
            int jumpedY = (startTile.yPos + tileSelected.yPos) / 2;

            Tile jumpedTile = board._grid[jumpedY][jumpedX];

            if (jumpedTile != null && jumpedTile._piece != null && jumpedTile._piece.tag == "P2")
            {
                return true;
            }
        }
        return false;
    }

    public bool checkDoubleJump(Piece pieceSelected, Board board, out Tile nextTile)
    {
        nextTile = null;
        Tile startTile = pieceSelected._tile;

        int[,] directions = new int[,] { { -1, -1 }, { -1, 1 } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int directionX = directions[i, 1];
            int directionY = directions[i, 0];

            int midX = startTile.xPos + directionX;
            int midY = startTile.yPos + directionY;

            int landingX = startTile.xPos + 2 * directionX;
            int landingY = startTile.yPos + 2 * directionY;

            if (landingY >= 0 && landingY < board._grid.Count && landingX >= 0 && landingX < board._grid[0].Count)
            {
                Tile midTile = board._grid[midY][midX];
                Tile possibleLanding = board._grid[landingY][landingX];

                if (midTile._piece != null && midTile._piece.tag == "P2" && possibleLanding._piece == null)
                {
                    nextTile = possibleLanding;
                    return true;
                }
            }
        }
        return false;
    }

}
