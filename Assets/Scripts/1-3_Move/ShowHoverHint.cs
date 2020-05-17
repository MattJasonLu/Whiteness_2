using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowHoverHint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject hint;
    bool isShowHint;
    // Start is called before the first frame update
    void Start()
    {
        hint = transform.Find("Desp").gameObject;
        hint.SetActive(false);
        isShowHint = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isShowHint = true;
        hint.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isShowHint = false;
        hint.SetActive(false);
    }
}
