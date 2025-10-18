using UnityEngine;

public class AI : MonoBehaviour
{
    private bool _isQueen;
    public int _scoreAI = 0;
    void Start()
    {

    }

    void Update()
    {

    }

    public void moveAI(Tile tileSelected, Piece pieceSelected, Board _board, UI _ui)
    {
        if (pieceSelected == null || tileSelected == null) return;

        Tile startTile = pieceSelected.tile;

        if (_board.checkJump(pieceSelected, _board, out _, tileSelected))
        {
            _board.move(pieceSelected, tileSelected);

            if (_board.checkJump(pieceSelected, _board, out Tile nextLanding))
            {
                return;
            }

            _board.EndTurn();
        }
        else if (!_board.checkEnnemy() && tileSelected.yPos == startTile.yPos + 1 && (tileSelected.xPos == startTile.xPos - 1 || tileSelected.xPos == startTile.xPos + 1))
        {
            if (tileSelected._piece == null)
            {
                pieceSelected.transform.position = tileSelected.transform.position;
                startTile._piece = null;
                tileSelected._piece = pieceSelected;
                pieceSelected.tile = tileSelected;

                _board.EndTurn();
            }
        }
        if (pieceSelected.tile.yPos == 4)
        {
            pieceSelected.isQueenSet = true;
        }
    }

    private void move(Piece piece, Tile landingTile, Board board)
    {
        Tile startTile = piece.tile;

        int jumpedX = (startTile.xPos + landingTile.xPos) / 2;
        int jumpedY = (startTile.yPos + landingTile.yPos) / 2;

        Tile jumpedTile = board._grid[jumpedY][jumpedX];

        if (jumpedTile._piece != null)
        {
            Destroy(jumpedTile._piece.gameObject);
            jumpedTile._piece = null;
            startTile._piece = null;
            landingTile._piece = piece;
            piece.tile = landingTile;
            piece.transform.position = landingTile.transform.position;
        }
    }

    public bool checkJump(Piece pieceSelected, Tile tileSelected, Board board)
    {
        if (pieceSelected == null || tileSelected == null) return false;

        Tile startTile = pieceSelected.tile;

        if (tileSelected.yPos == startTile.yPos + 2 && (tileSelected.xPos == startTile.xPos - 2 || tileSelected.xPos == startTile.xPos + 2))
        {
            int jumpedX = (startTile.xPos + tileSelected.xPos) / 2;
            int jumpedY = (startTile.yPos + tileSelected.yPos) / 2;

            Tile jumpedTile = board._grid[jumpedY][jumpedX];

            if (jumpedTile != null && jumpedTile._piece != null && jumpedTile._piece.isColorGet == PlayerColor.red)
            {
                return true;
            }
        }
        return false;
    }

    public bool checkDoubleJump(Piece pieceSelected, Board board, out Tile landingTile)
    {
        landingTile = null;
        Tile startTile = pieceSelected.tile;

        int[,] directions = new int[,] { { +1, +1 }, { -1, 1 } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int dx = directions[i, 1];
            int dy = directions[i, 0];

            int midX = startTile.xPos + dx;
            int midY = startTile.yPos + dy;

            int landingX = startTile.xPos + 2 * dx;
            int landingY = startTile.yPos + 2 * dy;

            if (landingY >= 0 && landingY < board._grid.Count &&
                landingX >= 0 && landingX < board._grid[0].Count)
            {
                Tile midTile = board._grid[midY][midX];
                Tile possibleLanding = board._grid[landingY][landingX];

                if (midTile._piece != null && midTile._piece.isColorGet == PlayerColor.red &&
                    possibleLanding._piece == null)
                {
                    landingTile = possibleLanding;
                    return true;
                }
            }
        }
        return false;
    }
}
