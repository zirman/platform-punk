using Godot;

namespace PlatformPunk.GameManager;

public partial class GameManager : Node
{
    private int _score;

    public void AddPoint()
    {
        _score++;
        GD.Print("Score: " + _score);
    }
}
