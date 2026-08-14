using UnityEngine;

public class PlayerTrainController : MonoBehaviour
{
    [Header("Physics Parameters (V = S / T)")]
    public float distanceS;
    public float timeT;
    public float inputSpeedV; // ความเร็ว V ที่คำนวณได้

    [Header("Track Navigation")]
    public TrackPath trackPath;     // ลาก TrackPath มาใส่ หรือเว้นว่างไว้ก็ได้
    public float rotateSpeed = 8f;  // ความเร็วในการหมุนเลี้ยวตามราง

    [Header("References")]
    public GameManager gameManager;

    private bool isMoving = false;
    private int currentWaypointIndex = 0;

    public void StartEngine()
    {
        currentWaypointIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        // เช็คว่ามี TrackPath และมีจุด Waypoint หรือไม่
        if (trackPath != null && trackPath.waypoints != null && trackPath.waypoints.Count > 0)
        {
            // 🟢 กรณีที่ 1: มี TrackPath -> วิ่งตามจุดรางโค้ง
            MoveAlongTrack();
        }
        else
        {
            // 🟡 กรณีที่ 2: ไม่ใส่ TrackPath -> วิ่งตรงไปข้างหน้า
            MoveStraight();
        }
    }

    private void MoveAlongTrack()
    {
        if (currentWaypointIndex >= trackPath.waypoints.Count) return;

        Transform targetWaypoint = trackPath.waypoints[currentWaypointIndex];
        Vector3 targetPos = targetWaypoint.position;

        // เคลื่อนที่เข้าหา Waypoint ตามความเร็ว V
        transform.position = Vector3.MoveTowards(transform.position, targetPos, inputSpeedV * Time.deltaTime);

        // หมุนหัวรถไฟไปตามแนวราง
        Vector3 moveDirection = (targetPos - transform.position).normalized;
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        // เมื่อถึงจุด Waypoint ให้เปลี่ยนไปจุดถัดไป
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= trackPath.waypoints.Count)
            {
                isMoving = false; // แล่นสุดทางแล้ว
            }
        }
    }

    private void MoveStraight()
    {
        // วิ่งตรงไปตามทิศทางหน้าของรถไฟด้วยความเร็ว V
        transform.Translate(Vector3.forward * inputSpeedV * Time.deltaTime, Space.Self);
    }

    public void ResetTrain(Vector3 startPosition, Quaternion startRotation)
    {
        isMoving = false;
        currentWaypointIndex = 0;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObstacleTrain"))
        {
            isMoving = false;
            if (gameManager != null) gameManager.OnTrainCrashed();
        }
        else if (other.CompareTag("FinishLine"))
        {
            isMoving = false;
            if (gameManager != null) gameManager.OnTrainPassed();
        }
    }
}