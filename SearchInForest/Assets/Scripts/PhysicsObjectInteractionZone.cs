using UnityEngine;

public class PhysicsObjectInteractionZone : MonoBehaviour
{
    public GameObject connectedPhysicsObject;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == connectedPhysicsObject)
        {
            Debug.Log("interaction");
            Destroy(connectedPhysicsObject);
        }
    }
    
}
