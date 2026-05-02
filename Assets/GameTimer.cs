using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 120f;
    public TMP_Text timerText;
    public GameObject gameOverText;

    bool isGameOver = false;

    void Update()
    {
        if (isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            timeRemaining = 0;
            GameOver();
        }
    }

    void UpdateUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString();
    }

    void GameOver()
    {
        isGameOver = true;

        Debug.Log("Game Over - Time Up");

        gameOverText.SetActive(true); // 👈 SHOW TEXT

        Time.timeScale = 0;
    }
}