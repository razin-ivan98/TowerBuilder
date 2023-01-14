using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(TransformAnimator))]
public class Box : MonoBehaviour
{
    [SerializeField] private BoxCatcher _catcher;
    private Rigidbody2D _rigidBody;
    private TransformAnimator _transformAnimator;
    private BoxStateMachine _boxStateMachine;

// да да не работает
    public event Action Falled;

    public ICatchingActioned Catcher => _catcher;

    private void Awake()
    {
        _transformAnimator = GetComponent<TransformAnimator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _boxStateMachine = new BoxStateMachine(transform, _catcher, _rigidBody, _transformAnimator);
    }

    private void Update()
    {
        _boxStateMachine.Update();
    }

// родитель меняется снаружи - оч плох
    public void IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess)
    {
        _boxStateMachine.IntegrateToBuilding(localPosition, onSuccess);
    }

    public void Reset()
    {
        _boxStateMachine.Reset();

        _rigidBody.velocity = new Vector2();
        _rigidBody.angularVelocity = 0;

        transform.SetParent(null);
        transform.position = new Vector3();
        transform.rotation = Quaternion.identity;
    }
}
