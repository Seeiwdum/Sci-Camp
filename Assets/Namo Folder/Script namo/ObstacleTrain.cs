using UnityEngine;

public class ObstacleTrain : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 20f;

    [Tooltip("ติ๊กถูกถ้าอยากให้รถไฟวิ่งทันทีที่กด Play (สำหรับทดสอบ)")]
    public bool moveOnStart = true;

    private bool isMoving = false;

    void Start()
    {
        if (moveOnStart)
        {
            StartMoving();
        }
    }

    void Update()
    {
        if (isMoving)
        {
            // วิ่งไปข้างหน้าตามแกน Z ของตัวโมเดล
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}