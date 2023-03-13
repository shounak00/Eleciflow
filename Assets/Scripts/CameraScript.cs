using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public bool shake;
    public float shakeAmount;
    public float shakeDuration;

    private Vector3 targetPosition;

    private void Update()
    {
        targetPosition = new Vector3(0,0,-10);
        if (shake)
        {
            Vector3 shakeVector = targetPosition + Random.insideUnitSphere * shakeAmount;
            targetPosition = new Vector3(shakeVector.x, shakeVector.y, targetPosition.z);
        }
        transform.position = targetPosition;
    }

    public void eventLevelStart()
    {
        Shake();
    }

    private void Shake()
    {
        shake = true;
        Invoke("stopShake", shakeDuration);
    }

    private void stopShake()
    {
        shake = false;
    }
}