using UnityEngine;
using UnityEngine.UI;

public class Layer : MonoBehaviour
{
	[SerializeField]
	private GameObject rawImageObj = null;
	[SerializeField]
	private Transform pearent = null;
	[SerializeField]
	private Compositor compositor = null;

	//タイマー
	private float timer = 0.0f;
	//インターバル
	[SerializeField]
	private float interval = 10.0f;
	[SerializeField]
	private float rotSpeed = 75.0f;

	// Start is called before the first frame update
	void Start()
    {
		timer = 0;
	}

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = interval;
			GameObject obj = Instantiate(rawImageObj, pearent);
			obj.GetComponent<LayerCtrl>().rotSpeed = rotSpeed;
			obj.GetComponent<RawImage>().material = compositor._material;
			//obj.GetComponent<RawImage>().texture = compositor._copiedTexture;
			obj.transform.SetSiblingIndex(0);

		}
	}
}
