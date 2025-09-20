using Unity.VisualScripting;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] public GameObject m_piece;
    private SpriteRenderer m_highLight;
    private Color m_initColor;
    private Board m_board;

    void Start()
    {
        m_highLight = GetComponent<SpriteRenderer>();
        m_piece = gameObject;
        m_initColor = m_highLight.color;
        m_board = FindFirstObjectByType<Board>();
    }

    void Update()
    {
      
        
    }

    private void OnMouseOver()
    {
        m_highLight.color = Color.blueViolet;
    }

    private void OnMouseDown()
    {
        m_board.pieceClicked(this);
    }

    private void OnMouseUp()
    {
        
    }

    private void OnMouseExit()
    {
        m_highLight.color = m_initColor;
    }

}
