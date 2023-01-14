using UnityEngine;

public class BoxStateMachine: IStateSwitcher, IBoxBahaviour
{
    private BaseBoxState[] _states; 
    private BaseBoxState _state;

    private readonly IStateSwitcher _switcher;
    private readonly BoxCatcher _boxCatcher;
    private readonly Transform _transform;
    private readonly Rigidbody2D _rigidBody;
    private readonly TransformAnimator _trasnformAnimator;

    public BoxStateMachine(Transform transform, BoxCatcher boxCatcher, Rigidbody2D rigidBody, TransformAnimator transformAnimator)
    {
        _transform = transform;
        _boxCatcher = boxCatcher;
        _rigidBody = rigidBody;
        _trasnformAnimator = transformAnimator;
        _states = new BaseBoxState[]{ new FallingBoxState(_boxCatcher, _transform, _rigidBody, this, _trasnformAnimator),
                                      new CatchingBoxState(_boxCatcher, _transform, _rigidBody, this),
                                      new PassiveBoxState(_boxCatcher, _transform, _rigidBody, this) };
    }

    public void Update()
    {
        _state.Update();
    }

    public BoxCatcher IntegrateToBuilding(Vector3 localPosition, TransformAnimator.OnSuccessCallback onSuccess)
    {
        return _state.IntegrateToBuilding(localPosition, onSuccess);
    }

    public void SwitchState<T>() where T : BaseBoxState
    {
        foreach (var state in _states)
        {
            if (state is T)
            {
                _state?.Stop();
                _state = state;
                _state.Start();
            }
        }
    }

    public void Reset()
    {
        SwitchState<FallingBoxState>();
    }
}
