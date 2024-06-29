using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public void LoadScene(string name) => SceneManager.LoadScene(name);
}
