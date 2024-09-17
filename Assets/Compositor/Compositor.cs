using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;
using BodyPix;

public sealed class Compositor : MonoBehaviour
{
    [SerializeField] ImageSource _source = null;
    [SerializeField] Shader _shader = null;
    [SerializeField] Texture2D _background = null;
    [SerializeField] RawImage _previewUI = null;
    [SerializeField] ResourceSet _resources = null;

    BodyDetector _detector;
    public Material _material;

	[SerializeField] RawImage _bgUI = null;

    //[SerializeField]
    public Texture2D _copiedTexture;

	public void Anonymize(bool flag)
      => _material.SetInt("_Anonymize", flag ? 1 : 0);

    public void SetThreshold(float value)
      => _material.SetFloat("_Threshold", value);

    void Start()
    {
        _detector = new BodyDetector(_resources, 320, 240);
        _material = new Material(_shader);
        _previewUI.material = _material;
    }

    void OnDestroy()
    {
        _detector?.Dispose();
        _detector = null;

        Destroy(_material);
        _material = null;
    }

    void Update()
    {
        _detector.ProcessImage(_source.Texture);
        _material.SetTexture("_BgTexture", _background);
        _material.SetTexture("_CameraTexture", _source.Texture);
        _material.SetTexture("_MaskTexture", _detector.MaskTexture);

		//// レンダーテクスチャを作成
		//RenderTexture renderTexture = new RenderTexture(_source.Texture.width, _source.Texture.height, 24);
		//// _materialの描画結果をレンダーテクスチャにコピー
		//Graphics.Blit(null, renderTexture, _material);

		//// 新しいTexture2Dを作成してレンダーテクスチャからピクセルデータを読み込む
		//_copiedTexture = new Texture2D(_source.Texture.width, _source.Texture.height, TextureFormat.RGBA32, false);
		//RenderTexture.active = renderTexture;
		//_copiedTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		//_copiedTexture.Apply();

		//// レンダーテクスチャを解放
		//RenderTexture.active = null;
		//Destroy(renderTexture);

		//// copiedTextureを使用する処理をここに記述
		//// 例: _bgUI.texture = copiedTexture;





		_bgUI.texture = _source.Texture;
	}
}
