using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour {

	//three codes for highscore table
	const string privateCode = "M330SAFZq0uZ0lhVH428mwT4D_6QjSu02lIW9sq9e-ww";
	const string publicCode = "5a5ffacd39992b09e438fc99";
	const string webURL = "http://dreamlo.com/lb/";
		
	void Awake() {
		AddNewHighscore ("Sebastian", 50);
		AddNewHighscore ("Mary", 85);
		AddNewHighscore ("Bob", 92);
			}

	public void AddNewHighscore(string username, int score) {
		StartCoroutine (UploadNewHighscore (username, score));
	}
	//IEnumerator is some fancy way to declare, I don't understand it and google isn't playing ball.
	IEnumerator UploadNewHighscore(string username, int score) {
		//creates the web url for adding your highscore into the table
		WWW web = new WWW (webURL + privateCode + "/add/" + WWW.EscapeURL (username) + "/" + score);
		//waits for a return rather than immediately continuing with the code
		yield return web;

		if (string.IsNullOrEmpty (web.error)) 
			print ("Upload successful");
		else {
			print ("Errror uploading: " + web.error);
		}
	}

}