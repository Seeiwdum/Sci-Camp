using UnityEngine;
using UnityEngine.Events;

public class PlayerTrainController : MonoBehaviour
{
    [Header("Physics Lesson: v = s/t")]
    [Tooltip("ระยะทางถึงเป้าหมาย (S)")]
    public float distanceS = 100f;
    
    [Tooltip("เวลาที่ปลอดภัยในการผ่านทางแยก (T)")]
    public float timeT = 10f;

    [Header("Student Input")]
    [Tooltip("ให้เด็กๆ คำนวณความเร็วแล้วนำมากรอกที่นี่ (V)")]
    public float inputSpeedV = 0f;

    [Header("Game Status")]
    public bool isRunning = false;

    [Header("Events (ลาก UI หรือ Effect มาใส่ได้เลย)")]
    public UnityEvent OnTrainCrashed;
    public UnityEvent OnTrainPassed;

    void Update()
    {
        // ถ้าเกมเริ่มแล้ว ให้รถไฟวิ่งไปข้างหน้าด้วยความเร็วที่เด็กๆ ตั้งไว้
        if (isRunning)
        {
            transform.Translate(Vector3.forward * inputSpeedV * Time.deltaTime);
        }
    }

    public void StartTrain()
    {
        isRunning = true;
    }

    public void StopTrain()
    {
        isRunning = false;
    }

    // ตรวจจับการชน
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleTrain"))
        {
            StopTrain();
            OnTrainCrashed?.Invoke();
            Debug.Log("รถไฟชนกัน! Game Over");
        }
        else if (other.CompareTag("FinishLine"))
        {
            StopTrain();
            OnTrainPassed?.Invoke();
            Debug.Log("ผ่านทางแยกปลอดภัย! You Win!");
        }
    }
}