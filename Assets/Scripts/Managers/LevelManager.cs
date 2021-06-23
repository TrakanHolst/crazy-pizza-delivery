using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void Awake()
    {
        instance = this;
    }

    [Header("Setup")]
    public List<EndBonusBehavior> finishMultipliers = new List<EndBonusBehavior>();
    public float timeBetweenMultiplierReveals = 0.5f;
    public float timeBetweenResultAndEndScreen = 2.5f;
    public Transform endCameraPosition;

    [Header("Data")]
    public int currencyCount = 0;
    public int finalMultiplier = 0;

    public void FinishLevel()
    {
        PlayerController.instance.canMove = false;
        PlayerController.instance.playerAnimations.SetBool("isRunning", false);
        CameraBehavior.instance.target = null;
        Camera.main.transform.DOMove(endCameraPosition.position, 1.5f);
        Camera.main.transform.DORotate(new Vector3(40, 0, 0), 1.5f).OnComplete(() =>
         {
             for (int i = 0; i < finishMultipliers.Count; i++)
             {

                 DOVirtual.DelayedCall(timeBetweenMultiplierReveals * i, () =>
                 {
                     finishMultipliers[0].CheckIfPlayerHasOrder();
                     finishMultipliers.RemoveAt(0);
                 });
             }
         });
        DOVirtual.DelayedCall((timeBetweenMultiplierReveals * finishMultipliers.Count) + timeBetweenResultAndEndScreen, () =>
        {
            GameManager.instance.EndLevel();
        });
    }

    public void RestartLevel()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
}
