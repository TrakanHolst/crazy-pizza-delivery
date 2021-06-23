using UnityEngine;
using DG.Tweening;

public enum TweenType
{
    none, punchScale, shakeRotate, scaleAppear, loopRotate
}

public class StaticTweeningElement : MonoBehaviour
{
    [Header("Setup")]
    public float pauseTime = 0.5f;
    public float power = 1;
    public float speed = 1;
    public int tweenRepeat = 0;
    public TweenType elementTween;
    public RectTransform element;


    private void Start()
    {
        Sequence tweenSequence = DOTween.Sequence();
        switch (elementTween)
        {
            case TweenType.punchScale:

                tweenSequence.Append(transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f) * power, 0.35f * speed).SetLoops(tweenRepeat));
                tweenSequence.AppendInterval(pauseTime);
                tweenSequence.SetLoops(-1);
                break;

            case TweenType.shakeRotate:

                tweenSequence.Append(element.DOShakeRotation(0.5f * speed, new Vector3(0, 0, 20 * power)).SetLoops(tweenRepeat));
                tweenSequence.Append(element.DOShakeRotation(0.5f * speed, new Vector3(0, 0, -20 * power)).SetLoops(tweenRepeat));
                tweenSequence.AppendInterval(pauseTime);
                tweenSequence.SetLoops(-1);
                break;

            case TweenType.scaleAppear:
                Vector3 originalScale = transform.localScale;
                transform.localScale = Vector3.zero;
                transform.DOScale(originalScale, 0.5f * speed * (Random.Range(0.5f, 1))).SetEase(Ease.OutBack);
                break;

            case TweenType.loopRotate:
                transform.DORotate(new Vector3(0, 0, 360), 3.5f * speed, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
                break;
        }
    }

    public void SlideLeft()
    {
        transform.DOLocalMoveX(-500, 1).SetEase(Ease.InSine).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }


    public void SlideRight()
    {
        transform.DOLocalMoveX(500, 1).SetEase(Ease.InSine).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
