using UnityEngine;
using System.Collections.Generic;

public class BuildingBoxesRegister : MonoBehaviour
{
    [SerializeField] private BoxesFactory _boxesFactory;
    [SerializeField] private int _maxBoxesCount = 5;
    [SerializeField] private BoxesContainer _boxesContainer;

    public Box LastBox { get; private set; } 

    public Queue<Box> Boxes { get; } = new Queue<Box>();

    public void PrepareShit()
    {
// и этот кал выкинуть
        Box newBox = _boxesFactory.getBox().GetComponent<Box>();
        newBox.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        newBox.transform.SetParent(_boxesContainer.transform);
        newBox.transform.localPosition = new Vector3(0, 2 * Boxes.Count, 0);
        

        Boxes.Enqueue(newBox);

        newBox = _boxesFactory.getBox().GetComponent<Box>();
        newBox.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        newBox.transform.SetParent(_boxesContainer.transform);

        newBox.transform.localPosition = new Vector3(0, 2 * Boxes.Count, 0);
        

        Boxes.Enqueue(newBox);

        newBox = _boxesFactory.getBox().GetComponent<Box>();
        newBox.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        newBox.GetComponentInChildren<BoxCatcher>().Activate();

        newBox.transform.SetParent(_boxesContainer.transform);
        newBox.transform.localPosition = new Vector3(0, 2 * Boxes.Count, 0);
        

        Boxes.Enqueue(newBox);

        LastBox = newBox;
    }

    public void Register(Box box)
    {
        Boxes.Enqueue(box);
        LastBox = box;
        FreeExtraBoxes();
    }

    private void FreeExtraBoxes()
    {
        if (Boxes.Count > _maxBoxesCount)
        {
            Box boxToFree = Boxes.Dequeue();
            _boxesFactory.freeBox(boxToFree);
        }
    }
}
