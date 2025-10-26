using UnityEngine;

public class Player : MonoBehaviour
{
    private int _scorePlayer = 0;
    private Piece _piece;
    public int scorePlayer { get { return _scorePlayer; }}
    public int setScorePlayer { set { _scorePlayer = value; } }
    void Awake()
    {
        _scorePlayer = 0;
    }

    public void newScorePlayer()
    {
        _scorePlayer++;
    }

}
