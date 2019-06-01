using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {
    new private Camera camera;
    public GameObject entity;
    public Slider slider;

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        slider.maxValue = entity.GetComponent<CharacterScript>().life;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = entity.transform.position;
        transform.LookAt(camera.transform);
	}

	private void Update()
	{
        slider.value = entity.GetComponent<CharacterScript>().life;
	}
}
