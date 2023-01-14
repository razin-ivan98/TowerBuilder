using UnityEngine;

public sealed class CatchingBoxState: BaseBoxState
{
    private const float DestroyY = -8f;

    public CatchingBoxState(BoxCatcher boxCatcher, Transform transform, Rigidbody2D rigidBody, IStateSwitcher switcher)
        : base(boxCatcher, transform, rigidBody, switcher)
    {}

    public override void Start()
    {
        _rigidBody.bodyType = RigidbodyType2D.Static;
        _boxCatcher.Activate();
        _boxCatcher.BoxCatched += OnBoxCatched;
    }

    private void OnBoxCatched(Box box, Vector2 point)
    {
        _switcher.SwitchState<PassiveBoxState>();
    }

    public override void Stop()
    {
        _boxCatcher.BoxCatched -= OnBoxCatched;
    }

    public override void Update() {}

    public override BoxCatcher IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess)
    {
        throw new System.NotImplementedException("Нельзя интегрировать бокс в этом состоянии");
    }
}
