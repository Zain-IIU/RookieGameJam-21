using DG.Tweening;
using UnityEngine;


public class FadeInMiniPlayerEffects : MonoBehaviour
{
    [SerializeField] private float fadeInDuration = 0.5f;
    
    private void OnEnable()
    {
        Vector3 scaleVal = transform.localScale;
        transform.DOScale(scaleVal, fadeInDuration).From(Vector3.zero).SetEase(Ease.InOutBounce);
    }

}
