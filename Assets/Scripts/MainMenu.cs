using System;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SaveLoadUI saveLoadUI;
    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button deleteSaveButton;

    private void UpdateContinueButton()
    {
        if(continueButton == null)
        {
            Debug.LogWarning("ContinueButton is missing");
            return;
        }

        continueButton.interactable = saveSystem != null && saveSystem.HasSavedFile();
    }

    public void ContinueGame()
    {
        if(saveSystem == null || !saveSystem.HasSavedFile())
        {
            Debug.LogWarning("No Save File available");
            UpdateButton();
            return;
        }

        if(saveLoadUI == null)
        {
            Debug.LogWarning("SaveLoadUI is missing");
            return;
        }

        saveLoadUI.LoadGame();
    }

    private void Start()
    {
        UpdateButton();
    }

    public void StartGame()
    {
        if(GameSession.Instance != null)
        {
            GameSession.Instance.ResetSession();
        }
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void UpdateButton()
    {
        bool hasSave = saveSystem != null && saveSystem.HasSavedFile();

        if(continueButton != null)
            continueButton.interactable = hasSave;

        if(deleteSaveButton != null)
            deleteSaveButton.interactable = hasSave;
    }

    public void DeleteSaveGame()
    {
        saveSystem.DeleteSave();
        UpdateButton();
    }

}
