//using UnityEngine;
//using System.Collections;
//using LitJson;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;
//using UnityEngine.UI;


//public class Client : MonoBehaviour {
//	public string url;
//	WWWForm form = new WWWForm ();
//	WWW www;
//	public Text text;
//	public InputField name;
//	public InputField  pw;
//
//	public void Start () {
//		url = "http://192.168.10.85:8050";
//		StartCoroutine (Login(www));
//	}
//
//	// Update is called once per frame
//	void Update () {
//	
//	}
//	IEnumerator Login(WWW www){
//		form.AddField ("username",name.text);
//		form.AddField ("password",pw.text);
//		www = new WWW (url, form);
//		yield return www;
//
//		if (www.error != null) {
//			print ("fail to request..." + www.error);
//		} 
//		else {
//			if (www.isDone) {
//				string ex=@"<span>([\S\s\t]*?)</span>";
//				Match m = Regex.Match (www.data, ex);
//				if (m.Success) {
//					string result = m.Value;
//					result = result.Substring(result.IndexOf(">") + 1, result.LastIndexOf("<") - result.IndexOf(">") - 1).Trim();
//					if (result == "success") {
//						text.text = "登录成功";
//					} else if (result == "empty") {
//						text.text = "用户名或密码为空";
//					} else if (result == "fail") {
//						text.text = "找不到指定用户";
//					} else {
//						text.text="未知错误";
//					}
//				}
//			}
//		}
//
//	}
//
//}

using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Client : MonoBehaviour
{
	WWW www;
	WWWForm form;
	string url;

	string username_label = "username:";
	string username_input = "";

	string password_label = "password:";
	string password_input = "";

	string password_label2 = "password2:";
	string password_input2 = "";

	string email_label = "email:";
	string email_input = "";

	string callback_label = "result:";
	string callback_label2 = "";








	void OnStart()
	{

	}

	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 80, 20), username_label);
		username_input = GUI.TextField(new Rect(80, 0, 100, 20), username_input);

		GUI.Label(new Rect(0, 30, 80, 20), password_label);
		password_input = GUI.TextField(new Rect(80, 30, 100, 20), password_input);

		GUI.Label(new Rect(0, 60, 80, 20), password_label2);
		password_input2 = GUI.TextField(new Rect(80, 60, 100, 20), password_input2);

		GUI.Label(new Rect(0, 90, 80, 20), email_label);
		email_input = GUI.TextField(new Rect(80, 90, 100, 20), email_input);

		GUI.Label(new Rect(0, 160, 80, 20), callback_label);
		callback_label2 = GUI.TextField(new Rect(50, 160, 160, 20), callback_label2);

		if (GUI.Button(new Rect(0, 120, 100, 30), "Login"))
		{
			form = new WWWForm();
			form.AddField("name", username_input);
			form.AddField("password", password_input);
//			string url = "http://192.168.100.98:8084/ddt/UserLogin.jsp";
			url="http://192.168.10.85:8050";
			www = new WWW(url, form);
			StartCoroutine(WaitForRequestUserNameLogin(www));
		}

		if (GUI.Button(new Rect(120, 120, 100, 30), "Register"))
		{

			form = new WWWForm ();
			form.AddField("id", SystemInfo.deviceUniqueIdentifier);
			form.AddField("name", username_input);
			form.AddField("password", password_input);
			form.AddField("retry_password", password_input2);
			form.AddField("email", email_input);
//			url = "http://192.168.100.98:8084/ddt/registerUser.jsp";
			url="http://192.168.10.85:8050";
			www = new WWW(url, form);
			StartCoroutine(WaitForRequestRegister(www));
		}

//		if (GUI.Button(new Rect(240, 120, 100, 30), "non-reg to play"))
//		{
//			form = new WWWForm();
//			form.AddField("id", SystemInfo.deviceUniqueIdentifier);
//			//form.AddField("name", username_input);
//			//form.AddField("password", password_input);
//			//form.AddField("retry_password", password_input2);
//			//form.AddField("email", email_input);
//			url = "http://192.168.100.98:8084/ddt/NonRegPlay.jsp";
//			www = new WWW(url, form);
//			StartCoroutine(WaitForRequestPhoneIdLogin(www));
//		}
//
//		if (GUI.Button(new Rect(200, 0, 130, 20), "Check UserName"))
//		{
//			form = new WWWForm();
//			form.AddField("name", username_input);
//			Debug.Log("username_input...." + username_input);
//			url = "http://192.168.100.98:8084/ddt/CheckUserIsExist.jsp";
//			www = new WWW(url, form);
//			StartCoroutine(WaitForRequestCheck(www));
//		}

