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
            statusText.text = "คำนวณความเร็วแล้วกด Play";
        }
    }

    // แสดงข้อความเมื่อชน
    public void ShowCrashed()
    {
        if (statusText != null) 
        {
            statusText.text = "💥 รถไฟชนกัน! GAME OVER";
        }
    }

    // แสดงข้อความเมื่อผ่านปลอดภัย
    public void ShowPassed()
    {
        if (statusText != null) 
        {
            statusText.text = "🎉 ผ่านทางแยกปลอดภัย! YOU WIN";
        }
    }

    // แสดงข้อความเมื่อลูกค้าโกรธ
    public void ShowAngry()
    {
        if (statusText != null) 
        {
            statusText.text = "😡 ช้าเกินไป! ลูกค้าโมโห GAME OVER";
        }
    }

    // อัปเดตเวลาบนหน้าจอ
    public void UpdateTimerUI(float timeRemaining)
    {
        if (timerText != null)
        {
            timerText.text = "เวลาความอดทนลูกค้า: " + timeRemaining.ToString("F1") + " วินาที";
        }
    }
}