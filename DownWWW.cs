using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
public class DownWWW : MonoBehaviour {
	public GameObject GoCube;
	public GameObject GoSphere;

	// Use this for initialization
	void Start () {
//		StartCoroutine (downLoadByFile ());
//		StartCoroutine (downLoadByHttp());
//		StartCoroutine (TestPost ());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator downLoadByFile(){
		using (WWW texture = new WWW ("file:///Users/mac/Desktop/lh.png")) {
			yield return texture;
			GoCube.GetComponent<Renderer> ().material.mainTexture = texture.texture;
		}
	}

	IEnumerator downLoadByHttp(){
		WWW data = new WWW ("https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1507885799669&di=e563196018ac60fcd748eb71ab1f4230&imgtype=jpg&src=http%3A%2F%2Fimg1.imgtn.bdimg.com%2Fit%2Fu%3D1136575859%2C3179708991%26fm%3D214%26gp%3D0.jpg");
		yield return data;
		GoSphere.GetComponent<Renderer> ().material.mainTexture = data.texture;
		data.Dispose ();
	}

	//发送数据
	IEnumerator TestPost(){
		string m_Info = null;
		Dictionary<string,string> hash = new Dictionary<string, string> ();
		hash.Add ("Content-Type", "application/json");
		string data="{'email':'123@qq.com','password':'123',"+"'phoneIdentity':'32324'}";
		byte[] bs = System.Text.UTF8Encoding.UTF8.GetBytes (data);

		WWW www = new WWW ("http://123.56.50.222:8050/userLogin", bs, hash);
		yield return www;
		if (www.error != null) {
			m_Info = www.error;
			yield return null;
		}
		m_Info = www.text;
		print (m_Info);
	}


	string GetURL(string mainURL,Dictionary<string,string>dic){
		StringBuilder data = new StringBuilder ();
		data.Append(mainURL).Append("?");
		if (dic.Count != 0) {
			foreach (var item in dic) {
				data.Append (item.Key).Append ("=").Append (item.Value).Append ("&");
			}
			data.Remove (data.Length - 1, 1);
		}
		return data.ToString ();
	}


}
