using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(TransformAnimator))]
public class Box : MonoBehaviour
{
    [SerializeField] private BoxCatcher _catcher;

    private Rigidbody2D _rigidBody;
    private TransformAnimator _transformAnimator;

    public event Action Falled;

    private void Awake()
    {
        _transformAnimator = GetComponent<TransformAnimator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // кал
        if (this.transform.position.y < -8 && _rigidBody.bodyType == RigidbodyType2D.Dynamic)
        {
            Falled.Invoke();
        }
    }

// унести бы куда нить
// родитель меняется снаружи - оч плох
    public BoxCatcher IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess)
    {
        _rigidBody.bodyType = RigidbodyType2D.Static;
        _transformAnimator.TransformTowards(localPosition, Quaternion.identity, onSuccess);
        _catcher.Activate();
        return _catcher;
    }

    public void Reset()
    {
        _transformAnimator.Reset();
        _catcher.Deactivate();

        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody.velocity = new Vector2();
        _rigidBody.angularVelocity = 0;

        transform.SetParent(null);
        transform.position = new Vector3();
        transform.rotation = Quaternion.identity;
    }
}
