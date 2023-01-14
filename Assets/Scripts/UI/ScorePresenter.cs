using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScorePresenter : MonoBehaviour
{
    [SerializeField] Building _building;
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        SetScore(3);
    }

    private void SetScore(int score)
    {
        _text.text = score.ToString();
    }

    private void OnEnable()
    {
        _building.ScoreChanged += SetScore;
    }
    private void OnDisable()
    {
        _building.ScoreChanged -= SetScore;
    }
}
