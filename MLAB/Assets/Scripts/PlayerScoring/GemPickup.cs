using System;
using DG.Tweening;
using UnityEngine;


public class GemPickup : MonoBehaviour
{
    [SerializeField] private RectTransform targetPos;
    [SerializeField] private RectTransform gemImage;
    [SerializeField] private int gemPickupScore = 1;

    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            Destroy(gameObject);
            gemImage.gameObject.SetActive(true);

           
            gemImage.DORotate(targetPos.transform.localEulerAngles, 0.75f);
            gemImage.DOMove(targetPos.position, 0.75f).OnComplete(() =>
                {
                      UIManager.instance.SetGemScore(ScoreManager.instance.AddScore(gemPickupScore).ToString());
                  
                    gemImage.gameObject.SetActive(false);
                    gemImage.anchoredPosition = new Vector2(-348f, -374.99f);
                    gemImage.transform.localEulerAngles = Vector3.zero;
                });
        }
    }
}
