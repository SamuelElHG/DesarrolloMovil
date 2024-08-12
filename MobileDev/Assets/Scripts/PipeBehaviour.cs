using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeBehaviour : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private float speed, deathTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

    // Update is called once per frame
    void Update()
    {
        trans.position = trans.position+Vector3.left*speed*Time.deltaTime;
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(deathTime*5);
        Destroy(gameObject);
    }
}
