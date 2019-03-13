using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Assets.FYP;
using UnityEngine;
using UnityEngine.Networking;

namespace FYP
{
    public class MainBehaviour : MonoBehaviour {
        // Visualization fields
        [SerializeField] GameObject realPath;
        [SerializeField] GameObject dot;
        [SerializeField] GameObject arrow;

        //Navigation fields
        private AnyplaceService anyplace;
        private IEnumerable<GameObject> poiObjects;
        private IDictionary<string, GameObject> poiMap; 
        private const string BASE_URL = "http://fyp.westeurope.cloudapp.azure.com:9000";

        private IEnumerator LoadPois(string buid, int floor)
        {
            var jsonBody = Encoding.UTF8.GetBytes(string.Format("{{\"buid\":\"{0}\", \"floor_number\": \"{1}\"}}", buid, floor));
            var url = BASE_URL + "/anyplace/mapping/pois/all_floor";
            using (var webRequest = new UnityWebRequest(url))
            {
                webRequest.method = UnityWebRequest.kHttpVerbPOST;
                webRequest.uploadHandler = new UploadHandlerRaw(jsonBody);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                yield return webRequest.SendWebRequest();
                if (webRequest.isNetworkError)
                {
                    Debug.Log("OurError: " + webRequest.error + ", " + webRequest.responseCode);
                }
                else
                {
                    var responseBody = Encoding.UTF8.GetString(Decompress(webRequest.downloadHandler.data));
                    Debug.Log("Received: " + responseBody);

                    AnyplacePoiResponse response = JsonUtility.FromJson<AnyplacePoiResponse>(responseBody);
                    AnyplacePoi[] pois = response.pois;
                    Debug.Log("JSON_STUFF: " + response + ", " + pois.Length + "," + pois);

                    poiObjects = pois.Select(anyplacePoi =>
                    {
                        // TODO Make this a prefab of some sort
                        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        go.name = anyplacePoi.puid;
                        go.GetComponent<Renderer>().material.color = Color.green;

                        go.AddComponent<Poi>().FromAnyplace(anyplacePoi);
                        Debug.Log("POI NAME:" + anyplacePoi.name);
                        return go;
                    });
                    poiMap = poiObjects.ToDictionary<GameObject, string>(poi => poi.name);
                    anyplace.GenerateWorldAnchors(33.88535648375892, 35.483956184344606, poiObjects);
                    drawPath();
                }
            }
       
        }
        public void drawPath()
        {
            //StartCoroutine(LoadPois("building_e80bcc63-c0fb-480a-9eec-e3abf419dd08_1550682078007", 0));
            var puids = new List<string>()
            {
                "poi_5efad985-688c-4d79-81b8-db0442300c12",
                "poi_10a61ae7-ecd3-4779-91bc-153cda01fa60",
                "poi_cfa5d0ab-c8b9-43c1-ba2f-948082383e37",
                "poi_6351e381-98d2-435a-a474-031170e0f716"
            };

            for (int j = 0; j < puids.Count - 1; j++)
            {
                var source = poiMap[puids[j]].transform.transform;
                var destination = poiMap[puids[j + 1]].transform.transform;
                float dist = Vector3.Distance(source.localPosition, destination.localPosition);
                int i = 0;
                //i *= 0.5f / dist;
                while (i * 1f / dist < dist)
                {
                    Debug.Log("Distance=" + dist.ToString());
                    Vector3 position = Vector3.Lerp(source.localPosition, destination.localPosition, i * 1f / dist);
                    // TODO : create object at position
                    var pathDot = Instantiate(dot);
                    pathDot.transform.parent = gameObject.transform;
                    pathDot.transform.localPosition = position;

                    if (Vector3.Distance(position, destination.localPosition) > 0.5f)
                    {
                        var pathArrow = Instantiate(arrow);
                        pathArrow.transform.parent = realPath.transform;
                        pathArrow.transform.localPosition = position;
                        Vector3 datAss;
                        datAss = destination.localPosition;
                        datAss.y = realPath.transform.position.y;
                        pathArrow.transform.LookAt(datAss);
                    }


                    i++;
                }
            }

        }
        public void doThing()
        {
            StartCoroutine(LoadPois("building_e80bcc63-c0fb-480a-9eec-e3abf419dd08_1550682078007", 0));
        }

        // Use this for initialization
        private void Start () {
            Debug.Log("STARTED MAIN BEHAVIOUR");
            anyplace = new AnyplaceService();
            // MixedRealityToolkit.Instance.RegisterService(typeof(AnyplaceService), Anyplace);
           StartCoroutine(LoadPois("building_e80bcc63-c0fb-480a-9eec-e3abf419dd08_1550682078007", 0));
        
		
        }
	
        // Update is called once per frame
        private void Update () {
		
        }

        // Used to decompress anyplace responses as they are gzipped and this has caused me so much pain for no reason
        // TODO Also will eventually need to write either a custom downloadHandler or a WebRequest class that wraps UnityWebRequest and
        // handles gzipped stuff based on the content-encoding reponse header among other things. I could also possibly use the MixedReality
        // Rest class but will need to look into how to handle coroutines given their custom async awaiter stuff for coroutines, might actually
        // make things a lot nicer
        private byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip),
                CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }
    }
}
