using UnityEngine;

public class Player : MonoBehaviour
{
    private int _scorePlayer = 0;
    private Piece _piece;
    public int scorePlayer { get { return _scorePlayer; }}
    void Awake()
    {
        _scorePlayer = 0;
    }

    void Update()
    {

    }

    public void newScorePlayer()
    {
        _scorePlayer++;
    }

}
