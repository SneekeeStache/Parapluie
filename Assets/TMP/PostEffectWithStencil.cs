using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectWithStencil : PostEffectsBase
{
	public Shader Shader;
	private Material Material = null;
	public Material material
	{
		get
		{
			Material = CheckShaderAndCreateMaterial(Shader, Material);
			return Material;
		}
	}

	
	void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (material != null)
		{
			
			Graphics.Blit(src, dest, material);
		}
		else
		{
			Graphics.Blit(src, dest);
		}
	}
}
