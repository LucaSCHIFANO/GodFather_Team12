using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockManagement : MonoBehaviour
{
    [SerializeField] private List<BlockMovement> listPattern = new List<BlockMovement>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBetweenSpawn = 3f;
    [SerializeField] private float timeMultiplicator = 1f;
    private int lastPattern = -1;

    private void Start()
    {
        StartCoroutine(SpawnTest());
    }

    private IEnumerator SpawnTest()
    {
        if(lastPattern == -1) lastPattern = Random.Range(0, listPattern.Count);
        else
        {
            var isOkay = false;
            while (isOkay == false)
            {
                var newPattern = Random.Range(0, listPattern.Count);
                if (lastPattern != newPattern)
                {
                    lastPattern = newPattern;
                    isOkay = true;
                };
            }
            
        }
        
        var spawned = Instantiate(listPattern[lastPattern], spawnPoint.transform);
        spawned.changeSpeed(timeMultiplicator);
        yield return new WaitForSeconds(timeBetweenSpawn);
        StartCoroutine(SpawnTest());
    }
}
