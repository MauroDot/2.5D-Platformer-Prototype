using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] private MeshRenderer _callButton;
    private int _requiredCoins = 8;
    private Elevator _elevator;

    private void Start()
    {
        _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();

        if (_elevator == null)
            Debug.LogError("The Elevator is NULL.");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E) && other.GetComponent<Player>().CoinCoint() >= _requiredCoins)
            {
                _callButton.material.color = Color.green;

                if (_elevator.IsActive())
                {
                    if (_elevator.IsMovingToTarget())
                    {
                        // Already moving towards the target, toggle the elevator off
                        _elevator.DeactivateElevator();
                    }
                    else
                    {
                        // Already moving towards the origin, switch direction and go to the target
                        _elevator.MoveToTarget();
                    }
                }
                else
                {
                    // Toggle the elevator on and move towards the origin
                    _elevator.MoveToOrigin();
                    _elevator.ActivateElevator();
                    _callButton.material.color = Color.red;
                }
            }
        }
    }
}







