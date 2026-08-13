using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement; // ต้องมี namespace นี้เพื่อรีโหลดฉากเกม

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Tooltip("เวลาสูงสุดก่อนที่ผู้โดยสารจะโมโห (วินาที) (T)")]
    public float maxPassengerPatience = 15f;
    
    [Tooltip("ปิดไว้เพื่อให้เกมรอเด็กๆ พิมพ์ความเร็วและกดปุ่ม Start บนหน้าจอก่อน")]
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

        // สั่งหยุดรถไฟผู้เล่นและรถไฟขัดขวาง
        if (playerTrain != null)
        {
            playerTrain.StopTrain();
        }

        ObstacleTrain[] obstacles = FindObjectsByType<ObstacleTrain>(FindObjectsSortMode.None);
        foreach (var obs in obstacles)
        {
            obs.StopMoving();
        }

        // แจ้ง UIManager ให้แสดงข้อความเวลาหมด + เฉลย
        if (uiManager != null && playerTrain != null)
        {
            uiManager.ShowAngryTimeout(playerTrain.distanceS, maxPassengerPatience, playerTrain.inputSpeedV);
        }

        OnPassengerAngry?.Invoke();
        Debug.Log("ช้าเกินไป! เวลาหมด ลูกค้าโมโห Game Over");
    }

    // ฟังก์ชันสั่งเริ่มเกมใหม่ (Reload Scene)
    public void RestartGame()
    {
        // โหลดฉากปัจจุบันขึ้นมาใหม่ทันที
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GameEnded() 
    {
        isGameActive = false;
    }
}