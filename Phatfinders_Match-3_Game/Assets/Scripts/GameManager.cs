using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Game Controller")]
    [Range(0, 180)]
    [SerializeField] private float maxTime = 60f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private TextMeshProUGUI timerTexts;

    [Header("Points")]
    [SerializeField] private TextMeshProUGUI[] pointsTexts;
    [SerializeField] private int[] points;

    // ------------------------

    private void OnEnable()
    {
        ElementCell.EarnPoints += EarnPoints;
    }

    private void OnDisable()
    {
        ElementCell.EarnPoints -= EarnPoints;        
    }

    private void Awake()
    {
        timer = maxTime;

        points = new int[pointsTexts.Length];
    }

    private void Update()
    {
        UpdateTimer();
    }

    // -----------------------

    private void UpdateTimer()
    {
        timer -= Time.deltaTime;

        timerTexts.text = timer.ToString("0");

        if (timer < 0)
        {
            RestartGame();
        }
    }

    public void EarnPoints(int id, int total)
    {
        points[id] += total;
        pointsTexts[id].text = points[id].ToString();
    }

    // ------------------------

    private void RestartGame()
    {
        timer = 0f;
    }

}
