using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    [SerializeField]
    private GameObject _respawnPoint;

    private bool _isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isTriggered)
        {
            _isTriggered = true;

            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            StartCoroutine(ResetTrigger());
        }
        other.transform.position = _respawnPoint.transform.position;
        CharacterController cc = other.GetComponent<CharacterController>();

        if(cc != null)
        {
            cc.enabled = false;
        }

        if(cc != null)
        {
            cc.enabled = true;
        }

        StartCoroutine(CCEnableRoutine(cc));
    }

    private IEnumerator ResetTrigger()
    {
        // Wait for a short duration before resetting the trigger
        yield return new WaitForSeconds(0.2f);
        _isTriggered = false;
    }

    IEnumerator CCEnableRoutine(CharacterController controller)
    {
        yield return new WaitForSeconds(0.5f);
        controller.enabled = true;
    }
}

