using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Setup")]
    public float scrollSpeed = 250;
    public float playerSpeed = 50;
    public Animator playerAnimations;
    public Rigidbody rigidBody;

    [Header("Data")]
    public bool canMove = false;

    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 6f;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(mousePos);
            if (Input.GetMouseButton(0))
            {
                targetPos.x = Mathf.Clamp(targetPos.x, -2.5f, 2.5f);
            }
            else
            {
                targetPos.x = transform.position.x;
            }
            targetPos.y = transform.position.y;
            targetPos.z = transform.position.z + playerSpeed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPos.x, scrollSpeed * Time.deltaTime), transform.position.y, Mathf.Lerp(transform.position.z, targetPos.z, 0.1f));
            playerAnimations.SetBool("isRunning", true);
        }
        else
        {
            playerAnimations.SetBool("isRunning", false);
        }
    }

    public void StartGame()
    {
        canMove = true;
    }
}
