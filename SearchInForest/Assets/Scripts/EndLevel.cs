using UnityEngine;

public class EndLevel : MonoBehaviour, IInteractable
{
    public InteractionOption InteractionOption { get; set; }
    public void Interact()
    {
        GameManager.Instance.EndLevel();
    } 
}
