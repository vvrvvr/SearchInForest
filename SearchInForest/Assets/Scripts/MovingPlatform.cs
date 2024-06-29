using UnityEngine;

public class MovingPlatform : MonoBehaviour, IInteractable
{
    public InteractionOption InteractionOption { get; set; }

   public void Interact()
    {
        PlayerController.Instance.currentCenter.transform.SetParent(this.transform);
    }
}
