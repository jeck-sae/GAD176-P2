using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugShortcuts : MonoBehaviour
{
    [SerializeField] GameObject giveItemPrefab;

    void Update()
    {
        // [CTRL + SHIFT + R] Restart Scene 
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
        {
            Debug.Log("Restarting Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // [CTRL + SHIFT + I] Toggle Invulnerability 
        if (Input.GetKeyDown(KeyCode.I) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
        {
            var player = FindObjectOfType<Player>();
            player.isVulnerable = !player.isVulnerable;
            Debug.Log("Invulnerable: " + !player.isVulnerable);
        }

        // [CTRL + SHIFT + E] Give player X item 
        if (Input.GetKeyDown(KeyCode.E) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl))
        {
            PlayerInventory.Instance.PickUpItem(giveItemPrefab.GetComponent<Item>(), 1);
        }
    }
}
