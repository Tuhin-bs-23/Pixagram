using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Text;
using System.IO;

public class PostCreateManager : MonoBehaviour
{
	public Image postedImage;
	public Button uploadButton;
	public Button shareButton;
	public TMP_InputField captionInput;

	byte[] byteArray;
	Texture2D texture2D;
	// Start is called before the first frame update
	void Start()
    {
		uploadButton.onClick.AddListener(() =>
		{
			TakePicture(512);
			uploadButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
		});
		shareButton.onClick.AddListener(() =>
		{
			//RequestUrl(StringResources.createPost);
			StartCoroutine(SendFile());
		});
		shareButton.interactable = false;
    }

	public void SetImage(Texture2D texture2D)
	{
		Texture2D texture = texture2D;
		if (texture == null) return;
		//byteArray = File.ReadAllBytes(path); texture.EncodeToPNG();
		postedImage.sprite = Sprite.Create(texture,
			new Rect(0.0f, 0.0f, texture.width, texture.height),
			new Vector2(0.5f, 0.5f), 100.0f);
		postedImage.preserveAspect = true;
	}
	void TakePicture(int maxSize)
	{
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create a Texture2D from the captured image
				texture2D = NativeCamera.LoadImageAtPath(path, maxSize);
				if (texture2D == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}
				SetImage(texture2D);
				shareButton.interactable = true;

				//byteArray = File.ReadAllBytes(path);
				//byteArray = texture2D.EncodeToPNG();
				//Destroy(texture2D, 5f);
			}
		}, maxSize);

		Debug.Log("Permission result: " + permission);
	}
	string bodyJsonString;
	IEnumerator RequestUrl(string extendUrl)
	{
		string url = StringResources.baseURL + extendUrl;
		var request = new UnityWebRequest(url, "POST");
		byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		request.SetRequestHeader("Authorization", "Bearer " + AppManager.instance.bearerToken);
		request.SetRequestHeader("Content-Type", "application/json");
		Debug.Log(AppManager.instance.bearerToken);
		print(url);
		print(bodyJsonString);
		yield return request.SendWebRequest();
		if (request.result == UnityWebRequest.Result.ConnectionError)
		{
			string jsonResponse = request.downloadHandler.text;
			Debug.Log(request.error);
			Debug.Log("Failure");
		}
		else
		{
			print(request.downloadHandler.text);
			
		}
	}
	IEnumerator SendFile()
	{
		WWWForm form = new WWWForm();
		//form.AddField("image",)
		//form.AddBinaryData("image", texture2D.EncodeToPNG(), "myImage.png", "image/png");
		form.AddField("location", "post body");
		form.AddField("postbody", "post body");
		

		using (UnityWebRequest www = UnityWebRequest.Post(StringResources.baseURL + StringResources.createPost, form))
		{
			www.SetRequestHeader("Authorization", "Bearer " + AppManager.instance.bearerToken);
			www.SetRequestHeader("Content-Type", "application/json");
			Debug.Log(www.url);
			Debug.Log(form.ToString());
			yield return www.SendWebRequest();

			if (www.result == UnityWebRequest.Result.ConnectionError)
			{
				string jsonResponse = www.downloadHandler.text;
				Debug.Log(www.error);
				Debug.Log("Failure");
			}
			else
			{
				Debug.Log(www.downloadHandler.text);
				Debug.Log("Form upload complete!");
			}
		}
	}

}
