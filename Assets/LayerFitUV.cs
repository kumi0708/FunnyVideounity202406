using UnityEngine;
using UnityEngine.UI;

public class LayerFitUV : MonoBehaviour
{
	[SerializeField]
	private GameObject rawImageObj = null;
	[SerializeField]
	private Transform pearent = null;
	[SerializeField]
	private CompositorFitUV compositor = null;

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
			obj.GetComponent< LayerCtrl>().rotSpeed = rotSpeed;
			if (compositor._special2)
			{
				if (obj.GetComponent<RawImage>().texture == null)
				{
					var tex = Create();
					obj.GetComponent<LayerCtrl>()._copiedTexture = tex;
					obj.GetComponent<RawImage>().texture = tex;
				}
			}
			else
			{
				obj.GetComponent<RawImage>().material = compositor._material;
			}
			obj.transform.SetSiblingIndex(0);
		}
	}

	Texture2D Create()
	{
		// 2024 06 23 

		// レンダーテクスチャを作成
		RenderTexture renderTexture2 = new RenderTexture(compositor._source.Texture.width, compositor._source.Texture.height, 24);
		// _materialの描画結果をレンダーテクスチャにコピー
		Graphics.Blit(null, renderTexture2, compositor._material);

		// 新しいTexture2Dを作成してレンダーテクスチャからピクセルデータを読み込む
		Texture2D _copiedTexture = new Texture2D(compositor._source.Texture.width, compositor._source.Texture.height, TextureFormat.RGBA32, false);

		RenderTexture.active = renderTexture2;
		_copiedTexture.ReadPixels(new Rect(0, 0, renderTexture2.width, renderTexture2.height), 0, 0);
		_copiedTexture.Apply();

		// レンダーテクスチャを解放
		RenderTexture.active = null;
		Destroy(renderTexture2);

		return _copiedTexture;
	}
}
