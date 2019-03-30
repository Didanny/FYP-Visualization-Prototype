using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFirebaseUnity;
using Assets.FYP;


public class FirebaseManager : MonoBehaviour {
    Firebase firebase;
    Firebase workers;


    // Use this for initialization
    void Start () {
        
        firebase = Firebase.CreateNew("https://fyp-warehouse.firebaseio.com/");

        //firebase.Child("Messages").Push("{ \"name\": \"simple-firebase-unity\", \"message\": \"awesome!\"}", true);
        //workers = firebase.Child("Workers");
        //Debug.Log("jdhjdjhdhdjdjhd djdjdjd ");

        firebase.OnGetSuccess += (Firebase sender, DataSnapshot snapshot) =>
        {
            Debug.Log("Successful Get Worker: <" + snapshot.RawJson + ">");
            Worker worker = JsonUtility.FromJson<Worker>(snapshot.RawJson);
            Debug.Log("Worker Extracted: " + JsonUtility.ToJson(worker));

            //Dictionary<string, object> dictionary = snapshot.Value < Dictionary<string, object> > ();
            //object job;
            //dictionary.TryGetValue("1", out job);
            //Dictionary<string, string> jobDict = job as Dictionary<string, string>;
            //Debug.Log("Dictionary: " + jobDict.ToString());
        };
        firebase.OnGetFailed += (Firebase sender, FirebaseError error) =>
        {
            Debug.Log("Get Failed: <" + error.ToString() + ">");
        };

        //firebase.Child("Workers").OnGetSuccess += (Firebase sender, DataSnapshot snapshot) =>
        //{
        //    Debug.Log("Successful Get Worker: <" + snapshot.RawJson + ">");
        //};
        //firebase.Child("Workers").OnGetFailed += (Firebase sender, FirebaseError error) =>
        //{
        //    Debug.Log("Get Failed: <" + error.ToString() + ">");
        //};

        //testLoadWorker();
        Invoke("testLoadWorker", 5f);

        //firebase.OnDeleteSuccess += (Firebase sender, DataSnapshot snapshot) => {
        //    Debug.Log("[OK] Delete from " + sender.Endpoint + ": " + snapshot.RawJson);
        //};

        //firebase.OnUpdateFailed += UpdateFailedHandler;
        // Method signature: void UpdateFailedHandler(Firebase sender, FirebaseError err)

        //firebase.GetValue("print=pretty");
    }

    public void loadWorker(string id)
    {
        firebase.Child("Workers", true).Child(id, true).GetValue();
        //firebase.GetValue();
        //firebase.Child("Workers", true).GetValue();
    }

    public void testLoadWorker()
    {
        Debug.Log("Entered testLoadWorker");
        loadWorker("1");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
