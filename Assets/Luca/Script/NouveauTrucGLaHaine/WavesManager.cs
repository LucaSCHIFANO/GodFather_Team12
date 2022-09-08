using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WavesManager : MonoBehaviour
{
    [Header("GD touchez pas a ca !")] 
    [SerializeField] private Transform spawnRight;
    [SerializeField] private Transform spawnUp;
    [SerializeField] private Transform spawnDown;
    private Waves currentWaves;
    private int wavesCount;
    
    private int phaseCount;
    private bool isRandom;
    private int lastPattern = 2;
    
    [Header("Pour vous les GD <3")]
    [SerializeField] private float timeBeforeBegin;
    [SerializeField] private List<Waves> listWavesPhase1 = new List<Waves>();
    [SerializeField] private List<Waves> listWavesPhase2 = new List<Waves>();
    [SerializeField] private List<Waves> listWavesPhase3 = new List<Waves>();

    private void Start()
    {
        StartCoroutine(StartingPoint());
    }


    IEnumerator StartingPoint()
    {
        yield return new WaitForSeconds(timeBeforeBegin);
        SpawnWaves();
    }


    void SpawnWaves()
    {
        List<Waves> actualPhase = null;
        
        switch (phaseCount)
        {
            case 0 :
                actualPhase = listWavesPhase1;
                break;
            
            case 1 :
                actualPhase = listWavesPhase2;
                break;
            
            case 2 :
                actualPhase = listWavesPhase3;
                break;
        }
        Debug.Log(phaseCount + " " + wavesCount);
        
        
        if(wavesCount < actualPhase.Count) currentWaves = actualPhase[wavesCount];
        else
        {
            StartCoroutine(EndWave());
            return;
        }
        wavesCount++;
        
        Transform currentSpawn = null;
        
        switch (currentWaves.whereIsTheSpawn)
        {
            case Waves.position.RIGHT :
                currentSpawn = spawnRight;
                break;
            
            case Waves.position.UP :
                currentSpawn = spawnUp;
                break;
            
            case Waves.position.DOWN :
                currentSpawn = spawnDown;
                break;
        }
        
        var leInt = Random.Range(0, currentWaves.listPattern.Count);
        var spawned = Instantiate(currentWaves.listPattern[leInt], currentSpawn);
        
        switch (currentWaves.whereIsTheSpawn)
        {
            case Waves.position.RIGHT :
                spawned.changeSpeed(new Vector2(1, 0));
                break;
            
            case Waves.position.UP :
                spawned.changeSpeed(new Vector2(0, 1));
                break;
            
            case Waves.position.DOWN :
                //currentSpawn = spawnDown;
                break;
        }
        
        

        StartCoroutine(LaunchNextWaves());
    }

    IEnumerator LaunchNextWaves()
    {
        yield return new WaitForSeconds(currentWaves.timeBeforeNextSpawn);
        SpawnWaves();
    }

    

    IEnumerator EndWave()
    {
        yield return new WaitForSeconds(currentWaves.timeBeforeNextSpawn);
        if (!isRandom)
        {
            phaseCount++;
            if (phaseCount == 2) isRandom = true;
            wavesCount = 0;
        }
        else
        {
            bool isOkay = false;
            
            while (!isOkay)
            {
                var leInt = Random.Range(0, 3);
                if (leInt != lastPattern)
                {
                    lastPattern = leInt;
                    phaseCount = leInt;
                    wavesCount = 0;
                    isOkay = true;
                }
            }
        }
        SpawnWaves();
    }
}

[System.Serializable]
public class Waves
{
    public List<BlockMovement> listPattern = new List<BlockMovement>();
    public position whereIsTheSpawn;
    public float timeBeforeNextSpawn;
    
    
    public enum position
    {
        RIGHT,
        UP,
        DOWN,
    }
}