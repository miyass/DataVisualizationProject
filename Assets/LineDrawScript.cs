using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawScript : MonoBehaviour {

	public GameObject earth;
	GameObject japan;
	GameObject america;

	// Use this for initialization
	void Start () {
		japan = earth.transform.FindChild ("Japan").gameObject;
		america = earth.transform.FindChild ("America").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		LineRenderer line = GameObject.Find("LineRendererObject").GetComponent<LineRenderer> ();
		line.SetPosition(0, japan.transform.position);
		line.SetPosition(1, america.transform.position);

		line.startWidth = 0.1f;
		line.endWidth = 0.1f;

		line.SetColors(Color.blue,Color.red);
	}
}
