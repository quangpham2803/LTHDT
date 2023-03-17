using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndExit : MonoBehaviour {
    public int numScene;
    void OnTriggerEnter2D(Collider2D col) {
        SaveSystem.SaveGameData();
        Cursor.visible = true;
        ReturnToMenu();
    }

    public void ReturnToMenu() {
        PlayMenuSound();
        FindObjectOfType<SoundManager>().StopAllSongs();
        ItemsInventory.itemsInInventory.Clear();
        SceneManager.LoadScene(numScene);
    }

    public void PlayMenuSound() {
        SoundManager.PlaySound("MenuSound");
    }
}
