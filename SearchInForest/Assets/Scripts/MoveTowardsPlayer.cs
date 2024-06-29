using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 1f;
    public float forceMagnitude = 10f;

    private Transform playerTransform;
    private bool isMoving = false;
    private Rigidbody rb;

    private void Start()
    {
        playerTransform = null;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isMoving && playerTransform != null)
        {
            MoveToPlayer();
        }
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isMoving = true;
            playerTransform = col.gameObject.transform;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isMoving = false;
            playerTransform = null;
        }
    }

    private void MoveToPlayer()
    {
        Vector3 playerPos = playerTransform.position;
        playerPos.y = transform.position.y; // Убираем изменение по оси Y
        Vector3 direction = playerPos - transform.position;
        rb.AddForce(direction.normalized * forceMagnitude);
    }
}