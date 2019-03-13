using Assets.FYP;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR.WSA;

// Due to a monumental mistake on my part, I have to rewrite a bunch of code I accdientally deleted. This used to inherit from an
// TODO INavigationService but I'll just create that later for now
// TODO Make into monobehavior
public class AnyplaceService {

    private readonly string BASE_URL = "http://192.168.43.200:9000";
    private readonly Dictionary<string, string> anyplaceHeaders = new Dictionary<string, string>()
    {
        {"Content-Type", "application/json" }
    };

    //public async Task<IEnumerable<GameObject>> GetPoisByFloor(string buid, int floor_number)
    //{
    //    Rest.UseSSL = false;
    //    string url = BASE_URL + "/anyplace/mapping/pois/all_floor";
    //    // TODO Use a JSON Serializable object instead
    //    string jsonBody = $"{{\"buid\":\"{buid}\", \"floor_number\": \"{floor_number}\"}}";

    //    Response response = await Rest.PostAsync(url, jsonBody, anyplaceHeaders);
    //    Debug.Log("ANYPLACE RESPONSE: " + response.ResponseBody + ", " +response.ResponseCode);

    //    //IEnumerable<AnyplacePoi> pois = JsonConvert.DeserializeObject<AnyplacePoiResponse>(response.ResponseBody).Pois;

    //    return pois.Select(anyplacePoi => 
    //    {
    //        // TODO Make this a prefab of some sort
    //        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    //        go.name = anyplacePoi.Puid;
    //        go.GetComponent<Renderer>().material.color = Color.blue;

    //        go.AddComponent<Poi>().FromAnyplace(anyplacePoi);
    //        Debug.Log("POI NAME:" + anyplacePoi.Name);
    //        return go;
    //    });
        
    //}

    public void GenerateWorldAnchors(double lat, double lon, IEnumerable<GameObject> gameObjects)
    {
        //TODO check if gameobjects have Poi components
        Vector3 source = new Vector3((float)lat, 0,(float)lon);

        foreach (var go in gameObjects)
        {
            Poi poi = go.GetComponent<Poi>();
            Vector3 poiVec = new Vector3((float)poi.Lat,0, (float)poi.Lon);
            go.transform.position = GetRelativePosition(source, poiVec);
            go.AddComponent<WorldAnchor>();
            Debug.Log("POI POSITION: " + go.transform.position.ToString());
        }

    }

    // TODO Fix this. Currently using simple subtraction cuz flat earth, yo. 
    // Look into conversion of WGS 84 coordinates to meters. If not that at least use Haversine
    // Currently, using 111,139 as a factor to convert from degrees to meters. This number is taken off a random website
    // Don't know how accurate it is but hopefully its accurate  enough.
    private Vector3 GetRelativePosition(Vector3 source, Vector3 point)
    {
        int conversion_factor = 111139;
        return conversion_factor*(point - source);
    }

}
