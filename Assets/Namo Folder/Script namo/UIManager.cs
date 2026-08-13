using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Text References")]
    public TextMeshProUGUI statusText;   // ข้อความแสดงสถานะ
    public TextMeshProUGUI timerText;    // ข้อความแสดงเวลาถอยหลัง

    [Header("UI Control References")]
    public TMP_InputField speedInputField; // ช่องกรอกค่าความเร็ว V
    public Button startButton;             // ปุ่มกดออกรถ
    public Button restartButton;           // ปุ่มกดเริ่มใหม่ (Restart)

    [Header("Script References")]
    public GameManager gameManager;
    public PlayerTrainController playerTrain;

    void Start()
    {
        if (statusText != null) 
        {
            statusText.text = "คำนวณความเร็ว V (m/s) แล้วพิมพ์ลงในช่องด้านล่าง";
        }

        // ผูก Event ปุ่มกดออกรถ
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        // ผูก Event ปุ่มกดเริ่มใหม่
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
            restartButton.gameObject.SetActive(false); // ซ่อนปุ่ม Restart ไว้ตอนเริ่มเกม
        }
    }

    public void OnStartButtonClicked()
    {
        if (speedInputField != null && playerTrain != null)
        {
            if (float.TryParse(speedInputField.text, out float inputSpeed))
            {
                playerTrain.inputSpeedV = inputSpeed;
                SetUIInteractable(false);

                if (gameManager != null)
                {
                    gameManager.StartGame();
                }
            }
            else
            {
                if (statusText != null)
                {
                    statusText.text = "⚠️ กรุณากรอกตัวเลขความเร็วให้ถูกต้อง!";
                }
            }
        }
    }

    // เมื่อกดปุ่ม Restart บนหน้าจอ
    public void OnRestartButtonClicked()
    {
        if (gameManager != null)
        {
            gameManager.RestartGame();
        }
    }

    public void ShowCrashed()
    {
        if (statusText != null) 
        {
            statusText.text = "💥 รถไฟชนกัน! GAME OVER\n<size=80%>(ความเร็วมากเกินไป หรือมาถึงช่วงทางตัดพร้อมกับรถไฟอีกขบวน)</size>";
        }
        ShowRestartUI();
    }

    public void ShowPassed()
    {
        if (statusText != null) 
        {
            statusText.text = "🎉 ผ่านทางแยกปลอดภัย! YOU WIN\n<size=80%>(คำนวณความเร็วได้ถูกต้องยอดเยี่ยม!)</size>";
        }
        ShowRestartUI();
    }

    // แสดงข้อความเมื่อเวลาหมด + เฉลยการคำนวณทางฟิสิกส์
    public void ShowAngryTimeout(float s, float t, float currentV)
    {
        float correctV = s / t; // สูตร V = S / T
        
        if (statusText != null) 
        {
            statusText.text = $" <b>เวลาหมด! ลูกค้าโมโห GAME OVER</b>\n" +
                              $"ระยะทาง S = {s} m | เวลาที่มี T = {t} s\n" +
                              $"ความเร็วที่คุณกรอก: <color=red>{currentV} m/s</color> (ช้าไป!)\n" +
                              $"<color=yellow>ความเร็วขั้นต่ำที่ต้องใช้: V = S/T = {correctV:F1} m/s</color>";
        }
        
        if (timerText != null)
        {
            timerText.text = "เวลาหมดลงแล้ว! (0.0 วินาที)";
        }

        ShowRestartUI();
    }

    public void UpdateTimerUI(float timeRemaining)
    {
        if (timerText != null)
        {
            timerText.text = "Passenger tolerance: " + timeRemaining.ToString("F1") + " Second...";
        }
    }

    private void SetUIInteractable(bool state)
    {
        if (speedInputField != null) speedInputField.interactable = state;
        if (startButton != null) startButton.interactable = state;
    }

    // เปิดการแสดงปุ่ม Restart เมื่อเกมจบ
    private void ShowRestartUI()
    {
        SetUIInteractable(false);
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
    }
}