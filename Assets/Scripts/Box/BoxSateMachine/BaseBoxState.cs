using UnityEngine;
using System;

public interface IStateSwitcher
{
    void SwitchState<T>() where T : BaseBoxState;
}

public interface IBoxBahaviour
{
    void Update();
    BoxCatcher IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess);
}

public abstract class BaseBoxState: IBoxBahaviour
{
    protected readonly IStateSwitcher _switcher;
    protected readonly BoxCatcher _boxCatcher;
    protected readonly Transform _transform;
    protected readonly Rigidbody2D _rigidBody;
    public event Action Falled;

    public BaseBoxState(BoxCatcher boxCatcher,
                        Transform transform,
                        Rigidbody2D rigidBody,
                        IStateSwitcher switcher)
    {
        _boxCatcher = boxCatcher;
        _transform = transform;
        _rigidBody = rigidBody;
        _switcher = switcher;
    }

    abstract public void Update();

    abstract public BoxCatcher IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess);

    abstract public void Start();

    abstract public void Stop();

    protected void Fall()
    {
        Falled?.Invoke();
    }
}
