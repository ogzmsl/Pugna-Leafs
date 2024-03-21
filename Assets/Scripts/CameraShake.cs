using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;

   
    public float shakeDuration = 0f;

 
    public float shakeMagnitude = 0.7f;


    public float dampingSpeed = 1.0f;

    Vector3 initialPosition;

    public CameraShake cameraShake;

    void Awake()
    {
        if (cameraTransform == null)
        {
            cameraTransform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        initialPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            cameraTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
            
        }
        else
        {
            shakeDuration = 0f;
            cameraTransform.localPosition = initialPosition;
        }
    }

    public void TriggerShake()
    {
        shakeDuration = 0.3f;
    }
}
