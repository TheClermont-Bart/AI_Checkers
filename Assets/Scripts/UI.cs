using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void PlayerChange(string player)
    {
        _text.text = ("� toi de jouer : "+player);
    }
}
