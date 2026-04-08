using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    public Slider slider;
    public Transform target;

    public float speed = 5f;
    private Vector3 offset = new Vector3(0, 2f, 0);

    public void Setup(int maxHealth)
    {
        slider.maxValue = maxHealth;  
        slider.value = maxHealth;
    }

    public void UpdateHealth(int currentHealth)
    {
        slider.value = currentHealth;
    }

    void LateUpdate()
    {
        transform.position = target.position + offset;
        transform.forward = Camera.main.transform.forward;
    }
}
