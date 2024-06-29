using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour, IInteractable
{
   public InteractionOption InteractionOption { get; set; }
   
   // [SerializeField]
   // private InteractionOption interactionOption = InteractionOption.None;
   //
   // public InteractionOption InteractionOption
   // {
   //    get { return interactionOption; }
   //    set { interactionOption = value; }
   // }
   
   public void Interact()
   {
      GameManager.Instance.Death();
      if(InteractionOption == InteractionOption.None)
         Debug.Log("Selected Interaction Option: " + InteractionOption);
   }
}
