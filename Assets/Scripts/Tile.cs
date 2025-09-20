using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private int positionX;
    [SerializeField] private int positionY;
    public int xPos { get { return positionX; } }
    private SpriteRenderer m_highLight;
    private Color m_initColor;
    private Board board; 

    void Start()
    {
        m_highLight = GetComponent<SpriteRenderer>();
        m_initColor = m_highLight.color;
        board = FindFirstObjectByType<Board>();
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        m_highLight.color = Color.blueViolet;
        board.tileClicked(this);
    }

    private void OnMouseUp()
    {
        m_highLight.color = m_initColor;
        board.moveBitch();
    }
}
