using UnityEngine;

public class GameData : MonoBehaviour
{
    private float _instability = 0;

    public int Height { get; set; } = 0;
    public float Instability
    {
        get => _instability;
        set => _instability = value <= 1 ? value : 1;
    }
}
