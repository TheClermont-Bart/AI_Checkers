using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTurn;
    [SerializeField] private TextMeshProUGUI _scoreP1;
    [SerializeField] private TextMeshProUGUI _scoreP2;
    [SerializeField] private TextMeshProUGUI _turnNB;
    [SerializeField] private TextMeshProUGUI _win;
    [SerializeField] private TextMeshProUGUI _lose;

    public void PlayerChange(PlayerColor player)
    {
        _textTurn.text = ("À toi de jouer : "+ player.ToString());
    }

    public void scoreP1(int playerscore)
    {
        _scoreP1.text = ("Score Player 1 : "+playerscore);
    }

    public void scoreP2(int playerscore)
    {
        _scoreP2.text = ("Score Player 2 : "+playerscore);
    }

    public void turnNB(int turnnb)
    {
        _turnNB.text = ("Nombre de tour : " + turnnb);
    }

    public void winLose(PlayerColor player)
    {
        if (player == PlayerColor.red) { _win.gameObject.SetActive(true); }
        if (player == PlayerColor.black) { _lose.gameObject.SetActive(true); }
    }
}
