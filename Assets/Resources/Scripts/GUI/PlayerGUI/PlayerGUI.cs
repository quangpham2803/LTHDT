using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour {
    private bool pauseMenuActive;
    private bool inventoryMenuActive;
    private bool inforMenuActive;
    public GameObject pauseMenuBackground;
    public GameObject inventoryMenu;
    public GameObject pauseMenu;
    public GameObject dialogueMenu;
    public GameObject gameOverMenu;
    public GameObject marineSelection;
    public GameObject inforMenu;

    private Scene activeScene;
    private DialogueManager dialogueManager;
    private PlayerHealth playerHealth;
    public Text txtHealth;
    public Text txtDamage;

    void Start() {
        pauseMenuActive = false;
        inventoryMenuActive = false;
        inforMenuActive = false;
        activeScene = SceneManager.GetActiveScene();

        dialogueManager = FindObjectOfType<DialogueManager>();

        playerHealth = FindObjectOfType<PlayerHealth>();

        //marineSelection.SetActive(true);
        //pauseMenuBackground.SetActive(true);
    }
    
    void Update() {
        bool dialogueCheck = dialogueManager.DialoguePanelIsActive;

        if (Input.GetKeyDown(KeyCode.Escape) && !inventoryMenuActive && !dialogueCheck && !playerHealth.IsKill && !marineSelection.activeSelf)
            ShowPauseMenu();

        if (Input.GetKeyDown(KeyCode.I) && !dialogueCheck && !playerHealth.IsKill && !marineSelection.activeSelf)
            ShowInforMenu();

        if (Input.GetKeyDown(KeyCode.E) && !pauseMenuActive && !dialogueCheck && !playerHealth.IsKill && !marineSelection.activeSelf)
            ShowInventory();

        if (playerHealth.IsKill && Time.timeScale != 0) {
            Time.timeScale = 0;
            pauseMenuBackground.SetActive(true);
            Cursor.visible = true;
            gameOverMenu.SetActive(true);
            FindObjectOfType<SoundManager>().StopAllSongs();
            SoundManager.PlaySound("GameOver");
        }
    }

    public void ShowPauseMenu() {
        PlayMenuSound();
        pauseMenuActive = !pauseMenuActive;
        pauseMenuBackground.SetActive(pauseMenuActive);
        pauseMenu.SetActive(pauseMenuActive);
        Cursor.visible = pauseMenuActive;
        Time.timeScale = pauseMenuActive ? 0 : 1;
    }

    public void ShowInventory() {
        PlayMenuSound();
        
        if (!inventoryMenuActive) {
            inventoryMenuActive = true;
            pauseMenuBackground.SetActive(inventoryMenuActive);
            inventoryMenu.SetActive(inventoryMenuActive);
            Cursor.visible = true;
            Time.timeScale = 0;
        } else {
            inventoryMenuActive = false;
            pauseMenuBackground.SetActive(inventoryMenuActive);
            inventoryMenu.SetActive(inventoryMenuActive);
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void ShowInforMenu()
    {
        PlayMenuSound();

        if (!inforMenuActive)
        {
            inforMenuActive = true;
            pauseMenuBackground.SetActive(inforMenuActive);
            inforMenu.SetActive(inforMenuActive);
            txtHealth.text = PlayerHealth.instance.health.ToString();
          if(GameObject.FindGameObjectWithTag("Player").GetComponent<GunsInventory>().GetActiveWeapon().name == "Gun4(Clone)")
            {
                txtDamage.text = "5";
                
            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<GunsInventory>().GetActiveWeapon().name == "Gun1(Clone)")
            {
                txtDamage.text = "3";

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<GunsInventory>().GetActiveWeapon().name == "Gun2(Clone)")
            {
                txtDamage.text = "8";

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<GunsInventory>().GetActiveWeapon().name == "Gun3(Clone)")
            {
                txtDamage.text = "5";

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<GunsInventory>().GetActiveWeapon().name == "Gun5(Clone)")
            {
                txtDamage.text = "8";

            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<GunsInventory>().GetActiveWeapon().name == "Gun6(Clone)")
            {
                txtDamage.text = "15";

            }
            Cursor.visible = true;
            Time.timeScale = 0;
            Debug.Log("Bat");

        }
        else
        {
            inforMenuActive = false;
            pauseMenuBackground.SetActive(inforMenuActive);
            inforMenu.SetActive(inforMenuActive);
            Cursor.visible = false;
            Time.timeScale = 1;
            Debug.Log("Tat");
        }
    }

    public void Restart() {
        PlayMenuSound();
        FindObjectOfType<SoundManager>().StopAllSongs();
        ItemsInventory.itemsInInventory.Clear();
        Destroy(GameObject.Find("---------------- Player (1)"));
        SceneManager.LoadScene(activeScene.name);
    }

    public void Continue() {
        ShowPauseMenu();
    }

    public void ReturnToMenu() {
        PlayMenuSound();
        FindObjectOfType<SoundManager>().StopAllSongs();
        ItemsInventory.itemsInInventory.Clear();
        Destroy(GameObject.Find("---------------- Player (1)"));
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayMenuSound() {
        SoundManager.PlaySound("MenuSound");
    }

    public GameObject DialoguePanel {
        get { return dialogueMenu; }
    }
}
