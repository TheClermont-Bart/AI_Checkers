using Unity.VisualScripting;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] public GameObject _piece;
    public bool isQueenGet { get { return _isQueen; } }
    public bool isQueenSet { set { _isQueen = value; } }
    public Tile _tile;
    private SpriteRenderer _highLight;
    private Color _initColor;
    private Board _board;
    private bool _isQueen = false;

    void Start()
    {
        _highLight = GetComponent<SpriteRenderer>();
        _piece = gameObject;
        _initColor = _highLight.color;
        _board = FindFirstObjectByType<Board>();
    }

    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        _highLight.color = Color.blueViolet;
    }

    private void OnMouseDown()
    {
        _board.pieceClicked(this);
    }

    private void OnMouseUp()
    {
        
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
            _tile = tile;
            tile.isFreeSet = false;
        }
    }
}
