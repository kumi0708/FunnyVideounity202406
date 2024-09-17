using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;
using BodyPix;
using System.Collections;
using UnityEngine.Rendering;
using System.Numerics;
using System.Collections.Generic;

public sealed class CompositorFitUV : MonoBehaviour
{
	public ImageSource _source = null;
	[SerializeField] Shader _shader = null;
	[SerializeField] Texture2D _background = null;
	[SerializeField] RawImage _previewUI = null;
	[SerializeField] ResourceSet _resources = null;

	BodyDetector _detector;
	public Material _material;

	Texture _tmpTexture;

	[SerializeField] RawImage _bgUI = null;
	[SerializeField] List<Texture2D> _BlendTex = new List<Texture2D>();

	[SerializeField] Shader shader_;
	[SerializeField] int blendIndex_ = 0;

	public void Anonymize(bool flag)
	  => _material.SetInt("_Anonymize", flag ? 1 : 0);

	public void SetThreshold(float value)
	  => _material.SetFloat("_Threshold", value);

	public void Special(bool flag)
	=> _special = flag;

	private bool _special = false;

	public bool _special2 = false;


	public void Special2(bool flag)
		=> _special2 = flag;

	public void SetSpecialData(int value)
	=> blendIndex_ = value;

	public void SetRandam(bool flag)
	=> _random = flag;
	private bool _random = true;
	[SerializeField]
	float time = 0.0f;
	[SerializeField]
	float interval = 10.0f;

	[Range(0.0f,1.0f)]
	 public float _BlendFactor = 0.5f;

	public UnityEngine.Vector4 _AddArea = new UnityEngine.Vector4(0.1f, 0.1f, 0.9f, 0.9f);


	void Start()
	{
		_detector = new BodyDetector(_resources, 320, 240);
		_material = new Material(_shader);
		_previewUI.material = _material;
		//StartCoroutine(RenderAndWait());
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
		if (Input.GetKeyDown(KeyCode.Space))
		{
			blendIndex_++;
			if (blendIndex_ >= _BlendTex.Count)
			{
				blendIndex_ = 0;
			}
		}
		if (_random)
		{
			time -= Time.deltaTime;
			if (time < 0)
			{
				blendIndex_ = Random.Range(0, _BlendTex.Count);
				time = interval;
			}
		}
		RenderAndWait();
		if (_tmpTexture != null)
		{
			_detector.ProcessImage(_tmpTexture);
			_material.SetTexture("_BgTexture", _background);
			_material.SetTexture("_CameraTexture", _tmpTexture);
			_material.SetTexture("_MaskTexture", _detector.MaskTexture);

			_bgUI.texture = _tmpTexture;
		}
	}
	Texture2D _previousTexture = null;
	void RenderAndWait()
	{
		// テクスチャのサイズを取得    
		var width = _source.Texture.width;
		var height = _source.Texture.height;
		// レンダーテクスチャを作成します。
		var renderTexture = new RenderTexture(width, height, 24);
		// レンダーテクスチャをアクティブにします。
		RenderTexture.active = renderTexture;

		// 加算を行うマテリアルを作成します。
		//Material material = new Material(Shader.Find("Custom/Blend"));
		Material material = new Material(shader_);
		
		// ブレンドする領域を設定します。
		material.SetVector("_BlendArea", _AddArea);
		material.SetFloat("_BlendFactor", _BlendFactor);
		// 加算するテクスチャを設定します。
		var tex = _BlendTex[blendIndex_];
		material.SetTexture("_BlendTex", _BlendTex[blendIndex_]);

		if (_special && tex!=null)
		{
			// マテリアルを使用してGraphics.Blitを呼び出します。
			Graphics.Blit(_source.Texture, renderTexture, material);
		}
		else
		{
			_material.SetFloat("_Threshold", 0.9f);

			// マテリアルを使用してGraphics.Blitを呼び出します。
			Graphics.Blit(_source.Texture, renderTexture);
		}

		// 前のフレームで作成したテクスチャを破棄します。
		if (_previousTexture != null)
		{
			Destroy(_previousTexture);
		}

		// 新しいテクスチャを作成し、現在アクティブなレンダーテクスチャからピクセルを読み込みます。
		_previousTexture = new Texture2D(width, height);
		_previousTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
		_previousTexture.Apply();

		// レンダーテクスチャを非アクティブにします。
		RenderTexture.active = null;
		// レンダーテクスチャを破棄します。
		Destroy(renderTexture);
		// マテリアルからメインテクスチャを取得
		_tmpTexture = _previousTexture;
		material = null;





	}

}
