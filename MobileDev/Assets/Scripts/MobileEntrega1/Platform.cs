using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Platform : MonoBehaviour
{
    [SerializeField] private float Time;
    [SerializeField] private float distance;
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveX(transform.position.x + distance, Time).SetEase(Ease.InOutSine));
        sequence.Append(transform.DOMoveX(transform.position.x, Time).SetEase(Ease.InOutSine));
        sequence.SetLoops(-1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
