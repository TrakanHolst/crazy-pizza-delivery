using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CurrencyBehavior : MonoBehaviour
{
    [Header("Setup")]
    public Collider currencyCollision;

    public void Pick()
    {
        currencyCollision.enabled = false;
        transform.DOScale(Vector3.zero, 0.20f);
        LevelManager.instance.currencyCount++;
        GameManager.instance.AddCurrency(1, false);
        transform.DOKill(false);
        Destroy(gameObject);
    }
}
