using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public static MissileSpawner Instance;

    public bool spawn = true;

    [SerializeField]
    GameObject launchPrefab;

    [SerializeField]
    GameObject misslePrefab;

     [SerializeField]
    GameObject alertPrefab;

    [SerializeField]
    float spawnRatePerSecond;

    [SerializeField]
    float spawnAngleArc;

    [SerializeField]
    float spawnRadius;

    float lastSpawnTime = 0.0f;
    // Start is called before the first frame update
    void Awake()
    {
        if (MissileSpawner.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Spawn() {
        if (!spawn) return;
        StartCoroutine(SpawnIter());
    }

    IEnumerator SpawnIter() {
        float angle = Random.Range(-spawnAngleArc / 2.0f, spawnAngleArc / 2.0f);

        float x = Mathf.Sin(Mathf.Deg2Rad * angle) * spawnRadius;
        float y = Mathf.Cos(Mathf.Deg2Rad * angle) * spawnRadius;
        Vector3 spawnLocation = transform.position + new Vector3(x, y, 0);

        Vector3 vectorToTarget = transform.position - spawnLocation;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;
        Quaternion rot = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);

        x = Mathf.Sin(Mathf.Deg2Rad * angle) * spawnRadius / 2.8f;
        y = Mathf.Cos(Mathf.Deg2Rad * angle) * spawnRadius / 2.8f;
        Vector3 alertLocation = transform.position + new Vector3(x, y, 0);

        x = Mathf.Sin(Mathf.Deg2Rad * angle) * spawnRadius / 10.0f;
        y = Mathf.Cos(Mathf.Deg2Rad * angle) * spawnRadius / 10.0f;
        Vector3 launchLocation = transform.position + new Vector3(x, y, 0);
        Quaternion launchRot = Quaternion.LookRotation(forward: Vector3.forward, upwards: -rotatedVectorToTarget);

        GameObject.Instantiate(launchPrefab, launchLocation, launchRot, transform);

        yield return new WaitForSeconds(.1f);

        GameObject.Instantiate(alertPrefab, alertLocation, rot, transform);

        yield return new WaitForSeconds(.2f);


        GameObject.Instantiate(misslePrefab, spawnLocation, rot, transform);

      
    }

    [SerializeField]
    bool showDebug = false;
    private void OnDrawGizmos()
    {
        if (!showDebug) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

        float angle = -spawnAngleArc/2.0f;
        for (int i = 0; i <= 100; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * spawnRadius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * spawnRadius;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(x,y,0));

            angle += ((spawnAngleArc) / 100);
        }
            
    }
}
