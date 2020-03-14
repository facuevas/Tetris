using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //initialize important text, panels, and audio fields
    public Text scoreText, lineText, levelText, volumeText;
    public GameObject pausePanel, gameoverPanel;
    public Toggle mToggle;

    // Update is called once per frame
    void Update()
    {
        //Check if music is enabled
        MusicEnabled(mToggle);

        //score updates
        UpdateGameUI();

        //Checks if we pause the game and displays the pause menu display
        PauseMenuDisplay(GameMaster.gamePaused);

        //Checks if the we display the gameover scree and display game over screen
        GameOverDisplay(GameMaster.gameOver);

        //volume change updates
        //Audio.tetris_song.GetComponent<AudioSource>().volume = volume_slider.value;
    }

    private void UpdateGameUI()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            scoreText.text = "Score: " + GameMaster.Score.ToString();
            lineText.text = "Lines: " + GameMaster.Lines.ToString();
            levelText.text = "Level: " + GameMaster.Level.ToString();
        }

        volumeText.text = string.Format("Volume: {0:0}%", Audio.volumeLevel * 100);
    }

    /*
     * This function displays the pause menu
     */
    private void PauseMenuDisplay(bool pause)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (pause)
                pausePanel.SetActive(true);
            else
                pausePanel.SetActive(false);
        }
    }

    /*
     * This function displays the game over screen
     */ 
    private void GameOverDisplay(bool go)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            if (go)
                gameoverPanel.SetActive(true);
            else
                gameoverPanel.SetActive(false);
    }

    /*
    * This function handles slider events
    */
    public void SliderEvents(Slider s)
    {
        switch (s.name)
        {
            case "VolumeSlider":
                Audio.volumeLevel = s.value;
                break;
        }
    }

    public void MusicEnabled(Toggle t)
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
            if (t.isOn)
                Audio.musicEnabled = true;
            else
                Audio.musicEnabled = false;
    } 

    /*
    * This function handles button events
    */
    public void ButtonEvents(Button b)
    {
        switch (b.name)
        {
            case "ExitGameButton":
                Debug.Log("Exit Button Clicked");
                Application.Quit();
                break;
            case "PlayButton":
                Debug.Log("Play Button Clicked");
                SceneManager.LoadScene(1);
                break;
            case "ReturnToMainMenuButton":
                Debug.Log("Return to main menu button clicked");
                SceneManager.LoadScene(0);
                break;
            case "ReturnToGameButton":
                Debug.Log("Return to game button clicked");
                GameMaster.gamePaused = false;
                break;
            case "RestartGameButton":
                SceneManager.LoadScene(1);
                break;
        }
    }
}
