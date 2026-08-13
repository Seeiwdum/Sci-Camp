using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Tooltip("เวลาสูงสุดก่อนที่ผู้โดยสารจะโมโห (วินาที)")]
    public float maxPassengerPatience = 15f;
    
    [Tooltip("ติ๊กถูกถ้าอยากให้เริ่มนับเวลาและสั่งวิ่งทันทีเมื่อกด Play")]
    public bool autoStartOnPlay = true;

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

            // คำนวณเวลาที่เหลือแล้วส่งไปแสดงผลบน UI
            float timeRemaining = maxPassengerPatience - timer;
            if (uiManager != null)
            {
                uiManager.UpdateTimerUI(Mathf.Max(0, timeRemaining));
            }

            // ตรวจสอบว่าช้าเกินไปจนลูกค้าโมโหหรือไม่
            if (timer > maxPassengerPatience)
            {
                TriggerPassengerAngry();
            }
        }
    }

    [ContextMenu("Start Game Test")]
    public void StartGame()
    {
        isGameActive = true;
        timer = 0f;
        
        if (playerTrain != null)
        {
            playerTrain.StartTrain();
        }
        
        // ค้นหารถไฟขัดขวางทั้งหมดในฉากแล้วสั่งให้เริ่มวิ่ง
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

        OnPassengerAngry?.Invoke();
        Debug.Log("Too Slow! Costumer is Angry Game Over");
    }

    public void GameEnded() 
    {
        isGameActive = false;
    }
}