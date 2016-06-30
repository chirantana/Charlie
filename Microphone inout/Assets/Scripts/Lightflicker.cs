using UnityEngine;
using System.Collections;

public class Lightflicker : MonoBehaviour {

	
 	public float flickerSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		light.range = Mathf.Lerp(light.range, Random.Range (2, 4),
		flickerSpeed * Random.Range (1,3) * Time.deltaTime );
	}
}
