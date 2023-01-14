using UnityEngine;

[RequireComponent(typeof(BuildingBoxesRegister))]
public class InstabilityCalculator : MonoBehaviour
{
    private BuildingBoxesRegister _register;
    private float _multiplicator = 0.4f;

    private void Start()
    {
        _register = GetComponent<BuildingBoxesRegister>();
    }

    public float Calculate()
    {
        float newInstability = 0;
        Box prev = null;
        foreach (Box box in _register.Boxes)
        {
            if (prev)
            {
                newInstability += Mathf.Abs(box.transform.localPosition.x - prev.transform.localPosition.x);
            }
            prev = box;
        }
        return newInstability * _multiplicator;
    }
}
