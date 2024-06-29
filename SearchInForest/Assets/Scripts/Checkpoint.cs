using UnityEngine;
using UnityEngine;

public class Checkpoint : MonoBehaviour, IInteractable
{
    public InteractionOption InteractionOption { get; set; }
    public void Interact()
    {
        Debug.Log("Set new position");
        GameManager.Instance.SetCheckpoint(transform.position);
    } 
}
