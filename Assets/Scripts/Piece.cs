using Unity.VisualScripting;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] public GameObject _piece;
    public bool isQueenGet { get { return _isQueen; } }
    public bool isQueenSet { set { _isQueen = value; } }
    private SpriteRenderer _highLight;
    private Color _initColor;
    private Board _board;
    [SerializeField] private bool _isQueen = false;
    public Tile _tile;

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
            _tile = tile;
        }
    }


}
