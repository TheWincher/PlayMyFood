using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public GameObject mainMenu, playMenu;
    public Button play;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(Play);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Play()
    {
        playMenu.SetActive(true);
        mainMenu.SetActive(false);
    }
}
