using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayScaleEffect : MonoBehaviour {

	public Shader m_Shader = null;
	public Transform playerPosition;
	private Material m_Material;
	
	// Use this for initialization
	void Start () {
		m_Material = new Material(m_Shader);
		m_Material.name = "GrayScaleMaterial";
		m_Material.hideFlags = HideFlags.HideAndDontSave;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst) {
		
		//apply shader
		if (m_Shader && m_Material) {
			//m_Material.SetVector("_sphereCenter",playerPosition.position);
			Graphics.Blit(src, dst, m_Material);
		}
	}
	
	void OnDisable()
	{
		if (m_Material)
		{
			DestroyImmediate(m_Material);
		}
	}
}
