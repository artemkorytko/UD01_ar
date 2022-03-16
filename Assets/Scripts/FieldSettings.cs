using UnityEngine;

public class FieldSettings : MonoBehaviour
{
    [SerializeField] private float MinScale = 0.1f;
    [SerializeField] private float MaxScale = 10f;

    public void ChangeScale(float delta)
    {
        Vector3 scale = gameObject.transform.localScale;
        scale.x = Mathf.Clamp(scale.x + delta, MinScale, MaxScale);
        scale.y = Mathf.Clamp(scale.y + delta, MinScale, MaxScale);
        scale.z = Mathf.Clamp(scale.z + delta, MinScale, MaxScale);
        gameObject.transform.localScale = scale;
    }

    public void ChangeRotation(float delta)
    {
        transform.Rotate(transform.up * delta, Space.Self);
    }
}