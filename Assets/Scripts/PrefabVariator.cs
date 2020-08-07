using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PrefabVariator : MonoBehaviour
{
    [SerializeField] Vector2 sizeVariation;

    private void Start()
    {
        RandomizeScale();
        RandomizeRotation();
    }

    void RandomizeScale()
    {
        transform.localScale = transform.localScale * Random.Range(sizeVariation.x, sizeVariation.y);
    }

    void RandomizeRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, Random.Range(0, 360), transform.rotation.z);
    }
}
