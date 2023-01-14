using UnityEngine;
using System;

public interface ICatchingActioned
{
    public event Action<Box, Vector2> BoxCatched;
}

[RequireComponent(typeof(Collider2D))]
public class BoxCatcher : MonoBehaviour, ICatchingActioned
{
    public event Action<Box, Vector2> BoxCatched;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<Box>(out _))
        {
            CatchBox(other.gameObject.GetComponent<Box>(), other.GetContact(0).point);
        }
    }

    private void CatchBox(Box box, Vector2 point)
    {
        BoxCatched?.Invoke(box, point);
    }

    public void Deactivate()
    {
        _collider.enabled = false;
    }

    public void Activate()
    {
        _collider.enabled = true;
    }
}
