using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockFallManager : MonoBehaviour
{
    [SerializeField] private List<BlockMovement> listPattern = new List<BlockMovement>();
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float timeBeforeActivated = 5f;
    [SerializeField] private float timeBetweenSpawn = 5f;
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
        spawned.changeSpeed(new Vector2(0, timeMultiplicator));
        yield return new WaitForSeconds(timeBetweenSpawn);
        StartCoroutine(SpawnTest());
    }
}
