using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Tooltip("เวลาสูงสุดก่อนที่ผู้โดยสารจะโมโห (วินาที)")]
    public float maxPassengerPatience = 15f;
    
    public bool autoStartOnPlay = false;

    [Header("References")]
    public PlayerTrainController playerTrain;
    public UIManager uiManager;

    [Header("Events")]
    public UnityEvent OnPassengerAngry;

    private bool isGameActive = false;
    private float timer = 0f;

    void Start()
    {
        if (autoStartOnPlay)
        {
            StartGame();
        }
    }

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;

            float timeRemaining = maxPassengerPatience - timer;
            if (uiManager != null)
            {
                uiManager.UpdateTimerUI(Mathf.Max(0, timeRemaining));
            }

            // เมื่อเวลารอของลูกค้าหมดลง
            if (timer >= maxPassengerPatience)
            {
                TriggerPassengerAngry();
            }
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        timer = 0f;
        
        if (playerTrain != null)
        {
            playerTrain.StartTrain();
        }
        
        ObstacleTrain[] obstacles = FindObjectsByType<ObstacleTrain>(FindObjectsSortMode.None);
        foreach (var obs in obstacles)
        {
            obs.StartMoving();
        }
    }

    private void TriggerPassengerAngry()
    {
        isGameActive = false;

        if (playerTrain != null)
        {
            playerTrain.StopTrain();
        }

        ObstacleTrain[] obstacles = FindObjectsByType<ObstacleTrain>(FindObjectsSortMode.None);
        foreach (var obs in obstacles)
        {
            obs.StopMoving();
        }

        if (uiManager != null)
        {
            uiManager.ShowAngry();
        }

        OnPassengerAngry?.Invoke();
        Debug.Log("ช้าเกินไป! เวลาหมด ลูกค้าโมโห Game Over");
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GameEnded() 
    {
        isGameActive = false;
    }
}