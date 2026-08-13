using UnityEngine;

public class ObstacleTrain : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 20f;
    private bool isMoving = false;

    void Update()
    {
        if (isMoving)
        {
            // วิ่งไปข้างหน้าตามแกน Z (ปรับตามโมเดลของคุณ)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }
}