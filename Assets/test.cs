using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//https://qiita.com/generosity_honda/items/2c5b75eec1be26ccf294
public class test : MonoBehaviour
{
	[SerializeField] Shader _shader = null;

	public Material _material;

	[SerializeField] RawImage rawImage;
	// Start is called before the first frame update
	void Start()
    {
		_material = new Material(_shader);
		rawImage.material = _material;
	}
	void OnDestroy()
	{
		Destroy(_material);
		_material = null;
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
