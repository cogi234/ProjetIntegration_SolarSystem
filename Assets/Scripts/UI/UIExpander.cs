using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExpander : MonoBehaviour
{
    [SerializeField] float expandedHeight;
    float collapsedHeight = 35;
    bool collapsed = true;

    [SerializeField] Button button;
    [SerializeField] Sprite collapsedSprite;
    [SerializeField] Sprite expandedSprite;

    private void OnEnable()
    {
        Collapse();
    }

    public void Expand()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, expandedHeight);
        collapsed = false;
        button.image.sprite = expandedSprite;
    }

    public void Collapse()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, collapsedHeight);
        collapsed = true;
        button.image.sprite = collapsedSprite;
    }

    public void Toggle()
    {
        if (collapsed)
            Expand();
        else
            Collapse();
    }
}
