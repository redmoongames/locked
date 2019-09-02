using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {
	Image _image;
	
	void OnEnable()
	{
		transform.SetParent(GameObject.Find("AllBackground").transform);
		if(_image == null) _image = GetComponent<Image>();
		_image.color = new Color(1,1,1,0);
		StartCoroutine(AlphaChannelChange.AlphaChannelChangeIncrease(gameObject, 0.1f, 2, 1));
		transform.position = transform.parent.GetChild(0).position;
	}
	
	public void SetImage(string newImage)
	{
		if(_image == null) _image = GetComponent<Image>();
		Sprite sprite = Resources.Load<Sprite>("Sprites/Backgounds/" + newImage);
		if(sprite) _image.sprite = sprite;
		else Destroy(gameObject);
	}
}