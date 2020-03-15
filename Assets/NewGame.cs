using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour {
        public GameObject gameUi;
        public GameObject newGame;
    public void New()
    {
        PlayerPrefs.DeleteAll();
        gameUi.SetActive(true);
        newGame.SetActive(false);
    }
}
