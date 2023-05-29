using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed = 2.0f;

    private Vector3 destination;
    private bool elevatorActive = false;
    private bool movingToTarget = false;

    private void Start()
    {
        // Set the initial destination to the origin position
        destination = _origin.position;
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate() called");

        if (elevatorActive)
        {
            // Move towards the destination position
            transform.position = Vector3.MoveTowards(transform.position, destination, _speed * Time.deltaTime);

            // Check if the destination position has been reached
            if (transform.position == destination)
            {
                if (destination == _target.position)
                {
                    // Reached the target, stop if not moving to origin
                    if (!movingToTarget)
                    {
                        elevatorActive = false;
                    }
                }
                else if (destination == _origin.position)
                {
                    // Reached the origin, stop
                    elevatorActive = false;
                }
            }
        }
    }

    public void ActivateElevator()
    {
        elevatorActive = true;
    }

    public void DeactivateElevator()
    {
        elevatorActive = false;
    }

    public bool IsActive()
    {
        return elevatorActive;
    }

    public void MoveToTarget()
    {
        destination = _target.position;
        movingToTarget = true;
        elevatorActive = true; // Activate the elevator when moving to target
    }

    public void MoveToOrigin()
    {
        destination = _origin.position;
        movingToTarget = false;
        elevatorActive = true; // Activate the elevator when moving to origin
    }

    public bool IsMovingToTarget()
    {
        return movingToTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}