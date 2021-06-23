using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance == null)
        {
            instance = this;
            if (PlayerPrefs.HasKey("PlayerCurrency"))
            {
                playerCurrency = PlayerPrefs.GetInt("PlayerCurrency");
            }
            UIManager.instance.globalCurrencyText.text = playerCurrency.ToString();
        }
        else Destroy(gameObject);
    }

    [Header("Data")]
    public int playerCurrency = 0;

    public void StartLevel()
    {
        PlayerController.instance.StartGame();
        UIManager.instance.OnGameStarted.Invoke();
    }

    public void LevelFailed()
    {
        PlayerController.instance.canMove = false;
        DOVirtual.DelayedCall(1f, () =>
        {
            UIManager.instance.OnGameLost.Invoke();
            AddCurrency(LevelManager.instance.currencyCount, true);
        });
    }

    public void EndLevel()
    {
        if (LevelManager.instance.finalMultiplier < 1)
        {
            LevelFailed();
        }
        else
        {
            UIManager.instance.OnGameEnded.Invoke();
        }
    }

    public void AddCurrency(int count, bool isDisplayOnly)
    {
        UIManager.instance.OnCurrencyAdded.Invoke(count);
        if (!isDisplayOnly)
        {
            playerCurrency += count;
            PlayerPrefs.SetInt("PlayerCurrency", playerCurrency);
        }
    }
}
