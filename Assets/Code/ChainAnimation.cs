using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChainAnimation : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    private Material material;
    private bool isTweening;
    private Color currentColor = Color.white;

    private void Awake()
    {
        material = meshRenderer.material;
    }
    private void OnMouseDown()
    {
        if (isTweening) return;
        Animate();
    }

    private void Animate()
    {
        isTweening = true;
        Sequence sequence = DOTween.Sequence();

        Vector3 targetRotation = transform.eulerAngles + Vector3.up * 360f;
        sequence.Append(Move());
        sequence.Append(ChangeColor());
        sequence.Append(Rotate());
        sequence.Append(Jump());
        sequence.Append(Scale());
        sequence.onComplete += AllowTweening;
    }

    private Tween Move()
    {
        Vector3 targetPosition = transform.position + Vector3.forward;
        return transform.DOMove(targetPosition, 1f);
    }
    
    private Tween Rotate()
    {
        Vector3 targetRotation = transform.eulerAngles + Vector3.up * 360f;
        return transform.DORotate(targetRotation, 1f, RotateMode.FastBeyond360);
    }

    private Tween Jump()
    {
        return transform.DOJump(transform.position + Vector3.left, 1f, 1, 1f);
    }

    private Tween Scale()
    {
        return transform.DOScaleX(2, 1f);
    }
    
    private Tween ChangeColor()
    {
        Color targetColor = currentColor == Color.white ? Color.blue : Color.white;
        currentColor = targetColor;
        return material.DOColor(targetColor, 1f);
    }


    private void AllowTweening()
    {
        isTweening = false;
    }
}
