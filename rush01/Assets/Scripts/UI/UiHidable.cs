using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UiHidable : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvasGroup;
	private bool _isHided = false;

	protected virtual void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();
	}

	public void Hide()
	{
		canvasGroup.alpha = 0f;
		canvasGroup.blocksRaycasts = false;
		_isHided = true;
	}

	public void Show()
	{
		canvasGroup.alpha = 1f;
		canvasGroup.blocksRaycasts = true;
		_isHided = false;
	}

	public bool isHided()
	{
		return _isHided;
	}
	
}
