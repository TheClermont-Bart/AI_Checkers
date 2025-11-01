using System.Runtime.CompilerServices;
using UnityEngine;

public class Queen : Piece
{
    private Color _colorQueenP1 = Color.magenta;
    private Color _colorQueenP2 = Color.brown;
    public Color getColorQueenP1 { get { return _colorQueenP1; } }
    public Color getColorQueenP2 { get { return _colorQueenP2; } }

}
