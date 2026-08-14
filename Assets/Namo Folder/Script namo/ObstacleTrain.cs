using UnityEngine;

public class ObstacleTrain : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Vector3 moveDirection = Vector3.forward;

    [Header("Track Navigation (Optional - ไม่ใส่จะวิ่งตรง)")]
    public TrackPath trackPath;
    public float rotateSpeed = 8f;

    private bool isMoving = false;
    private int currentWaypointIndex = 0;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void StartEngine()
    {
        currentWaypointIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving) return;

        if (trackPath != null && trackPath.waypoints != null && trackPath.waypoints.Count > 0)
        {
            MoveAlongTrack();
        }
        else
        {
            MoveStraight();
        }
    }

    private void MoveAlongTrack()
    {
        if (currentWaypointIndex >= trackPath.waypoints.Count) return;

        Transform targetWaypoint = trackPath.waypoints[currentWaypointIndex];
        Vector3 targetPos = targetWaypoint.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        Vector3 dir = (targetPos - transform.position).normalized;
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= trackPath.waypoints.Count)
            {
                isMoving = false;
            }
        }
    }

    private void MoveStraight()
    {
        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    public void ResetTrain()
    {
        isMoving = false;
        currentWaypointIndex = 0;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}