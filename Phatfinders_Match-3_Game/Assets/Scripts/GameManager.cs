using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Grid Manager")]
    [SerializeField] private GridManager gridManager;

    [Header("Game Controller")]
    [Range(0, 180)]
    [SerializeField] private float maxTime = 60f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private TextMeshProUGUI timerTexts;
    [SerializeField] private GameObject menuLayer;

    [Header("Points")]
    [SerializeField] private TextMeshProUGUI[] pointsTexts;
    private int[] points;

    private bool stopGame = true;

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

        stopGame = true;
        menuLayer.SetActive(true);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = 0;
            pointsTexts[i].text = "0";
        }

        timerTexts.text = maxTime.ToString();
    }

    private void Update()
    {
        UpdateTimer();
    }

    // -----------------------

    private void UpdateTimer()
    {
        if (!stopGame)
        {
            timer -= Time.deltaTime;

            timerTexts.text = timer.ToString("0");

            if (timer < 0)
            {
                CallMenu();
            }
        }
    }

    public void EarnPoints(int id, int total)
    {
        points[id] += total;
        pointsTexts[id].text = points[id].ToString();
    }

    // ------------------------

    public void CallMenu()
    {
        menuLayer.SetActive(true);

        stopGame = true;

        gridManager.CleanGrid();
        gridManager.StartGrid(false);
    }

    public void StartGame()
    {
        menuLayer.SetActive(false);
        stopGame = false;

        gridManager.StartGrid(true);

        timer = maxTime;

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = 0;
            pointsTexts[i].text = "0";
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}