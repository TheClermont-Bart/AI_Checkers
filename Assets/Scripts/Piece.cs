using Unity.VisualScripting;
using UnityEngine;

public enum PlayerColor
{
    red,
    black
}

public class Piece : MonoBehaviour
{
    [SerializeField] public GameObject _piece;
    [SerializeField] private bool _isQueen = false;
    private SpriteRenderer _highLight;
    private Color _initColor;
    private Board _board;
    private PlayerColor _playerColor;
    public Tile tile;
    public bool isQueenGet { get { return _isQueen; } }
    public bool isQueenSet { set { _isQueen = value; } }
    public PlayerColor isColorGet { get { return _playerColor; } }
    public PlayerColor isColorSet { set { _playerColor = value; } }

    void Start()
    {
        _highLight = GetComponent<SpriteRenderer>();
        _piece = gameObject;
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
        Debug.Log("Click");
    }

    private void OnMouseExit()
    {
        _highLight.color = _initColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Tile>(out Tile tile))
        {
            Debug.Log("Trigger");
            this.tile = tile;
        }
    }

    public int[,] GetDirections(PlayerColor currentPlayer, int dirY = 0)
    {
        if (_isQueen)
        {
            return new int[,] { { 1, -1 }, { 1, 1 }, { -1, -1 }, { -1, 1 } };
        }
        return new int[,] { { dirY, -1 }, { dirY, 1 } };
    }

}
