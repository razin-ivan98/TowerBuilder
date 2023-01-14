using UnityEngine;
using System;

public interface IStateSwitcher
{
    void SwitchState<T>() where T : BaseBoxState;
}

public interface IBoxBahaviour
{
    void Update();
    void IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess);
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

    virtual public void Update() {}

    virtual public void IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess)
    {
        throw new System.NotImplementedException("It is not possible to integrate Box to Building in this state");
    }

    virtual public void Start() {}

    virtual public void Stop() {}

    protected void Fall()
    {
        Falled?.Invoke();
    }
}
