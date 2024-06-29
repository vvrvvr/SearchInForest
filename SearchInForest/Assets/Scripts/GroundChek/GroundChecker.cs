using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Transform start;
    private Rigidbody groundCheckRb;
    private Transform startTrasform; // Точка, из которой выпускается луч
    public GameObject groundCheckObj; // Объект, который нужно переместить
    public float rayDistance = 10f; // Дистанция луча
    public LayerMask groundLayer;
    private float interactionDistance = 2f;


    private void Start()
    {
        groundCheckRb = groundCheckObj.GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckGround();
    }

    void CheckGround()
    {
        Ray ray = new Ray(start.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, groundLayer))
        {
            groundCheckRb.isKinematic = true;
            groundCheckObj.transform.position = hit.point;
        }
        else
        {
            groundCheckRb.isKinematic = false;
        }
    }
    
    public void InteractWithFloor()
    {
        Debug.Log(gameObject.name);
        Ray ray = new Ray(gameObject.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            interactable?.Interact();
        }
    }
}