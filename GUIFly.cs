using UnityEngine;
using System.Collections;

public class GUIFly : MonoBehaviour
{
    public enum InterpolationType
    {
        Linear,
        Sinusoidal,
        Hermite
    }

    public Vector3 m_InPosition;
    public Vector3 m_OutPosition;
    public float m_TravelTime = 0.5f;
    public float m_DelayToStartTravelingAfterMessageReceived = 0.1f;
    public bool m_StartWithInPosition = false;
    public InterpolationType m_InterpolationType = InterpolationType.Sinusoidal;

    void Start()
    {
        transform.position = (m_StartWithInPosition) ? m_InPosition : m_OutPosition;
    }

    public IEnumerator Fly(bool flyIn)
    {
        yield return new WaitForSeconds(m_DelayToStartTravelingAfterMessageReceived);

        Vector3 targetPosition = (flyIn) ? m_InPosition : m_OutPosition;
        float startTime = Time.time;
        Vector3 startPosition = transform.position;

        while (Time.time < startTime + m_TravelTime)
        {
            switch (m_InterpolationType)
            {
                case InterpolationType.Linear:
                    transform.position = Vector3.Lerp(startPosition, targetPosition, (Time.time - startTime) / m_TravelTime);
                    break;
                case InterpolationType.Sinusoidal:
                    transform.position = Sinerp(startPosition, targetPosition, (Time.time - startTime) / m_TravelTime);
                    break;
                case InterpolationType.Hermite:
                    transform.position = Hermite(startPosition, targetPosition, (Time.time - startTime) / m_TravelTime);
                    break;
            }
            yield return 0;
        }

        transform.position = targetPosition;
    }

    void Reset()
    {
        m_InPosition = transform.position;
    }
    
    private static Vector3 Sinerp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Sinerp(start.x, end.x, value), Sinerp(start.y, end.y, value), Sinerp(start.z, end.z, value));
    }

    private static Vector3 Hermite(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value), Hermite(start.z, end.z, value));
    }

    /* The following functions are also in the Mathfx script on the UnifyWiki, but are included here so the script is self sufficient. */

    private static float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }

    private static float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }
}