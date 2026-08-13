using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Tooltip("เวลาสูงสุดก่อนที่ผู้โดยสารจะโมโห (วินาที)")]
    public float maxPassengerPatience = 15f;
    
    [Header("References")]
    public PlayerTrainController playerTrain;

    [Header("Events")]
    public UnityEvent OnPassengerAngry; // Event เมื่อช้าเกินไป

    private bool isGameActive = false;
    private float timer = 0f;

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;

            // ตรวจสอบว่าช้าเกินไปจนลูกค้าโมโหหรือไม่
            if (timer > maxPassengerPatience)
            {
                TriggerPassengerAngry();
            }
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        timer = 0f;
        playerTrain.StartTrain();
        
        // สั่งให้รถไฟอุปสรรคเริ่มวิ่ง (ถ้ามีระบบจัดการรวม)
        ObstacleTrain[] obstacles = FindObjectsByType<ObstacleTrain>(FindObjectsSortMode.None);
        foreach (var obs in obstacles)
        {
            obs.StartMoving();
        }
    }

    private void TriggerPassengerAngry()
    {
        isGameActive = false;
        playerTrain.StopTrain();
        OnPassengerAngry?.Invoke();
        Debug.Log("ช้าเกินไป! ลูกค้าโมโห Game Over");
    }

    public void GameEnded() 
    {
        isGameActive = false;
    }
}