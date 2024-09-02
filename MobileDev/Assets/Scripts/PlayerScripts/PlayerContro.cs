using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerContro : MonoBehaviour
{
    #region [SerializeField]
    [SerializeField, Header("Variables")] private float jumpForce;
    [SerializeField] private float moveSpeed;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D bCollider;

    [SerializeField, Header("Booleans")] private bool MoveLeft, MoveRight, canJump;

    [SerializeField, Header("Projectile")] private GameObject bullet;
    [SerializeField] private float bulletSpeed;
    #endregion

    private void Start()
    {
        canJump=true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpForce));
            Debug.Log("im working");

        }

        if (MoveLeft)
        {
            transform.position = transform.position + new Vector3(-1, 0) * Time.deltaTime * moveSpeed;
        }

        if (MoveRight)
        {
            transform.position = transform.position + new Vector3(1, 0) * Time.deltaTime * moveSpeed;

        }
    }

    public void Attack()
    {

        var FiredBullet = Instantiate(bullet, transform.position+new Vector3(3,0), transform.rotation);
        FiredBullet.GetComponent<Rigidbody2D>().AddForce(new Vector3(1,0)*bulletSpeed, ForceMode2D.Impulse);
        
    }
    #region JumpAndCheck
    public void Jump()
    {
        if (canJump)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            canJump = false;
            Debug.Log("salto");
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colisionó");
        canJump = true;
    }

    #endregion
    #region HorizontalMovement
    public IEnumerator LeftRoutine()
    {
        MoveLeft = true;
        yield return new WaitForSeconds(1);
        MoveLeft = false;

    }

    public void LeftMove()
    {
        StartCoroutine(LeftRoutine());
    }


    public IEnumerator RightRoutine()
    {
        MoveRight = true;
        yield return new WaitForSeconds(1);
        MoveRight = false;

    }

    public void RightMove()
    {
        StartCoroutine(RightRoutine());
    }
    #endregion
}
