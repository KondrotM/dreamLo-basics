using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour {

	public Text[] highscoreText;
//Reference to the highscore class
	Highscores highscoreManager;
		
	void Start () {
		//Goes through all the texts inside highscoreText that we have,
		for (int i = 0; i < highscoreText.Length; i++) {
			highscoreText [i].text = i + 1 + ". Fetching...";
		}
		highscoreManager = GetComponent<Highscores> ();

		StartCoroutine ("RefreshHighscores");
	}
	//Appends the fetching text with current highscores,
	//checks that there are enough highscores to display and if there aren't displays a dot.
	public void OnHighscoresDownloaded(Highscore[] highscoreList) {
		for (int i = 0; i < highscoreText.Length; i++) {
			highscoreText [i].text = i + 1 + ". ";
			if (highscoreList.Length > i) {
				highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
			}
		}
	}
	//Every so often, refreshes highscores
	IEnumerator RefreshHighscores() {
		while (true) {
			highscoreManager.DownloadHighscores ();
			yield return new WaitForSeconds (30);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
