using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int positionX;
    [SerializeField] private int positionY;
    [SerializeField] private bool _isFree = true;
    public int xPos { get { return positionX; } }
    public int yPos { get { return positionY; } }
    public bool isFreeGet { get { return _isFree; } }
    public bool isFreeSet { set { _isFree = value; } }  
    private SpriteRenderer _highLight;
    private Color _initColor;
    private Board _board;
    

    void Start()
    {
        _highLight = GetComponent<SpriteRenderer>();
        _initColor = _highLight.color;
        _board = FindFirstObjectByType<Board>();
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        _highLight.color = Color.blueViolet;
        _board.tileClicked(this);
    }

    private void OnMouseUp()
    {
        _highLight.color = _initColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
