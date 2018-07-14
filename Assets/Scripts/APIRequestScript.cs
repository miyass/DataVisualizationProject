using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class APIRequestScript : MonoBehaviour {

	private string API_KEY = "Ew8FnGHoOEvZTZD05u6Ye8n0N8xFvzOd4uYXE7Js";
	private string parameter = "year=2015&prefCode=1&countryCode=304&purpose=1";
	private string url = "https://opendata.resas-portal.go.jp/api/v1/tourism/foreigners/forTo?";

	private int totalVisitor;

	//jsonのキーとclass変数の名前は一致させる
	[System.Serializable]
	public class Response
	{
		public string message;
		public Result result;
	}

	[System.Serializable]
	public class Result
	{
		public int year;
		public string countryName;
		public string prefName;
		public Changes[] changes;

	}

	[System.Serializable]
	public class Changes
	{
		public string prefName;
		public string rank;
		public Data[] data;
	}

	[System.Serializable]
	public class Data 
	{
		public int year;
		public int quarter;
		public int value;
	}


	// Use this for initialization
	void Start () {
		StartCoroutine(GetInfo());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator GetInfo() {
		UnityWebRequest request = UnityWebRequest.Get (url + parameter);
		request.SetRequestHeader("Content-Type", "application/json");
		request.SetRequestHeader("Accepted", "application/json");
		request.SetRequestHeader("X-API-KEY", API_KEY);

		yield return request.Send();

		if (request.isError) {
			Debug.Log(request.error);
		} else {
			string jsonText = request.downloadHandler.text;
			Debug.Log(jsonText);
			Response res = JsonUtility.FromJson<Response> (jsonText);
			//前年度の部分まで取得される 0~3:前年度1~4quater 4~7:取得年度1~4quater
			for (int i = 4; i <= 7; i++){
				int visitor = res.result.changes [0].data[i].value;
				totalVisitor += visitor;
			}
			Debug.Log (totalVisitor);
		}
	}
}
