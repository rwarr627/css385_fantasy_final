using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToggle : MonoBehaviour
{
	public Material normal;
	public Material selected;

	private bool isSelected = false;

	public void SetNormalMat()
	{
		if( isSelected )
		{
			SetMaterial( normal );
			isSelected = !isSelected;
		}
	}

	public void SetSelectedMat()
	{
		if( !isSelected )
		{
			SetMaterial( selected );
			isSelected = !isSelected;
		}
	}

	private void SetMaterial( Material material )
	{
		foreach( Transform child in transform )
		{
			Renderer currRend = child.gameObject.GetComponent< Renderer >();
			currRend.material = material;
		}
	}
}
