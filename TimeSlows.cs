using UnityEngine;
using UnityEngine.UI;

public class TimeSlows : MonoBehaviour
{
    public float currentTimeScale = 1;

    public float minSpeed = 0.5f;
    public float maxSpeed = 3.0f;

    public Slider slider;

    private static TimeSlows instance;

    private void Awake()
    {
        instance = this;
    }

    public static TimeSlows GetInstance()
    {
        return instance;
    }
    public void Pause()
    {
        Time.timeScale = 0.01f;
        Debug.Log("Pause:" + Time.timeScale);
    }

    public void ChangeSpeed(float speed)
    {
        Time.timeScale = speed;
        currentTimeScale = speed;
        //Debug.Log("Change:" + Time.timeScale);
    }

    public void ChangeSpeed()
    {
        currentTimeScale = (maxSpeed - minSpeed) * slider.value + minSpeed;
        Time.timeScale = currentTimeScale;

        //Debug.Log("Speed:" + Time.timeScale);
    }

    public void Recover()
    {
        Time.timeScale = currentTimeScale;
        Debug.Log("Recover:" + Time.timeScale);
    }
}
