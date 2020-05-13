using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapPanelControl : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public RawImage img;//获取图片，因为我们要获取他的RectTransform
    Vector3 offsetPos; //存储按下鼠标时的图片-鼠标位置差
    void Start()
    {

    }

    public void OnClose()
    {
        GameManager._instance.OnResume();
        gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //将鼠标的位置坐标进行钳制，然后加上位置差再赋值给图片position
        img.rectTransform.position = new Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height), 0) + offsetPos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offsetPos = img.rectTransform.position - Input.mousePosition;
    }
}
