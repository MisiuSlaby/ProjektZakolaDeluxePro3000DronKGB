using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float minAngle = -90f;
    public float maxAngle = 90f;

    [Range(0f, 1f)]
    public float value;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }

    private void Update()
    {
        float angle = Mathf.Lerp(minAngle, maxAngle, value);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void SetValue(float newValue)
    {
        value = Mathf.Clamp01(newValue);
    }
}