//		if (GUI.Button(new Rect(0, 200, 50, 30), "IMEI"))
//		{
//			callback_label2 = SystemInfo.deviceUniqueIdentifier;
//		}

	}

	IEnumerator WaitForRequestUserNameLogin(WWW www)
	{
		yield return www;
		if (www.error != null)
			Debug.Log("fail to request..." + www.error);
		else
		{
			if (www.isDone)
			{
				string ex = @"<span>([\S\s\t]*?)</span>";
				Match m = Regex.Match(www.data, ex);
				if (m.Success)
				{
					string result = m.Value;
					result = result.Substring(result.IndexOf(">") + 1, result.LastIndexOf("<") - result.IndexOf(">") - 1).Trim();
					if (result == "success")
					{
						callback_label2 = "登录成功";
					}
					else if (result == "empty")
					{
						callback_label2 = "用户名或密码为空";
					}
					else if (result == "fail")
					{
						callback_label2 = "找不到指定用户";
					}
					else
					{
						callback_label2 = "未知错误";
					}
				}
			}
		}
	}

	IEnumerator WaitForRequestRegister(WWW www)
	{
		yield return www;
		if (www.error != null)
			Debug.Log("fail to request..." + www.error);
		else
		{
			if (www.isDone)
			{
				string ex = @"<span>([\S\s\t]*?)</span>";
				Match m = Regex.Match(www.data, ex);
				if (m.Success)
				{
					string result = m.Value;
					result = result.Substring(result.IndexOf(">") + 1, result.LastIndexOf("<") - result.IndexOf(">") - 1).Trim();
					if (result == "success")
					{
						callback_label2 = "注册成功";
					}
					else if (result == "empty")
					{
						callback_label2 = "用户名或密码为空";
					}
					else if (result == "equals")
					{
						callback_label2 = "两次输入密码不一致";
					}
					else if (result == "fail")
					{
						callback_label2 = "更新数据库失败";
					}
					else
					{
						callback_label2 = "未知错误";
					}
				}
			}
		}

	}

	/*
	 IEnumerator WaitForRequestCheck(WWW www)
	{
		yield return www;
		if (www.error != null)
			Debug.Log("fail to request..." + www.error);
		else
		{
			if (www.isDone)
			{
				Debug.Log("data-->" + www.data);
				string ex = @"<span>([\S\s\t]*?)</span>";
				Match m = Regex.Match(www.data, ex);
				if (m.Success)
				{
					string result = m.Value;
					result = result.Substring(result.IndexOf(">") + 1, result.LastIndexOf("<") - result.IndexOf(">") - 1).Trim();
					if (result == "empty")
					{
						callback_label2 = "用户名为空";
					}
					else if (result == "nothing")
					{
						callback_label2 = "用户名不存在,可以注册";
					}
					else if (result == "exist")
					{
						callback_label2 = "用户名已存在";
					}
					else
					{
						callback_label2 = "未知错误";
					}
				}
			}
		}
	}

	IEnumerator WaitForRequestPhoneIdLogin(WWW www)
	{
		yield return www;
		if (www.error != null)
			Debug.Log("fail to request..." + www.error);
		else
		{
			if (www.isDone)
			{
				string ex = @"<span>([\S\s\t]*?)</span>";
				Match m = Regex.Match(www.data, ex);
				if (m.Success)
				{
					string result = m.Value;
					result = result.Substring(result.IndexOf(">") + 1, result.LastIndexOf("<") - result.IndexOf(">") - 1).Trim();
					if (result == "ok")
					{
						callback_label2 = "手机ID登录成功";
					}
					else if (result == "error")
					{
						callback_label2 = "手机ID登录成功";
					}
					else
					{
						callback_label2 = "未知错误";
					}
				}
			}
		}
	} */


}