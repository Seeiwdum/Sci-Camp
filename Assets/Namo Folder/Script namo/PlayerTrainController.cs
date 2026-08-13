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

    [Header("Events")]
    public UnityEvent OnTrainCrashed;
    public UnityEvent OnTrainPassed;

    void Update()
    {
        // ถ้าสั่งให้วิ่ง ให้รถไฟเคลื่อนที่ไปข้างหน้าตามความเร็ว inputSpeedV
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
            Debug.Log("Crashed! Game Over");
        }
        else if (other.CompareTag("FinishLine"))
        {
            StopTrain();
            OnTrainPassed?.Invoke();
            Debug.Log("Yeah! You Win!");
        }
    }
}