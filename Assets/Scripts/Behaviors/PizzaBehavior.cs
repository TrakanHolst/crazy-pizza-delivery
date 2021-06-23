using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PizzaBehavior : MonoBehaviour
{
    [Header("Setup")]
    public Collider collectableCollider;
    public float distanceBetweenPizzas = 0.05f;
    public float pizzaLerpSpeed = 0.1f;
    public Rigidbody dropRigidBody;
    public Collider dropCollider;

    // Random Custom
    public Sprite[] randomPizzaElement;
    public SpriteRenderer pizzaElement;
    public Renderer pizzaModel;
    public Color[] randomPizzaBoxColors;

    [Header("Data")]
    private Transform previousPizza;
    private Transform mainPivot;
    public int pizzaNumber;

    [Header("Events")]
    public UnityEvent OnPizzaPicked;
    public UnityEvent OnDropPizza;
    public UnityEvent OnPizzaDropped;

    private void Awake()
    {
        pizzaElement.sprite = randomPizzaElement[Random.Range(0, randomPizzaElement.Length)];
        pizzaModel.material = new Material(pizzaModel.material);
        pizzaModel.material.color = randomPizzaBoxColors[Random.Range(0, randomPizzaBoxColors.Length)];
        transform.DORotate(new Vector3(0, 360, 0), 3.5f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void FixedUpdate()
    {
        if (previousPizza != null)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, previousPizza.position.x, pizzaLerpSpeed), previousPizza.position.y + distanceBetweenPizzas, mainPivot.position.z);
        }
    }

    public void Pick(int pizzaCount, Transform follower, Transform pivot)
    {
        transform.DOKill(false);
        OnPizzaPicked.Invoke();
        pizzaNumber = pizzaCount;
        collectableCollider.enabled = false;
        transform.DOJump(pivot.position, 2, 1, 0.15f).AppendCallback(() =>
        {
            transform.position = pivot.position;
            transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            mainPivot = pivot;
            previousPizza = follower;
        });

    }

    public void Drop()
    {
        previousPizza = null;
        transform.DORotate(new Vector3(-720, transform.localEulerAngles.y, transform.localEulerAngles.z), 0.25f);
        OnDropPizza.Invoke();
        transform.DOJump(new Vector3(transform.position.x, -1.3f, transform.position.z), 2, 1, 0.25f).OnComplete(() =>
          {
              OnPizzaDropped.Invoke();
          });
    }

    public void Deliver(EndBonusBehavior bonus)
    {
        previousPizza = null;
        transform.DOJump(bonus.pizzaPivot.transform.position, 2, 1, 0.4f).OnComplete(() =>
        {
            bonus.ReceiveOrder();
        });
    }

}
