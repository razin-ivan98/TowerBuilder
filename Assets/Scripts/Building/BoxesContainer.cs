using UnityEngine;
using System;

[RequireComponent(typeof(TransformAnimator))]
public class BoxesContainer : MonoBehaviour
{
    public static event Action ReadyForNewBox;
    private TransformAnimator _transformAnimator;

    private void Start()
    {
        _transformAnimator = GetComponent<TransformAnimator>();
    }

    public void moveDown()
    {
        // хардкод говна
        Vector3 newPosition = transform.localPosition - new Vector3(0, 2, 0);
        _transformAnimator.TransformTowards(newPosition, null, () => ReadyForNewBox.Invoke());
    }
}
