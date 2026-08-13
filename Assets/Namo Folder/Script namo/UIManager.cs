using UnityEngine;
using TMPro; // รองรับ TextMeshPro ใน Unity 6.0

public class UIManager : MonoBehaviour
{
    [Header("UI Text References")]
    public TextMeshProUGUI statusText; // ข้อความแสดงสถานะ (ชนะ/แพ้/โกรธ)
    public TextMeshProUGUI timerText;  // ข้อความแสดงเวลาถอยหลัง

    void Start()
    {
        if (statusText != null) 
        {
            statusText.text = "Calculate and Play";
        }
    }

    // แสดงข้อความเมื่อชน
    public void ShowCrashed()
    {
        if (statusText != null) 
        {
            statusText.text = " Crashed! GAME OVER";
        }
    }

    // แสดงข้อความเมื่อผ่านปลอดภัย
    public void ShowPassed()
    {
        if (statusText != null) 
        {
            statusText.text = " Yeahh! YOU WIN";
        }
    }

    // แสดงข้อความเมื่อลูกค้าโกรธ
    public void ShowAngry()
    {
        if (statusText != null) 
        {
            statusText.text = " Too Slow! Costumer is Angry GAME OVER";
        }
    }

    // อัปเดตเวลาบนหน้าจอ
    public void UpdateTimerUI(float timeRemaining)
    {
        if (timerText != null)
        {
            timerText.text = "Costumer Can wait: " + timeRemaining.ToString("F1") + " Second..";
        }
    }
}