using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sliderHealth;
    public Slider sliderStamina;
    public GameObject panel;
    public GameObject lostPanel;
    public GameObject winPanel;

    public PlayerHealth playerHealth;

    public TextMeshProUGUI enemyCounter;
    public int enemyNum;
    public int enemyCurrentNum;

    public bool settingsIsActive;

    private void Start()
    {
        enemyCounter.SetText("Enemy remaining: " + enemyNum.ToString());
        enemyCurrentNum = enemyNum;
        panel.SetActive(false);
        lostPanel.SetActive(false);
        winPanel.SetActive(false);
        settingsIsActive = false;
    }

    private void Update()
    {
        enemyCounter.SetText("Enemy remaining: " + enemyCurrentNum.ToString());

        if (Input.GetKeyDown(KeyCode.E) && !settingsIsActive)
        {
            OpenSettings();
            settingsIsActive = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && settingsIsActive)
        {
            CloseSettings();
            settingsIsActive = false;
        }

        if (playerHealth.currentHealth <= 0)
        {
            settingsIsActive = true;
            LostGame();
        }

        if (enemyCurrentNum <= 0)
        {
            settingsIsActive = true;
            WonGame();
        }
    }
    public void SetupPlayerHealth(int maxHealth)
    {
        sliderHealth.maxValue = maxHealth;
        sliderHealth.value = maxHealth;
    }

    public void SetupPlayerStamina(float maxStamina)
    {
        sliderStamina.maxValue = maxStamina;
        sliderStamina.value = maxStamina;
    }

    public void UpdatePlayerHealth(int currentHealth)
    {
        sliderHealth.value = currentHealth;
    }

    public void UpdatePlayerStamina(float currentStamina)
    {
        sliderStamina.value = currentStamina;
    }

    public void OpenSettings()
    {
        panel.SetActive(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true;                  // show cursor
    }

    public void CloseSettings()
    {
        panel.SetActive(false);
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Locked; // lock to center
        Cursor.visible = false;                   // hide cursor
    }

    private void LostGame()
    {
        lostPanel.SetActive(true);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true;                  // show cursor
    }

    private void WonGame()
    {
        winPanel.SetActive(true);   
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true;                  // show cursor
    }

}
