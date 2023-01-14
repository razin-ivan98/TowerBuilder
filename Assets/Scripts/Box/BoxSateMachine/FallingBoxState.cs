using UnityEngine;

public sealed class FallingBoxState: BaseBoxState
{
    private TransformAnimator _transformAnimator;
    private const float DestroyY = -8f;

    public FallingBoxState(BoxCatcher boxCatcher,
                           Transform transform,
                           Rigidbody2D rigidBody,
                           IStateSwitcher switcher,
                           TransformAnimator transformAnimator)
        : base(boxCatcher, transform, rigidBody, switcher)
    {
        _transformAnimator = transformAnimator;
    }

    public override void Start()
    {
        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _boxCatcher.Deactivate();
    }

    public override void Stop()
    {
        _transformAnimator.Reset();
    }

    public override void Update()
    {
        if (this._transform.position.y < DestroyY)
            Fall();
    }

    public override void IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess)
    {
        _rigidBody.bodyType = RigidbodyType2D.Static;
        _transformAnimator.TransformTowards(localPosition, Quaternion.identity, () => {
            onSuccess();
            _switcher.SwitchState<CatchingBoxState>();
        } );
    }
}
