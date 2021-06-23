using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

public class EndBonusBehavior : MonoBehaviour
{
    [Header("Setup")]
    public TextMeshPro txtMultiplier;
    public ClientBehavior client;
    public int bonusMultiplier;
    public Renderer multiplierModelRenderer;
    [SerializeField] private List<Color> multiplierColors = new List<Color>();
    public Transform pizzaPivot;

    [Header("Events")]
    public UnityEvent OnOrderReceived;

    private void Awake()
    {
        if (bonusMultiplier < 1) Debug.LogError("End game multiplier not set");
        multiplierModelRenderer.material = new Material(multiplierModelRenderer.material);
        multiplierModelRenderer.material.color = multiplierColors[bonusMultiplier];
        txtMultiplier.text = "x" + bonusMultiplier;
    }

    public void ReceiveOrder()
    {
        OnOrderReceived.Invoke();
        client.ReceiveOrder();
        txtMultiplier.DOFade(1, 0.5f);
        txtMultiplier.rectTransform.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }

    public void CheckIfPlayerHasOrder()
    {
        if (PlayerPowerUps.instance.playerPizzas.Count > 0)
        {
            PlayerPowerUps.instance.Deliver(this);
            LevelManager.instance.finalMultiplier++;
        }
        else
        {
            client.Angry();
        }
    }
}
