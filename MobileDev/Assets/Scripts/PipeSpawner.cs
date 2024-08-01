using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject pipe;
    [SerializeField] private float timeToSpawnFirstPipe;
    [Space, SerializeField] private float spawnTimePipe;
    [SerializeField] private Transform spawnPoint;

    private WaitForSeconds waitFirstPipe;
    private WaitForSeconds waitPipe;


    // Singleton;
    public static PipeSpawner Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else { Instance = this; }
    }
    private void Start()
    {
        StartCoroutine(startSpawningPipe());
    }
    private void instantiatePipe()
    {
        Instantiate(pipe, spawnPoint.position, Quaternion.identity);
    }
    private IEnumerator startSpawningPipe()
    {
        waitFirstPipe = new WaitForSeconds(timeToSpawnFirstPipe);
        waitPipe = new WaitForSeconds(spawnTimePipe);

        yield return waitFirstPipe;
        instantiatePipe();
        do
        {
            yield return waitPipe;
            instantiatePipe();
        } while (true);
    }

}
