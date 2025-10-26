using UnityEngine;

public class AI : MonoBehaviour
{
    private int _scoreAI = 0;
    public int scoreAI { get { return _scoreAI; } }
    public int setScoreAI { set { _scoreAI = value; } }
    void Start()
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
        else if (!_board.checkEnnemy() && tileSelected.yPos == startTile.yPos + _board.whatDir && (tileSelected.xPos == startTile.xPos - 1 || tileSelected.xPos == startTile.xPos + 1))
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
    }

    public void newScoreAI()
    {
        _scoreAI++;
    }
}
