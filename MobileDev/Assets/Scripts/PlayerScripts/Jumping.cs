using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float JumpForce;
    [SerializeField] private float fallForce;

    // Start is called before the first frame update
    void Start()
    {
        //rb2d.AddForce(transform.up * -fallForce);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0)) // 0 es para el botón izquierdo del mouse
        {
            rb2d.AddForce(Vector3.up*JumpForce, ForceMode2D.Impulse);
            //player.transform.position = player.transform.position+new Vector3(0, JumpForce * Time.deltaTime, 0);
            Debug.Log("Mouse right button clicked");

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene(0);
    }
}
