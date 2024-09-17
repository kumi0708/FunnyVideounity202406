using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscAppEnd : MonoBehaviour
{
	// ESCキーが押されたかをチェック
	// アプリケーションを終了
	// エディター内で動作させる場合は、以下の行も追加してください



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		// ESCキーが押されたかをチェック
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// アプリケーションを終了
			Application.Quit();

			// エディター内で動作させる場合は、以下の行も追加してください
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}
	}
}
