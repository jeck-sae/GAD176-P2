using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugShortcuts : MonoBehaviour
{
    void Update()
    {
        //restart scene [CTRL + SHIFT + R]
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
