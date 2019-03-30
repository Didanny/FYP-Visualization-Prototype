using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vision;
using System.IO;
using UnityEngine.Networking;
using System.Linq;
using UnityEngine.Events;

public class CustomVisionAnalyser : MonoBehaviour {

    /// <summary>
    /// Unique instance of this class
    /// </summary>
    public static CustomVisionAnalyser Instance;


    /// <summary>
    /// Insert your prediction endpoint here
    /// </summary>
    private string predictionEndpoint = "https://barcode-prototype.herokuapp.com/";

    /// <summary>
    /// Bite array of the image to submit for analysis
    /// </summary>
    [HideInInspector] public byte[] imageBytes;

    NavManager navManager;
    FirebaseManager firebaseManager;

    /// <summary>
    /// Initializes this class
    /// </summary>
    private void Awake()
    {
        // Allows this instance to behave like a singleton
        Instance = this;

        navManager = GameObject.Find("Managers").GetComponent<NavManager>();
        firebaseManager = GameObject.Find("Managers").GetComponent<FirebaseManager>();
    }

    /// <summary>
    /// Call the Computer Vision Service to submit the image.
    /// </summary>
    public IEnumerator AnalyseLastImageCaptured(string imagePath, string operation = "label")
    {
        Debug.Log("Analyzing...");

        WWWForm webForm = new WWWForm();

        using (UnityWebRequest unityWebRequest = UnityWebRequest.Post(predictionEndpoint, webForm))
        {
            // Gets a byte array out of the saved image
            imageBytes = GetImageAsByteArray(imagePath);

            unityWebRequest.SetRequestHeader("Content-Type", "application/octet-stream");

            // The upload handler will help uploading the byte array with the request
            unityWebRequest.uploadHandler = new UploadHandlerRaw(imageBytes);
            unityWebRequest.uploadHandler.contentType = "application/octet-stream";

            // The download handler will help receiving the analysis from Azure
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();

            // Send the request
            yield return unityWebRequest.SendWebRequest();

            string jsonResponse = unityWebRequest.downloadHandler.text;

            Debug.Log("response: " + jsonResponse);

            // Create a texture. Texture size does not matter, since
            // LoadImage will replace with the incoming image size.
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(imageBytes);
            SceneOrganiser.Instance.quadRenderer.material.SetTexture("_MainTex", tex);

            // The response will be in JSON format, therefore it needs to be deserialized
            AnalysisRootObject analysisRootObject = new AnalysisRootObject();
            analysisRootObject = JsonUtility.FromJson<AnalysisRootObject>(jsonResponse);
            QRObject qRObject = (analysisRootObject.predictions.ToList())[0];
            string data = qRObject.tagName;
            data = data.Substring(7);

            switch (operation)
            {
                case "label":
                    SceneOrganiser.Instance.FinaliseLabel(analysisRootObject);
                    break;
                case "get_location":
                    // TODO Validate data, i.e error handling
                    var parameters = data.Split(',');
                    var buid = parameters[0];
                    var floor_number = int.Parse(parameters[1]);
                    StartCoroutine(navManager.LoadPois(buid, floor_number));
                    break;
                case "get_worker":
                    firebaseManager.loadWorker(data);
                    break;
                default:
                    break;
            }
            SceneOrganiser.Instance.FinaliseLabel(analysisRootObject);
        }
    }

    /// <summary>
    /// Returns the contents of the specified image file as a byte array.
    /// </summary>
    static byte[] GetImageAsByteArray(string imageFilePath)
    {
        FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);

        BinaryReader binaryReader = new BinaryReader(fileStream);

        return binaryReader.ReadBytes((int)fileStream.Length);
    }


}
