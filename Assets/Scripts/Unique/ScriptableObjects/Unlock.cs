using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Unlock : ScriptableObject
{
	[SerializeField]
	private bool _istrue;

	public bool IsUnlocked
	{
		get { return _istrue; }
		set { _istrue = value; }
	}

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
}
