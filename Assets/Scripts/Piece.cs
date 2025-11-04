using Unity.VisualScripting;
using UnityEngine;

public enum PlayerColor
{
    red,
    black
}

public class Piece : MonoBehaviour
{
    [SerializeField] public GameObject piece;
    [SerializeField] private bool _isQueen = false;
    private SpriteRenderer _highLight;
    private Color _initColor;
    private Color _queenColor;
    private Board _board;
    private PlayerColor _playerColor;
    public Tile tile;
    public bool isQueenGet { get { return _isQueen; } }
    public bool isQueenSet { set { _isQueen = value; } }
    public PlayerColor isColorGet { get { return _playerColor; } }
    public Color setColorQueen { set { _queenColor = value; } }

    void Start()
    {
        _highLight = GetComponent<SpriteRenderer>();
        piece = gameObject;
        _initColor = _highLight.color;
        _board = FindFirstObjectByType<Board>();
        if (gameObject.tag == "P1")
            _playerColor = PlayerColor.red;
        else
            _playerColor = PlayerColor.black;
    }

    private void OnMouseOver()
    {
        _highLight.color = Color.blueViolet;
    }

    private void OnMouseDown()
    {
        _board.pieceClicked(this);
    }

    private void OnMouseExit()
    {
        _highLight.color = _initColor;
        if (isQueenGet)
        {
            _highLight.color = _queenColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Tile>(out Tile tile))
        {
            this.tile = tile;
        }
    }

    public int[,] getDirections(PlayerColor currentPlayer, int dirY = 0)
    {
        if (_isQueen)
        {
            return new int[,] { { 1, -1 }, { 1, 1 }, { -1, -1 }, { -1, 1 } };
        }
        return new int[,] { { dirY, -1 }, { dirY, 1 } };
    }

    public void isQueen(PlayerColor currentPlayer,Piece pieceSelected)
    {
        if (currentPlayer == PlayerColor.red && pieceSelected.tile.yPos == 0)
        {
            pieceSelected.isQueenSet = true;
            pieceSelected.setColorQueen = Color.magenta;
        }
        if (currentPlayer == PlayerColor.black && pieceSelected.tile.yPos == 4)
        {
            pieceSelected.isQueenSet = true;
            pieceSelected.setColorQueen = Color.burlywood;
        }
    }

}
