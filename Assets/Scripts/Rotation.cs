using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rotateThatShit;

    void Update()
    {
        transform.Rotate(_rotateThatShit * Time.deltaTime);
    }
}
