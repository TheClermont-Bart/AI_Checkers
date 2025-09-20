using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    public Tile tileSelected;
    public Piece pieceSelected;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void tileClicked(Tile tile)
    {
        //Debug.Log(string.Format("Position X:{0}, Position Y:{1}", x, y));
        tileSelected = tile;
    }

    public void pieceClicked(Piece piece)
    {
        Debug.Log("Piece Cliquer");
        pieceSelected = piece;
    }

    public void moveBitch()
    {
        if (tileSelected.transform.position.x == pieceSelected.transform.position.x)
        {
            //pieceSelected.transform.position = tileSelected.transform.position;
        }
    }
}
