using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Text References")]
    public TextMeshProUGUI statusText;        // ข้อความแสดงสถานะ
    public TextMeshProUGUI timerText;         // ข้อความแสดงเวลาถอยหลัง
    public TextMeshProUGUI calculatedVText;   // ข้อความแสดงผลการคำนวณ V = S / T

    [Header("UI Control References")]
    public TMP_InputField distanceSInputField; // ช่องกรอกระยะทาง S (เมตร)
    public TMP_InputField timeTInputField;     // ช่องกรอกเวลา T (วินาที)
    public Button startButton;                 // ปุ่มกดออกรถ
    public Button restartButton;               // ปุ่มกดเริ่มใหม่ (Restart)

    [Header("Script References")]
    public GameManager gameManager;
    public PlayerTrainController playerTrain;

    void Start()
    {
        if (statusText != null) 
        {
            statusText.text = "กรอกระยะทาง S  และ เวลา T  เพื่อคำนวณความเร็ว V = S / T";
        }

        if (calculatedVText != null)
        {
            calculatedVText.text = "ผลการคำนวณ V = m/s";
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
            restartButton.gameObject.SetActive(false); // ซ่อนไว้ตอนเริ่ม
        }
    }

    public void OnStartButtonClicked()
    {
        if (distanceSInputField == null || timeTInputField == null || playerTrain == null) return;

        // แปลงค่าที่กรอกเป็นตัวเลข float
        bool isSValid = float.TryParse(distanceSInputField.text, out float inputS);
        bool isTValid = float.TryParse(timeTInputField.text, out float inputT);

        if (isSValid && isTValid)
        {
            // เช็คป้องกันการหารด้วยศูนย์ (T ต้องไม่เท่ากับ 0)
            if (inputT <= 0)
            {
                if (statusText != null)
                {
                    statusText.text = "เวลา ต้องมากกว่า 0 วินาที!";
                }
                return;
            }

            // 🧮 คำนวณความเร็ว V = S / T
            float calculatedV = inputS / inputT;

            // ส่งค่าไปที่รถไฟผู้เล่น
            playerTrain.distanceS = inputS;
            playerTrain.timeT = inputT;
            playerTrain.inputSpeedV = calculatedV;

            // แสดงผลการคำนวณให้เด็กๆ เห็นบน UI
            if (calculatedVText != null)
            {
                calculatedVText.text = $"คำนวณได้ V = {inputS} / {inputT} = <color=yellow>{calculatedV:F2} m/s</color>";
            }

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
                statusText.text = "กรุณากรอกตัวเลข S และ T ให้ถูกต้อง!";
            }
        }
    }

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
            statusText.text = "รถไฟชนกัน! GAME OVER";
        }
        ShowRestartUI();
    }

    public void ShowPassed()
    {
        if (statusText != null) 
        {
            statusText.text = " ผ่านทางแยกปลอดภัย! YOU WIN";
        }
        ShowRestartUI();
    }

    public void ShowAngry()
    {
        if (statusText != null) 
        {
            statusText.text = "ช้าเกินไป! ลูกค้าโมโห GAME OVER";
        }
        
        if (timerText != null)
        {
            timerText.text = "เวลาความอดทนลูกค้า: 0.0 วินาที";
        }

        ShowRestartUI();
    }

    public void UpdateTimerUI(float timeRemaining)
    {
        if (timerText != null)
        {
            timerText.text = "เวลาความอดทนลูกค้า: " + timeRemaining.ToString("F1") + " วินาที";
        }
    }

    private void SetUIInteractable(bool state)
    {
        if (distanceSInputField != null) distanceSInputField.interactable = state;
        if (timeTInputField != null) timeTInputField.interactable = state;
        if (startButton != null) startButton.interactable = state;
    }

    private void ShowRestartUI()
    {
        SetUIInteractable(false);
        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(true);
        }
    }
}