using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Setup")]
    public Canvas mainCanvas;
    public TextMeshProUGUI globalCurrencyText;
    public TextMeshProUGUI finalMultiplierText;
    public TextMeshProUGUI finalCurrencyText;
    public Image[] totalPizzasRenderers;
    public GameObject currencyEarnedPrefab;
    public RectTransform globalCurrencyPivot;
    public CanvasGroup tutorialGroup;

    [Header("Data")]
    private Sequence addedCurrencyScaleSequence;

    [Header("Events")]
    public UnityEvent OnGameEnded;
    public UnityEvent OnGameStarted;
    public UnityEvent OnGameLost;
    public UnityEvent OnCurrencyMovement;
    public IntEvent OnCurrencyAdded;

    public void AddCurrency(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject currencyEarned = Instantiate(currencyEarnedPrefab, mainCanvas.transform);
            //currencyEarned.transform.SetSiblingIndex(0);
            DOVirtual.DelayedCall(i * 0.1f, () =>
            {
                OnCurrencyMovement.Invoke();
            });
            currencyEarned.transform.DOMove(globalCurrencyPivot.transform.position, 0.5f).SetDelay(i * 0.1f).OnComplete(() =>
            {

                globalCurrencyText.text = GameManager.instance.playerCurrency.ToString();
                globalCurrencyText.rectTransform.DOKill(false);
                globalCurrencyText.rectTransform.localScale = new Vector3(1, 1, 1);
                globalCurrencyText.rectTransform.DOShakeScale(0.25f, 1.5f, 15, 25);
                currencyEarned.transform.DOKill(false);
                Destroy(currencyEarned);
            });
        }
    }

    public void DisplayFinalScore()
    {
        int currentAtScore = 0;
        for (int i = 0; i < LevelManager.instance.finalMultiplier; i++)
        {
            DOVirtual.DelayedCall(i * 0.75f, () =>
             {
                 totalPizzasRenderers[currentAtScore].enabled = true;
                 totalPizzasRenderers[currentAtScore].rectTransform.DOPunchScale(new Vector3(1.3f, 0.7f, 1.2f), 0.20f, 3, 5);
                 finalCurrencyText.rectTransform.DOPunchScale(new Vector3(1.2f, 1.2f, 1.2f), 0.25f, 10, 3);
                 finalCurrencyText.text = "+ " + (LevelManager.instance.currencyCount * (currentAtScore + 1)).ToString() + " <sprite index=12>";

                 finalMultiplierText.rectTransform.DOPunchScale(new Vector3(1.2f + (currentAtScore * 1.1f), 1.2f + (currentAtScore * 1.1f), 1.2f), 0.25f, 10, 3);
                 finalMultiplierText.text = "x" + (currentAtScore + 1);
                 currentAtScore++;
             });
        }
        DOVirtual.DelayedCall(LevelManager.instance.finalMultiplier * 0.75f, () =>
        {
            GameManager.instance.AddCurrency((LevelManager.instance.finalMultiplier * LevelManager.instance.currencyCount)/* - LevelManager.instance.currencyCount*/, false);
        });
    }

    public void HideTutorial()
    {
        tutorialGroup.DOFade(0, 0.5f).SetEase(Ease.Linear);
    }

}
