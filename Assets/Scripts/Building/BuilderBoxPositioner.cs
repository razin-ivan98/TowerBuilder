using UnityEngine;

public class BuilderBoxPositioner
{
    private BoxesContainer _boxesContainer;
    private BuildingBoxesRegister _register;

    public BuilderBoxPositioner(BoxesContainer boxesContainer, BuildingBoxesRegister register)
    {
        _boxesContainer = boxesContainer;
        _register = register;
    }

    public BoxCatcher IntegrateBox(Box box, Vector2 point, float height)
    {
        box.transform.SetParent(_boxesContainer.transform);

        Vector3 newBoxPosition = CalculateBoxPosition(box.transform, point, height);
        _register.Register(box);
    
        return box.IntegrateToBuilding(newBoxPosition, () => _boxesContainer.moveDown() );
    }

    private Vector3 CalculateBoxPosition(Transform boxTransform, Vector3 pivotPoint, float height)
    {
        Vector3 v = new Vector3(pivotPoint.x, pivotPoint.y) - boxTransform.position;

        float xShift = Vector3.Project(v, boxTransform.right).magnitude * (Vector3.Dot(v, boxTransform.right) < 0 ? 1 : -1);
        // опять говнище доступ к чужому трансформу
        float previousBoxX = _boxesContainer.transform.InverseTransformPoint(pivotPoint).x;
    
        return new Vector3(xShift + previousBoxX, 2 * height, 0);
    }
}

