using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public Tile tileSelected;
    public Piece pieceSelected;
    private string _currentPlayer = "P1";

    void Start()
    {

    }

    void Update()
    {

    }

    public void tileClicked(Tile tile)
    {
        Debug.Log("Tile Cliquer");
        tileSelected = tile;

        if (pieceSelected != null)
        {
            moveBitch();
        }
    }

    public void pieceClicked(Piece piece)
    {
        if (piece.tag == _currentPlayer)
        {
            Debug.Log("Piece Cliquer");
            pieceSelected = piece;
        }
    }

    public void moveBitch()
    {
        if (pieceSelected.tag == "P1" && tileSelected.xPos == pieceSelected._tile.xPos - 1 || tileSelected.xPos == pieceSelected._tile.xPos + 1 && tileSelected.yPos == pieceSelected._tile.yPos - 1)
        {
            pieceSelected.transform.position = tileSelected.transform.position;
            pieceSelected = null;
            tileSelected = null;
            _currentPlayer = _currentPlayer == "P1" ? "P2" : "P1";
        }

        if (pieceSelected.tag == "P2" && tileSelected.xPos == pieceSelected._tile.xPos - 1 || tileSelected.xPos == pieceSelected._tile.xPos + 1 && tileSelected.yPos == pieceSelected._tile.yPos + 1)
        {
            pieceSelected.transform.position = tileSelected.transform.position;
            pieceSelected = null;
            tileSelected = null;
            _currentPlayer = _currentPlayer == "P1" ? "P2" : "P1";
        }
    }
}
