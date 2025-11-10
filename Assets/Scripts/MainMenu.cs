using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenu : MonoBehaviour
{
    public TMP_Text hstext;
    private void Start()
    {
        int hs = GameData.LoadHighScore();
        hstext.text = "High Score : " + hs;

        ChangeState(State.Main);
    }
    public void PlayLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    private enum State
    {
        Main,
        CarSelection
    }

    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _carSelectionPanel;

    [SerializeField] private GameObject _carPanel;

    private State _currentState;

    private void ChangeState(State state)
    {
        _currentState = state;
        _mainPanel.SetActive(_currentState == State.Main);
        _carSelectionPanel.SetActive(_currentState == State.CarSelection);
        _carPanel.SetActive(_currentState == State.CarSelection);
    }

    public void OnReturnButtonClicked()
    {
        ChangeState(State.Main);
    }

    public void OnStartButtonClicked()
    {
        ChangeState(State.CarSelection);
    }
}
