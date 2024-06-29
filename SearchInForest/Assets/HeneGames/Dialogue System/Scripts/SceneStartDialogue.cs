using UnityEngine;

public class SceneStartDialogue : MonoBehaviour
{
    [SerializeField] private GameObject startDialogue;
    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     StartDialog();
        // }
    }
    public void StartDialog()
    {
        startDialogue.SetActive(true);
    }

    public void EndDialogue()
    {
        Destroy(gameObject);
    }
}
