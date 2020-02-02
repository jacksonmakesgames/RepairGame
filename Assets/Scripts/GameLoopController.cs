using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    public int missilesPerWave;




    [Range(0, 60000.0f)]
    public float timeToWin;

    [Range(0, 60.0f)]
    public float startDelayTime;

    [Range(0, 10.0f)]
    public float waveLength; // seconds

    [Range(0, 20.0f)]
    public float timeBetweenWaves; // seconds

    float lastMissileLaunchedTime;

    int countThisWave = 0;

    private float awakeTime;
    // Start is called before the first frame update
    void Start()
    {
        awakeTime = Time.time;
        timeBetweenMissiles = waveLength / (float) missilesPerWave;
        lastMissileLaunchedTime = startDelayTime - timeBetweenMissiles;
    }

    float lastWaveEndTime = 0.0f;
    float timeBetweenMissiles;
    // Update is called once per frame
    void Update()
    {
        if (Time.time - awakeTime > timeToWin) {
            Radio.Instance.Ready();
        }

        if (countThisWave >= missilesPerWave)
            NextWave();
        else if(Time.time - awakeTime >= lastMissileLaunchedTime + timeBetweenMissiles)
            LaunchMissile();
}
    void LaunchMissile() {
            countThisWave++;
            lastMissileLaunchedTime = Time.time - awakeTime;
            MissileSpawner.Instance.Spawn();
    }

    void NextWave() {
        if (Time.time - awakeTime >= lastWaveEndTime + timeBetweenWaves)
        {
            countThisWave = 0;
            lastWaveEndTime = Time.time - awakeTime;
            lastMissileLaunchedTime = Time.time - awakeTime + timeBetweenWaves - timeBetweenMissiles;
        }
    }
}
