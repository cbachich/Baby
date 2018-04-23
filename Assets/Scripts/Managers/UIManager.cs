using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private TextMeshProUGUI timeRemaining;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private Image[] babyDeathIndicators;

    [SerializeField]
    private Sprite babyFaceSilohouette;
    [SerializeField]
    private Sprite babyFaceDeath;
    [SerializeField]
    private Color babyFaceSilohouetteColor;

    private GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        score.text = gameManager.Score.ToString();
        timeRemaining.text = FormatTimeText((int)gameManager.TimeRemaining);

        gameOverPanel.SetActive(gameManager.IsGameOver);

        for(int i=0; i<babyDeathIndicators.Length; i++) {
            babyDeathIndicators[i].sprite = i >= gameManager.BabyDeathCount ? babyFaceSilohouette : babyFaceDeath;
            babyDeathIndicators[i].color = (i >= gameManager.BabyDeathCount) ? babyFaceSilohouetteColor : Color.white;
        }
    }

    private string FormatTimeText(int seconds) {
        var mins = seconds / 60;
        var secs = seconds % 60;
        return string.Format("{0:D1}:{1:D2}", mins, secs);
    }
}