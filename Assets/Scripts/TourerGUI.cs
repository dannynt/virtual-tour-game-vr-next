using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TourerGUI : MonoBehaviour
{
    [SerializeField] private GameObject map;

    private void Start()
    {
        HideMap();
    }

    public void HideMap()
    {
        map.transform.DOScale(Vector3.zero, 0.15f).SetEase(Ease.InOutQuad);
    }

    public void ShowMap()
    {
        map.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.InOutQuad);
    }
}
