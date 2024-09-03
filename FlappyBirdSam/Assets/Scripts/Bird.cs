using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{


    [SerializeField, Range(0, 10)]
    private float speed;
    [SerializeField]
    private Rigidbody2D rigidbody2D;

    [SerializeField] private AudioClip wingClip, deathClip;

    [SerializeField] private AudioSource aSource;
    [SerializeField] private float volume;

    [Header("SpriteRandomizer")]
    [SerializeField, Header("Bird")] private SpriteRenderer birdSpriteRenderer;
    [SerializeField] private Sprite[] BirdSprite;



    [Header("SpriteRandomizer")] //pero con animator
    [SerializeField, Header("Bird")] private Animator birdAnimator;
    [SerializeField] private RuntimeAnimatorController[] BirdControllers;

    private void Awake()
    {
        if (rigidbody2D == null)
            rigidbody2D = GetComponent<Rigidbody2D>();

        int randomIndex = Random.Range(0, BirdControllers.Length);

        RuntimeAnimatorController birdController = BirdControllers[randomIndex];

        // Asigna el sprite aleatorio al SpriteRenderer

        birdAnimator.runtimeAnimatorController = birdController;
    }


    private void Update()
    {
        // Code for Andorid
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            Move();

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            Move();
#endif
    }

    private void OnCollisionEnter2D(Collision2D collision2D) //aqui perdemos
    {
        if (collision2D.collider.CompareTag("Pipe") || collision2D.collider.CompareTag("Ground"))
        {
            Debug.Log(string.Format("Bird :: OnCollisionEnter2D() :: {0}", collision2D.collider.name));
            aSource.PlayOneShot(deathClip, volume);
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("PipeTrigger"))
        {
            Debug.Log(string.Format("Bird :: OnTriggerEnter2D() :: {0}", collider2D.name));

            GameManager.Instance.IncreaseScore();

        }
    }

    private void Move() //here the bird moves
    {
        Debug.Log("Bird :: Move()");
        aSource.PlayOneShot(wingClip, volume);

        rigidbody2D.velocity = Vector2.up * speed;
    }

    public void FreezeeBirdPosition()
    {
        Debug.Log("Bird :: FreezeeBirdPosition()");

        rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;

        rigidbody2D.AddForce(Vector2.zero);
    }

    public void UnfrezeeBirdPosition()
    {
        Debug.Log("Bird :: UnfrezeeBirdPosition()");

        rigidbody2D.constraints = RigidbodyConstraints2D.None;

        rigidbody2D.AddForce(Vector2.zero);
    }
}
