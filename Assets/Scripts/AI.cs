using UnityEngine;

public class AI : MonoBehaviour
{
    private int _scoreAI = 0;
    public int scoreAI { get { return _scoreAI; } }
    public int setScoreAI { set { _scoreAI = value; } }


    public void newScoreAI()
    {
        _scoreAI++;
    }
}
