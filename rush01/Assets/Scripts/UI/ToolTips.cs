using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour {

	public static ToolTips Instance { get; private set; }

	public GameObject pannel;
	public Text title;
	public Text tips;
	private Color _titleColorBase;
	

	private void Awake()
	{
		Instance = this;
		_titleColorBase = title.color;
	}

	public void setTitleColor()
	{
		title.color = _titleColorBase;
	}
	
	public void setTitleColor(Color colorTitle)
	{
		title.color = colorTitle;
	}
	
	public void setToolTips(string pTitle, string pTips, Vector3 position)
	{
		pannel.SetActive(true);
		title.text = pTitle;
		tips.text = pTips;
		pannel.transform.position = position + Vector3.left * Screen.width * 0.10f;
	}

	public void unsetToolTips()
	{
		pannel.SetActive(false);
	}
}
