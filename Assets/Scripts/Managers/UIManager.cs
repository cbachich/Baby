using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour {
    [SerializeField]
    private Text score;

    private GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        score.text = "Score: " + gameManager.Score;
    }
}