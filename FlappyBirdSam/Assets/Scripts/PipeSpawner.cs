using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("SpriteRandomizer")]
    [SerializeField, Header("BackGround")] private SpriteRenderer bgSpriteRenderer;
    [SerializeField] private Sprite[] BGsprites;

    [SerializeField] private GameObject[] pipe;

    int randomIndex;


    [Space, SerializeField] private float timeToSpawnFirstPipe;
    [SerializeField] private float timeToSpawnPipe;

    [Space, SerializeField] private Transform pipeSpawnPosition;

    [Space, SerializeField] private Transform pipeMinSpawnHeight;
    [SerializeField] private Transform pipeMaxSpawnHeight;

    private void Start()
    {
        randomIndex = Random.Range(0, 2);
        Sprite randomSprite = BGsprites[randomIndex];

        // Asigna el sprite aleatorio al SpriteRenderer
        bgSpriteRenderer.sprite = randomSprite;

        StartCoroutine(SpawnPipes());
    }

    private void SpawnPipe()
    {
        Debug.Log("PipeSpawner :: SpawnPipe()");

        Instantiate(pipe[randomIndex], GetPipePosition(), Quaternion.identity);
    }

    private Vector3 GetPipePosition()
    {
        return new Vector3(pipeSpawnPosition.position.x, GetPipeHeight(), pipeSpawnPosition.position.z);
    }

    private float GetPipeHeight()
    {
        return Random.Range(pipeMinSpawnHeight.position.y, pipeMaxSpawnHeight.position.y);
    }

    private IEnumerator SpawnPipes()
    {
        yield return new WaitForSeconds(timeToSpawnFirstPipe);

        SpawnPipe();

        WaitForSeconds timToSpawnPipeWaitForSeconds = new WaitForSeconds(timeToSpawnPipe);

        do
        {
            yield return timToSpawnPipeWaitForSeconds;

            SpawnPipe();
        } while (!GameManager.Instance.isGameOver);
    }
}
