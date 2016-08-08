using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class ReadWrite : MonoBehaviour
{
    string monthVar = System.DateTime.Now.Day + "." + System.DateTime.Now.Month + "." + System.DateTime.Now.Year + "@" + System.DateTime.Now.Hour + "." + System.DateTime.Now.Minute;
    string path;
    
	// Use this for initialization
	void Start ()
    {
        path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\FeedBack (" + monthVar + ").txt";


        if (!File.Exists(path))//this will only happen if document doesnt exist. we need to make sure the path gets changed each time they submit a comment
        {
            //create a file to write to, this will need to happen every time they want to leave a comment
            string createText = "Hello and Welcome " + Environment.NewLine;
            File.WriteAllText(path, createText);
        }
        else
        {
            //to append things after the file is created, use the following
            string appendText = "This is extra text" + Environment.NewLine;
            File.AppendAllText(path, appendText);
        }
        

        //open a file to read from
        string readText = File.ReadAllText(path);
        Console.WriteLine(readText);
	}
	    
	// Update is called once per frame
	void Update () {
	
	}
}
