using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(HingeJoint2D))]
public class Crane : MonoBehaviour
{
    [SerializeField] private Building _building;
    private Box _currenBox;
    private Rigidbody2D _currentBoxRB;
    private Rigidbody2D _rigidBody;
    private HingeJoint2D _joint;

    [SerializeField] private BoxesFactory _boxesFactory;

    private void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
        PrepareNewBox();
    }

    private void OnValidate()
    {
        if (!_building)
            throw new System.Exception("Building не определено");
    }

    public void Release()
    {
        _joint.enabled = false;
    }

// кал
    private void PrepareNewBox()
    {
        if (_currenBox)
            _currenBox.Falled -= onCurrentBoxFalled;

        _currenBox = _boxesFactory.getBox().GetComponent<Box>();

        _currenBox.Falled += onCurrentBoxFalled;

        _currentBoxRB = _currenBox?.GetComponent<Rigidbody2D>();
        _currentBoxRB.transform.position = _rigidBody.position - new Vector2(0, 3.0f);
        _joint.connectedBody = _currentBoxRB;
        _joint.enabled = true;
        _currentBoxRB.AddForce(new Vector2(100, 0));
    }

    private void onCurrentBoxFalled()
    {
        _boxesFactory.freeBox(_currenBox);
        PrepareNewBox();
    }

    private void OnEnable()
    {
        BoxesContainer.ReadyForNewBox += PrepareNewBox;
    }
    private void OnDisable()
    {
        BoxesContainer.ReadyForNewBox -= PrepareNewBox;
    }
}
