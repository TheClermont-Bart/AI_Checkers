using UnityEngine;

public class Player : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void movePlayer(Tile tileSelected, Piece pieceSelected, Board _board, UI _ui)
    {
        if (pieceSelected == null || tileSelected == null) return;

        Tile startTile = pieceSelected._tile;

        int jumpedX = (startTile.xPos + tileSelected.xPos) / 2;
        int jumpedY = (startTile.yPos + tileSelected.yPos) / 2;

        Tile jumpedTile = _board._grid[jumpedY][jumpedX];

        while (checkJump(pieceSelected, tileSelected, _board) /* checkDoubleJump(pieceSelected, _board)*/)
        {
            Destroy(jumpedTile._piece.gameObject);
            jumpedTile._piece = null;
            startTile._piece = null;

            tileSelected._piece = pieceSelected;
            pieceSelected._tile = tileSelected;

            pieceSelected.transform.position = tileSelected.transform.position;
        }
        if (!checkJump(pieceSelected, tileSelected, _board))
        {
            if (tileSelected.yPos == startTile.yPos - 1 && (tileSelected.xPos == startTile.xPos - 1 || tileSelected.xPos == startTile.xPos + 1))
            {
                if (tileSelected._piece == null)
                {
                    pieceSelected.transform.position = tileSelected.transform.position;
                    startTile._piece = null;
                    tileSelected._piece = pieceSelected;
                    pieceSelected._tile = tileSelected;
                }
            }
            _board._currentPlayer = "P2";
        }
        _ui.PlayerChange(_board._currentPlayer);
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

    public bool checkDoubleJump(Piece pieceSelected, Board board)
    {
        Tile startTile = pieceSelected._tile;

        int jumpedX = (startTile.xPos);
        int jumpedY = (startTile.yPos);
        
        if (board._grid[startTile.yPos - 1][startTile.xPos + 1]._piece.tag == "P2")
        {
            return true;
        }
        if (board._grid[startTile.yPos - 1][startTile.xPos - 1]._piece.tag == "P2")
        {
            return true;
        }
        return false;
    }
}
