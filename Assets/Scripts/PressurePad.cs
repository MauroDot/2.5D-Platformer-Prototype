using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    //detect moving box
    //when close to center
    //disable the box's rigidbody or set it to kinematic
    //change color of pressure pad to blue

    private bool _isActivated = false;
    private Renderer _renderer;
    private Color _inactiveColor = Color.red;
    private Color _activeColor = Color.green;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = _inactiveColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActivated && other.CompareTag("MovingBox"))
        {
            _isActivated = true;

            Rigidbody boxRigidbody = other.GetComponent<Rigidbody>();
            if (boxRigidbody != null)
            {
                boxRigidbody.isKinematic = true;
            }

            _renderer.material.color = _activeColor;
        }
    }

    //or
    /*private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Moving Box")
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            Debug.Log("Distance: " + distance);

            if(distance < 0.05f)
            {
                Rigidbody box = other.GetComponent<Rigidbody>();
                if(box != null)
                {
                    box.isKinematic = true;
                }
                MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
                if(renderer != null)
                {
                    renderer.material.color = Color.green;
                }
                Destroy(this);
            }
        }
    }*/
}
