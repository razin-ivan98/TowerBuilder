using UnityEngine;

public class BuildingRocker : MonoBehaviour
{
    // говно
    [SerializeField] private GameData _gameData;
    private readonly float _maxAngle = 10;

    private float _speed = 10;
    private float _direction = 1;
    private float Angle => _maxAngle * _gameData.Instability;
    private Quaternion Target => Quaternion.Euler(0, 0, Angle * _direction);

    private void Update()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Target, Time.deltaTime * _speed * _gameData.Instability);

        if (Target == transform.rotation)
        {
            _direction = -_direction;
        }
    }
}
