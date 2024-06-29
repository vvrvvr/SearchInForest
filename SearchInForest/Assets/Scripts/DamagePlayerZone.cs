using Cinemachine;
using UnityEngine;

public class DamagePlayerZone : MonoBehaviour, IInteractable
{
    [SerializeField] private float impulsePower = 1;
    public InteractionOption InteractionOption { get; set; }
    public void Interact()
    {
        GameManager.Instance.ReduceLives(1,impulsePower);
        Debug.Log("Damage zone");
    }
}
