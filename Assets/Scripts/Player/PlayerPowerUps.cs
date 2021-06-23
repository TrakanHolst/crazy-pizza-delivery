using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerPowerUps : MonoBehaviour
{
    public static PlayerPowerUps instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Setup")]
    public Transform firstPizzaPivot;

    [Header("Data")]
    public int pizzaCount = 0;
    public List<PizzaBehavior> playerPizzas = new List<PizzaBehavior>();

    public void AddPizza(GameObject pizzaObject)
    {
        PizzaBehavior pizza = pizzaObject.GetComponent<PizzaBehavior>();
        if (playerPizzas.Count == 0)
        {
            playerPizzas = new List<PizzaBehavior>();
            pizza.Pick(pizzaCount, firstPizzaPivot, firstPizzaPivot);
        }
        else
        {
            pizza.Pick(pizzaCount, playerPizzas[playerPizzas.Count - 1].transform, firstPizzaPivot);
        }

        playerPizzas.Add(pizza);
        pizzaCount++;

        CheckPizzaCount();
    }

    public void CheckPizzaCount()
    {
        if (playerPizzas.Count > 0)
        {
            PlayerController.instance.playerAnimations.SetBool("hasPizza", true);
        }
        else
        {
            PlayerController.instance.playerAnimations.SetBool("hasPizza", false);
        }
    }

    public void TakeObstacle()
    {
        PlayerController.instance.playerAnimations.SetBool("isRunning", false);
        PlayerController.instance.playerAnimations.SetTrigger("Trip");
        DOVirtual.DelayedCall(0.25f, () =>
        {
            PlayerController.instance.playerAnimations.SetBool("isRunning", true);
        });

        if (playerPizzas.Count < 1) return;
        PizzaBehavior lastPizza = playerPizzas[playerPizzas.Count - 1];
        lastPizza.Drop();
        playerPizzas.Remove(lastPizza);
        CheckPizzaCount();
    }

    public void DropAll()
    {
        foreach (PizzaBehavior pizza in playerPizzas)
        {
            pizza.Drop();
        }

        playerPizzas.Clear();
        CheckPizzaCount();
    }

    public void Deliver(EndBonusBehavior endBonus)
    {
        PizzaBehavior lastPizza = playerPizzas[playerPizzas.Count - 1];
        PlayerController.instance.playerAnimations.SetTrigger("Throw");
        lastPizza.Deliver(endBonus);
        playerPizzas.Remove(lastPizza);
        pizzaCount--;
        CheckPizzaCount();
    }
}
