using UnityEngine;
using StarterAssets;
using Cinemachine;

public class LightControl : MonoBehaviour
{
    public Light DirectionalLight;
    public ThirdPersonController controller;
    public CinemachineVirtualCamera virtualCamera;

    [SerializeField] float lightChangeSpeed = 0.4f;

    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Update()
    {
        LightControllerMethod();
    }

    private void LightControllerMethod()
    {
        if (controller.LightContoller)
        {
            DirectionalLight.intensity -= Time.deltaTime * lightChangeSpeed;
            Invoke("StartCameraShake", 1.5f);
        }
        else if (DirectionalLight.intensity >= 0.2f && DirectionalLight.intensity <= 1.7f)
        {
            DirectionalLight.intensity += Time.deltaTime * lightChangeSpeed;
        }
    }

    private void StartCameraShake()
    {
        ShakeCamera(0.7f, 2f, 1.5f); 
    }

    private void ShakeCamera(float amplitude, float frequency, float duration)
    {
        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
        Invoke("StopShaking", duration);
    }

    private void StopShaking()
    {
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}
