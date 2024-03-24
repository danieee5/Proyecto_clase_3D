using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] itemPrefab;
    public float minTime = 1f;
    public float maxTime = 2f;
    public Vector2 spawnAreaSize = new Vector2(2.5f, 2.5f);
    public float fallSpeed = 11f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine(0));
    }

    IEnumerator SpawnCoroutine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Vector3 spawnPosition = GetRandomSpawnPosition();

            Collider[] colliders = Physics.OverlapSphere(spawnPosition, 0.5f);

            if (colliders.Length == 0)
            {
                Quaternion spawnRotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)); // Rotación aleatoria en 3 ejes
                GameObject newItem = Instantiate(itemPrefab[Random.Range(0, itemPrefab.Length)], spawnPosition, spawnRotation);
                Destroy(newItem, 15f); // Destruir el objeto después de 20 segundos

                Rigidbody newItemRigidbody = newItem.GetComponent<Rigidbody>();
                if (newItemRigidbody != null)
                {
                    newItemRigidbody.velocity = Vector3.down * fallSpeed;
                }
            }

            waitTime = Random.Range(minTime, maxTime);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float randomX = transform.position.x + Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f);
        float randomZ = transform.position.z + Random.Range(-spawnAreaSize.y / 2f, spawnAreaSize.y / 2f);

        // Asegura que las coordenadas generadas estén dentro del área de generación
        randomX = Mathf.Clamp(randomX, transform.position.x - spawnAreaSize.x / 2f, transform.position.x + spawnAreaSize.x / 2f);
        randomZ = Mathf.Clamp(randomZ, transform.position.z - spawnAreaSize.y / 2f, transform.position.z + spawnAreaSize.y / 2f);

        return new Vector3(randomX, transform.position.y, randomZ);
    }
}