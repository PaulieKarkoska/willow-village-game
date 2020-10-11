using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField]
    private Vector3 direction = Vector3.up;
    [SerializeField]
    private float speed = 100;

    void Update()
    {
        transform.Rotate(direction * Time.deltaTime * speed);
    }
}