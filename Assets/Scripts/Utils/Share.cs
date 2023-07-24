using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Share : MonoBehaviour
{
	public ScoreManager scoreManager;
	private string subject;
	private const string body = "https://play.google.com/store/apps/details?id=com.Default.DreamingPet";

	public void Shareshare()
	{
		subject = "내 펫 산책시키고 친구의 스코어 " + scoreManager.score.ToString() + "점 뛰어넘기!";
#if UNITY_ANDROID && !UNITY_EDITOR
		using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent")) 
		using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
			intentObject.Call<AndroidJavaObject>("setAction", intentObject.GetStatic<string>("ACTION_SEND"));
			intentObject.Call<AndroidJavaObject>("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_SUBJECT"), subject);
			intentObject.Call<AndroidJavaObject>("putExtra", intentObject.GetStatic<string>("EXTRA_TEXT"), body);
			using (AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
			using (AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity")) 
			using (AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share Via"))
			currentActivity.Call("startActivity", jChooser);
		}
#endif
	}
}
