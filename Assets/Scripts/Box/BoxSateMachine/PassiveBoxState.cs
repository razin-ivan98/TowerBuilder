using UnityEngine;

public sealed class PassiveBoxState: BaseBoxState
{
    private const float DestroyY = -8f;

    public PassiveBoxState(BoxCatcher boxCatcher, Transform transform, Rigidbody2D rigidBody, IStateSwitcher switcher)
        : base(boxCatcher, transform, rigidBody, switcher)
    {}

    public override void Start()
    {
        _rigidBody.bodyType = RigidbodyType2D.Static;
        _boxCatcher.Deactivate();
    }

    public override void Stop() {}

    public override void Update() {}

    public override BoxCatcher IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess)
    {
        throw new System.NotImplementedException("Нельзя интегрировать бокс в этом состоянии");
    }
}
