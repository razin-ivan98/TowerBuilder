using UnityEngine;
using System;

[RequireComponent(typeof(BuildingRocker), typeof(BuildingBoxesRegister), typeof(InstabilityCalculator))]
public class Building : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    private BoxesContainer _boxesContainer;

    private BuildingRocker _rocker;
    private BuildingBoxesRegister _register;
    private InstabilityCalculator _instabilityCalculator;
    private BuilderBoxPositioner _boxPositioner;
    private ICatchingActioned _catcher;
    public event Action<int> ScoreChanged;

    public void Start()
    {
        _rocker = GetComponent<BuildingRocker>();
        _register = GetComponent<BuildingBoxesRegister>();
        _instabilityCalculator = GetComponent<InstabilityCalculator>();
        // ну такое, хотя бы валидацию
        _boxesContainer = GetComponentInChildren<BoxesContainer>();

        _boxPositioner = new BuilderBoxPositioner(_boxesContainer);

        // выкинуть этот кал

        _register.PrepareShit();

        Box lastBox = _register.LastBox;
        _catcher = lastBox.gameObject.GetComponentInChildren<BoxCatcher>();
        _catcher.BoxCatched += AddBox;

        _gameData.Height = 3;
    }

    public void AddBox(Box box, Vector2 point)
    {
        _catcher.BoxCatched -= AddBox;
        _boxPositioner.IntegrateBox(box, point, _gameData.Height);
        _register.Register(box);
        _catcher = box.Catcher;
        _catcher.BoxCatched += AddBox;

        updateInstability();
        _gameData.Height += 1;

        ScoreChanged?.Invoke(_gameData.Height);
    }

    private void updateInstability()
    {
        _gameData.Instability = _instabilityCalculator.Calculate();
    }
}
