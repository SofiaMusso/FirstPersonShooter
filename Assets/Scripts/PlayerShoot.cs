using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject impactPrefab;
    public float shootDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDistance))
        {
            //Debug.Log(hit.transform.name);

            Vector3 spawnPos = hit.point + (hit.normal * 0.01f);
            Quaternion spawnRot = Quaternion.LookRotation(hit.normal);

            GameObject impact = Instantiate(impactPrefab, spawnPos, spawnRot);
            GameObject hitObject = hit.collider.gameObject;

            Debug.Log(hit.collider.gameObject.name);

            impact.transform.SetParent(hitObject.transform);

            Destroy(impact, 1f);
        }
    }
}
