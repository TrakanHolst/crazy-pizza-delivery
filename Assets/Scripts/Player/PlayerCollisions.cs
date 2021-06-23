using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Pizza":
                PlayerPowerUps.instance.AddPizza(other.gameObject);
                break;

            case "Obstacle":
                other.enabled = false;
                PlayerPowerUps.instance.TakeObstacle();
                break;

            case "Currency":
                other.gameObject.GetComponent<CurrencyBehavior>().Pick();
                break;

            case "FinishLevel":
                LevelManager.instance.FinishLevel();
                break;

            case "Fail":
                PlayerPowerUps.instance.DropAll();
                PlayerController.instance.playerAnimations.SetTrigger("Stumble");
                GameManager.instance.LevelFailed();
                break;
        }
    }
}
