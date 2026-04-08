using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HeaderAttribute("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    public UIManager ui;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log(currentHealth);

        ui.SetupPlayerHealth(currentHealth);
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        ui.UpdatePlayerHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}
