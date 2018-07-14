using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawScript : MonoBehaviour {

	public GameObject LineRendererObject;
	public GameObject earth;
	GameObject japan;
	GameObject america;

	[Range(0, 1)]
	float t;
	public Transform top;
	LineRenderer line;

	// Use this for initialization
	void Start () {
		japan = earth.transform.FindChild ("Japan").gameObject;
		america = earth.transform.FindChild ("America").gameObject;
		t = 0.1f;
		line = LineRendererObject.GetComponent<LineRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {

		LineRendererObject.transform.position = BezierCurve (
			america.transform.position,
			japan.transform.position,
			top.transform.position,
			t
		);
		OnLineDraw ();
	}

	public static Vector3 BezierCurve(Vector3 pt1, Vector3 pt2, Vector3 ctrlPt, float t) {
		if (t > 1.0f)
			t = 1.0f;

		Vector3 result = new Vector3();
		float cmp = 1.0f - t;
		result.x = cmp * cmp * pt1.x + 2 * cmp * t * ctrlPt.x + t * t * pt2.x;
		result.y = cmp * cmp * pt1.y + 2 * cmp * t * ctrlPt.y + t * t * pt2.y;
		result.z = cmp * cmp * pt1.z + 2 * cmp * t * ctrlPt.z + t * t * pt2.z;
		return result;	
	}

	public void OnLineDraw() {
		var posList = new List<Vector3>();
		posList.Add(america.transform.position);
		float length = 0f;
		while (length < 1f)
		{
			length += 0.1f;
			posList.Add(
				BezierCurve(
					america.transform.position,
					japan.transform.position,
					top.position,
					length
				)
			);
		}
		line.material = new Material (Shader.Find("Particles/Additive"));
		line.SetColors (Color.green, Color.blue);
		line.startWidth = 0.8f;
		line.endWidth = 0.8f;
		line.numPositions = posList.Count;
		line.SetPositions(posList.ToArray());
	}
}
