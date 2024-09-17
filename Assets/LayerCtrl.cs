using UnityEngine;

public class LayerCtrl : MonoBehaviour
{
    public float rotSpeed = 100.0f;
	[SerializeField]
    private float@destroyPos = 180.0f;


	public Texture2D _copiedTexture;
	// Update is called once per frame
	void Update()
    {
        // x²‚ğ‰ñ“]‚µ‚Ä‚¢‚­
        this.gameObject.transform.Rotate(-Time.deltaTime* rotSpeed, 0.0f, 0.0f);

        if (this.gameObject.transform.localRotation.eulerAngles.x < destroyPos)
        {
			// ˆÈ‘O‚ÌcopiedTexture‚ª‘¶İ‚·‚éê‡‚Í”jŠü
			if (_copiedTexture != null)
			{
				Destroy(_copiedTexture);
			}

			Destroy(this.gameObject);
        }
    }
}
