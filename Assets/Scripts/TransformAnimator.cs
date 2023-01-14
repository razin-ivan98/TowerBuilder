using UnityEngine;

public class TransformAnimator : MonoBehaviour
{
    [SerializeField] private readonly float _translationSpeed = 4.0f;
    [SerializeField] private readonly float _rotationSpeed = 200.0f;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private bool _isTransforming = false;

    public delegate void OnSuccessCallback();

    private OnSuccessCallback _onSuccess;

    private void Update()
    {
        if (_isTransforming)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, _targetPosition, Time.deltaTime * _translationSpeed);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _targetRotation, Time.deltaTime * _rotationSpeed);
        }
        if (_isTransforming && transform.localPosition.Equals(_targetPosition) && transform.localRotation.Equals(_targetRotation))
        {
            _onSuccess?.Invoke();
            Reset();
        }
    }

    public void TransformTowards(Vector3? position, Quaternion? rotation, OnSuccessCallback OnSuccess)
    {
        _targetPosition = position ?? transform.localPosition;
        _targetRotation = rotation ?? transform.localRotation;
        _isTransforming = true;
        _onSuccess = OnSuccess;
    }
    public void Reset()
    {
        _isTransforming = false;
        _onSuccess = null;
    }
}
