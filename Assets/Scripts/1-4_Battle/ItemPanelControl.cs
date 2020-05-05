using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPanelControl : MonoBehaviour {

	public GameObject basicPanel;

	public void Item_1()
	{

	}

	public void Item_2()
	{

	}
	public void Item_3()
	{

	}

	public void Item_4()
	{

	}

	public void Item_5()
	{

	}

	public void Back()
	{
		gameObject.SetActive(false);
		basicPanel.SetActive(true);
	}
}
