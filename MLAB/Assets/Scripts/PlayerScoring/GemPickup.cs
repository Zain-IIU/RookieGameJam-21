using System;
using DG.Tweening;
using UnityEngine;


public class GemPickup : MonoBehaviour
{
    [SerializeField] private RectTransform targetPos;
    [SerializeField] private RectTransform gemImage;
    [SerializeField] private Ease gemEaseMove;
    
    [SerializeField] private int gemPickupScore = 1;
    
    private BoxCollider boxCollider;
    private Camera mainCam;
    
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        mainCam = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            boxCollider.enabled = false;
            Destroy(gameObject);

            RectTransform gem = Instantiate(gemImage, mainCam.WorldToScreenPoint(other.transform.position + new Vector3(0, 1f, 0)), Quaternion.identity,
                targetPos);
            
            gem.DOMove(targetPos.position, 0.75f).SetEase(gemEaseMove).OnComplete(() =>
                {
                      UIManager.instance.SetGemScoreText(ScoreManager.instance.AddScore(gemPickupScore).ToString());
                      Destroy(gem.gameObject);
                });
        }
    }
}
