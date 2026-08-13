using UnityEngine;

public class ObstacleTrain : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 20f;

    [Tooltip("เปิดไว้เฉพาะถ้าต้องการทดสอบวิ่งทันที ให้ปิด (false) เพื่อรอปุ่ม Start")]
    public bool moveOnStart = false; // <-- ปรับเป็น false เพื่อไม่ให้วิ่งเองตอนกด Play

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