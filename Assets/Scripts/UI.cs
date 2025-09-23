using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayerChange(string player)
    {
        _text.text = ("À toi de jouer : "+player);
    }
}
