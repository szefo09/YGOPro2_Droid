using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

public class JSONObject { 
	public string Stringify()
	{
		return JsonUtility.ToJson(this);
	}
}

public class LoginUserObject : JSONObject {
	public int id;
	public string username;
	public string name;
	public string email;
	public string password_hash;
	public bool active;
	public bool admin;
	public string avatar;
	public string locale;
	public string registration_ip_address;
	public string ip_address;
	public string created_at;
	public string updated_at;
	public string token;
}
public class LoginObject : JSONObject {
	public LoginUserObject user;
	public string token;
	public string message;
}

public class LoginRequest : JSONObject {
	public string account;
	public string password;
	public LoginRequest(string user, string pass) {
		this.account = user;
		this.password = pass;
	}
}

public class MatchObject : JSONObject {
	public string address;
	public int port;
	public string password;
}

public class MyCardHelper : MonoBehaviour {
	string username = null;
	int userid = -1;
	public bool login(string name, string password, out string fail_reason) {
		try { 
			LoginRequest data = new LoginRequest(name, password);
			string data_str = data.Stringify();
			Dictionary<String, String> header_list = new Dictionary<String, String>();
			header_list.Add("Content-Type", "application/json");
			byte[] data_bytes = Encoding.UTF8.GetBytes(data_str);
			WWW www = new WWW("https://api.moecube.com/accounts/signin", data_bytes, header_list);
			while (!www.isDone) { 
				if (Application.internetReachability == NetworkReachability.NotReachable || !string.IsNullOrEmpty(www.error))
				{
					fail_reason = www.error;
					return false;
				}
			}
			string result = www.text;
			LoginObject result_object = JsonUtility.FromJson<LoginObject>(result);
			username = result_object.user.username;
			userid = result_object.user.id;
		} catch (Exception e) {
			fail_reason = e.Message;
			return false;
		}
		return true;
	}

	public string requestMatch(string match_type, out string fail_reason) {
		string ret;
		if (username == null || userid < 0) {
			fail_reason = "Not logged in";
			return null;
		}
		try {
			string auth_str = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(username + ":" + userid));
			Dictionary<String, String> header_list = new Dictionary<String, String>();
			header_list.Add("Authorization", auth_str);
			header_list.Add("Content-Type", "application/x-www-form-urlencoded");
			byte meta = new byte[1];
			WWW www = new WWW("https://api.mycard.moe/ygopro/match?locale=zh-CN&arena=" + match_type, meta, header_list);
			while (!www.isDone) { 
				if (Application.internetReachability == NetworkReachability.NotReachable || !string.IsNullOrEmpty(www.error))
				{
					fail_reason = www.error;
					return null;
				}
			}
			string result = www.text;
			MatchObject result_object = JsonUtility.FromJson<MatchObject>(result);
			ret = result_object.password;
		} catch (Exception e) {
			fail_reason = e.Message;
			return null;
		}
		fail_reason = null;
		return ret;
	}
}
