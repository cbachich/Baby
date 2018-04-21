using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour {
    [SerializeField]
    private Text score;

    [SerializeField]
    private Text timeRemaining;

    [SerializeField]
    private GameObject gameOverPanel;

    private GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        score.text = "Score - " + gameManager.Score;
        timeRemaining.text = "Time - " + FormatTimeText((int)gameManager.TimeRemaining);

        gameOverPanel.SetActive(gameManager.IsGameOver);
    }

    private string FormatTimeText(int seconds) {
        var mins = seconds / 60;
        var secs = seconds % 60;
        return string.Format("{0:D1}:{1:D2}", mins, secs);
    }
}