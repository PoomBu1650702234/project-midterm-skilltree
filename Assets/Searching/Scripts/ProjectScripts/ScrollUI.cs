using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUI : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    public void ResetRectTF()
    {
        if(rectTransform != null)
        {
            // Reset position
            rectTransform.localPosition = Vector3.zero;
            rectTransform.anchoredPosition = Vector2.zero;

        }
    }
}
