using UnityEngine;

public sealed class PassiveBoxState: BaseBoxState
{
    public PassiveBoxState(BoxCatcher boxCatcher, Transform transform, Rigidbody2D rigidBody, IStateSwitcher switcher)
        : base(boxCatcher, transform, rigidBody, switcher) {}

    public override void Start()
    {
        _rigidBody.bodyType = RigidbodyType2D.Static;
        _boxCatcher.Deactivate();
    }
}
