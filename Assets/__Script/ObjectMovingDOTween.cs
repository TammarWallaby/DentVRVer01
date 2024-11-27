using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovingDOTween : MonoBehaviour
{
    public Vector3 targetPosition;
    public float moveTime;

    public bool isMove = false;

    Sequence moveSequence;

    private void Start()
    {
        moveSequence = DOTween.Sequence().
            Append(transform.DOMove(targetPosition, moveTime).SetEase(Ease.Linear))
            .OnComplete(() => { Debug.Log("Finish!"); });
        moveSequence.Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMove = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            moveSequence.Pause();
        }

        if (isMove)
        {
            moveSequence.Play();
            isMove = false;
        }
    }
}
