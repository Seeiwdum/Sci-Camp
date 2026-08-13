using UnityEngine;

public class ObstacleTrain : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 20f;
    public bool moveOnStart = false;

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