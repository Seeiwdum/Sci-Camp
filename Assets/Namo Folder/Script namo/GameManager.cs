using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public float customerMaxPatienceTime = 10f;

    [Header("References")]
    public PlayerTrainController playerTrain;
    public ObstacleTrain obstacleTrain;
    public UIManager uiManager;

    private float currentPatienceTimer;
    private bool isGameActive = false;

    void Start()
    {
        currentPatienceTimer = customerMaxPatienceTime;
    }

    void Update()
    {
        if (!isGameActive) return;

        currentPatienceTimer -= Time.deltaTime;
        if (uiManager != null)
        {
            uiManager.UpdateTimerUI(currentPatienceTimer);
        }

        if (currentPatienceTimer <= 0)
        {
            OnCustomerAngry();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        currentPatienceTimer = customerMaxPatienceTime;

        if (playerTrain != null) playerTrain.StartEngine();
        if (obstacleTrain != null) obstacleTrain.StartEngine();
    }

    // --- ฟังก์ชันที่ PlayerTrainController เรียกใช้ ---
    public void OnTrainPassed()
    {
        if (!isGameActive) return;
        isGameActive = false;

        if (uiManager != null) uiManager.ShowPassed();
    }

    public void OnTrainCrashed()
    {
        if (!isGameActive) return;
        isGameActive = false;

        if (uiManager != null) uiManager.ShowCrashed();
    }

    public void OnCustomerAngry()
    {
        if (!isGameActive) return;
        isGameActive = false;

        if (uiManager != null) uiManager.ShowAngry();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}