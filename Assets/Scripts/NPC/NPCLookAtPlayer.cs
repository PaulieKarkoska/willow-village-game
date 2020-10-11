using Invector.Utils;
using UnityEngine;

public class NPCLookAtPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform defaultLookAtTarget;

    [SerializeField]
    private Transform playerTarget;

    private void Start()
    {
        if (!defaultLookAtTarget)
            Debug.Log("Store Owner needs a default target for the NPCLookAtPlayer script to work");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTarget = collision.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerTarget = null;
            ResetLook();
        }
    }

    private void LateUpdate()
    {
        if (!playerTarget)
        {
            LookAtPlayer();
        }
    }

    private void ResetLook()
    {
        head.LookAt(defaultLookAtTarget);
    }

    private void LookAtPlayer()
    {
        head.LookAt(playerTarget);
    }
}