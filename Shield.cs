using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	[SerializeField]Material mat;

	public bool isDecreasing = false;
	public bool isIncreasing = false;

	private float _Dissolve;
	private float _EdgeRange = 0.05f;
	private float speed = 0.5f;

	const string DissolveIntensity = "_DissolveIntensity";
	const string DissolveEdgeRange = "_DissolveEdgeRange";
	const string DissolveTex = "_DissolveTex";

	void Start()
	{
		StartCoroutine(ShieldEffect(0.025f));
	}

	IEnumerator ShieldEffect(float amount)
	{
		while(true)
		{
			mat.SetFloat(DissolveIntensity, _Dissolve);
			mat.SetFloat(DissolveEdgeRange, _EdgeRange);

			if(isDecreasing)
			{
				_Dissolve -= amount * speed;

				if(_Dissolve < 0 && !isIncreasing){
					isDecreasing = false;
					_EdgeRange = 0f;
					mat.SetTextureOffset(DissolveTex, new Vector2(Random.Range(-1f,1f),
						Random.Range(-1f,1f)));
				}
					
			}

			if(isIncreasing)
			{
				_Dissolve += amount * speed;
				_EdgeRange = 0.1f;

				if(_Dissolve > 1 && !isDecreasing)
					isIncreasing = false;
			}
			yield return new WaitForFixedUpdate();
		}
	}

	void Update()
	{
		//Just for testing, needs to be replaced in the future
		if(Input.GetKey(KeyCode.O))
		{
			isDecreasing = true;
			isIncreasing = false;
		}
		if(Input.GetKey(KeyCode.P))
		{
			isDecreasing = false;
			isIncreasing = true;
		}

	}
}
