using UnityEngine;
using UnityEngine.Events;

public class PlayerTrainController : MonoBehaviour
{
    [Header("Physics Settings")]
    public float distanceS = 100f;
    public float timeT = 10f;

    [Header("Student Input")]
    public float inputSpeedV = 0f;

    [Header("Game Status")]
    public bool isRunning = false;

    [Header("Events")]
    public UnityEvent OnTrainCrashed;
    public UnityEvent OnTrainPassed;

    void Update()
    {
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