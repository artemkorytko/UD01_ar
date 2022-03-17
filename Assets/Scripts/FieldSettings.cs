using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSettings : MonoBehaviour
{
    [SerializeField] private float minScale = 0.1f;
    [SerializeField] private float maxScale = 10f;
    
    public void ChangeScale(float delta)
    {
        Vector3 scale = gameObject.transform.localScale;
        scale.x = Mathf.Clamp(scale.x + delta, minScale, maxScale);
        scale.y = Mathf.Clamp(scale.y + delta, minScale, maxScale);
        scale.z = Mathf.Clamp(scale.z + delta, minScale, maxScale);
        gameObject.transform.localScale = scale;
    }

    public void ChangeRotation(float delta)
    {
        transform.Rotate(transform.up * delta,Space.Self);
    }
}
