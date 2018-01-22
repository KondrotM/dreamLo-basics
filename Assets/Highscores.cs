//following Sebastian Lague's tutorial on how to create online highscores in dreamLo


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour {

	//three codes for highscore table
	const string privateCode = "M330SAFZq0uZ0lhVH428mwT4D_6QjSu02lIW9sq9e-ww";
	const string publicCode = "5a5ffacd39992b09e438fc99";
	const string webURL = "http://dreamlo.com/lb/";

	//array from highscore data structure
	public Highscore[] highscoresList;

	void Awake() {
		AddNewHighscore ("Sebastian", 50);
		AddNewHighscore ("Mary", 85);
		AddNewHighscore ("Bob", 92);
	
		DownloadHighscores ();
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
	public void DownloadHighscores() {
		StartCoroutine ("DownloadHighScoresFromDatabase");
	}

	IEnumerator DownloadHighScoresFromDatabase() {
		//creates the web url for fetching highscores
		WWW web = new WWW (webURL + privateCode + "/pipe/");
		//waits for a return rather than immediately continuing with the code
		yield return web;

		if (string.IsNullOrEmpty (web.error)) 
			//prints the scoreboard
			FormatHighscores(web.text);
		else {
			print ("Errror downloading: " + web.error);
		}
	}

	void FormatHighscores(string textStream) {
		//splits up all highscore entries, depending on where a new line begins
		string[] entries = textStream.Split (new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries); //removes the final empty line from being an entry

		highscoresList = new Highscore[entries.Length];
		//loops for the number of entries in the table
		for (int i = 0; i < entries.Length; i++) {
			//creates a new sub-section when the pipe symbol is used
			string[] entryInfo = entries[i].Split (new char[] { '|' });
			string username = entryInfo [0];
			//makes the score into an integer before setting it as a seperate variable
			int score = int.Parse (entryInfo [1]);
			//updates the highscoresList array with current username and score, then prints it
			highscoresList [i] = new Highscore (username, score);
			print (highscoresList [i].username + ": " + highscoresList [i].score);

		}
	}

}
//creates data structure for storing all highscores
public struct Highscore {
	public string username;
	public int score;

	//creates a structure for what a highscore entry should look like
	public Highscore(string _username, int _score) {
		username = _username;
		score = _score;
	}
}