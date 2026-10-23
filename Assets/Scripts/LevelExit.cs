using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private const int KeyItemId = 3;

    [SerializeField]
    private string nextSceneName = "Level2Scene";
    private bool isLoading;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || isLoading) return;

        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();
        if (playerInventory == null || !playerInventory.HasItem(KeyItemId, 1)) return;

        isLoading = true;
        Time.timeScale = 1f;
        playerInventory.RemoveItem(3,1);
        SceneManager.LoadScene(nextSceneName);
    }
}
