using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treeHP : MonoBehaviour
{
    [SerializeField]
    private GameObject HPbar;
    [SerializeField]
    private GameObject canvas;

    private float x = 0.05f;
    private float y = 1.5f;
    private Image nowHP;

    private GameObject bar;
    private RectTransform barRect;

    // Start is called before the first frame update
    void Start()
    {
        bar = Instantiate(HPbar, canvas.transform);
        barRect = bar.GetComponent<RectTransform>();
        nowHP = bar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 barPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + x, transform.position.y - y, 0));
        barRect.position = barPos;
    }

    public void DecreaseHP()
    {
        nowHP.fillAmount -= 0.34f;
    }
}
