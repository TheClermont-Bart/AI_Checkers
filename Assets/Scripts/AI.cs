using UnityEngine;

public class AI : MonoBehaviour
{
    private bool _isQueen;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void moveAI(Tile tileSelected, Piece pieceSelected, Board _board,UI _ui)
    {
        if (pieceSelected == null || tileSelected == null) return;

        Tile startTile = pieceSelected._tile;

        if (tileSelected.yPos == startTile.yPos + 2 && (tileSelected.xPos == startTile.xPos + 2 || tileSelected.xPos == startTile.xPos - 2))
        {
            int jumpedX = (startTile.xPos + tileSelected.xPos) / 2;
            int jumpedY = (startTile.yPos + tileSelected.yPos) / 2;

            Tile jumpedTile = _board._grid[jumpedY][jumpedX - 1];

            if (jumpedTile != null && jumpedTile._piece != null && jumpedTile._piece.tag == "P1")
            {

                Destroy(jumpedTile._piece.gameObject);
                jumpedTile._piece = null;
                startTile._piece = null;

                tileSelected._piece = pieceSelected;
                pieceSelected._tile = tileSelected;

                pieceSelected.transform.position = tileSelected.transform.position;

                _board._currentPlayer = "P1";
            }
        }
        else if (tileSelected.yPos == startTile.yPos + 1 && (tileSelected.xPos == startTile.xPos + 1 || tileSelected.xPos == startTile.xPos - 1))
        {
            if (tileSelected._piece == null)
            {
                startTile._piece = null;
                tileSelected._piece = pieceSelected;
                pieceSelected._tile = tileSelected;
                pieceSelected.transform.position = tileSelected.transform.position;

                _board._currentPlayer = "P1";
            }
        }


        _ui.PlayerChange(_board._currentPlayer);
    }
}
