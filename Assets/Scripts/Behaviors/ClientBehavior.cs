using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBehavior : MonoBehaviour
{
    [Header("Setup")]
    public Animator clientAnimation;
    public Renderer clientDisplay;
    public string[] successAnimationTriggers;
    public string[] failAnimationTriggers;

    public void Awake()
    {
        clientDisplay.material = new Material(clientDisplay.material);
        clientDisplay.material.color = Random.ColorHSV();
    }

    public void ReceiveOrder()
    {
        clientAnimation.SetTrigger(successAnimationTriggers[Random.Range(0, successAnimationTriggers.Length)]);
    }

    public void Angry()
    {
        clientAnimation.SetTrigger(failAnimationTriggers[Random.Range(0, failAnimationTriggers.Length)]);
    }

}
