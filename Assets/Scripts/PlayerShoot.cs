using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject impactPrefab;
    public float shootDistance;

    [HeaderAttribute("Shot Effects")]
    public AudioSource shot;
    public Animator shooting;
    public GameObject shotExplosion;

    public Transform shootTransform;

    public UIManager ui;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !ui.settingsIsActive)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        shot.Play();
        shooting.SetTrigger("Shoot");

        Instantiate(shotExplosion, shootTransform.position, shootTransform.rotation);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDistance))
        {
            Vector3 spawnPos = hit.point + (hit.normal * 0.01f);
            Quaternion spawnRot = Quaternion.LookRotation(hit.normal);

            GameObject impact = Instantiate(impactPrefab, spawnPos, spawnRot);

            GameObject hitObject = hit.collider.gameObject;

            EnemyController enemy = hitObject.GetComponent<EnemyController>();

            if (enemy != null)
            {
                enemy.EnemyTakeDamage(20);
            }

            impact.transform.SetParent(hitObject.transform);
            Destroy(impact, 1f);
        }
    }
}
