using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPath : MonoBehaviour {

    [SerializeField] GameObject realPath;
    [SerializeField] GameObject dot;
    [SerializeField] GameObject arrow;

    public List<int> dotList;
    public Transform[] children;


	// Use this for initialization
	void Start () {
        children = GetComponentsInChildren<Transform>();
        
        for (int i = 0; i < children.Length; i++)
        {
            //Debug.Log("Index:" + i.ToString());
            //Debug.Log(children[i].transform.localPosition.ToString());
        }

        dotList = new List<int>();
        dotList.Add(1);
        dotList.Add(2);
        dotList.Add(3);
        dotList.Add(4);
        dotList.Add(5);

        //drawPath();
    }

    public void drawPath()
    {
        for (int j = 0; j < dotList.Count - 1; j++)
        {
            var source = children[dotList[j]].transform;
            var destination = children[dotList[j + 1]].transform;
            float dist = Vector3.Distance(source.localPosition, destination.localPosition);
            int i = 0;
            //i *= 0.5f / dist;
            while (i*1f/dist < dist)
            {   
                Debug.Log("Distance=" + dist.ToString());
                Vector3 position = Vector3.Lerp(source.localPosition, destination.localPosition, i * 1f/dist);
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

    // Update is called once per frame
    void Update () {

    }
}
