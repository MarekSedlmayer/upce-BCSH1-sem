using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotButtons = new List<GameObject>();
    [SerializeField] private List<GameObject> deleteButtons = new List<GameObject>();

    private static readonly int _numberOfProfiles = 3;
    private static readonly string _defaultSlotString = "EMPTY SLOT";

    [SerializeField] private GameObject LoadMenu;
    [SerializeField] private GameObject newProfileMenu;
    [SerializeField] private TMP_InputField inputField;

    private readonly ProfileData[] _profiles = new ProfileData[_numberOfProfiles];

    private int _newProfileMenuIndex = -1;

    void Start()
    {
        LoadProfiles();
        RefreshMainMenu();
    }

    public void PlayProfile(int index)
    {
        ProfileManager.SetActiveProfile(_profiles[index], index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenNewProfileMenu(int index)
    {
        _newProfileMenuIndex = index;
        newProfileMenu.SetActive(true);
        LoadMenu.SetActive(false);
    }
    public void CloseNewProfileMenu()
    {
        _newProfileMenuIndex = -1;
        LoadMenu.SetActive(true);
        newProfileMenu.SetActive(false);
    }

    public void NewProfile()
    {
        if (inputField.text.Length > 0 && _newProfileMenuIndex >= 0)
        {
            ProfileData profile = new ProfileData { ProfileName = inputField.text };
            _profiles[_newProfileMenuIndex] = profile;
            ProfileManager.SaveProfile(profile, _newProfileMenuIndex);

            CloseNewProfileMenu();
            RefreshMainMenu();
        }
    }

    public void DeleteProfile(int index)
    {
        if (ProfileManager.DeleteProfile(index)) _profiles[index] = null;

        RefreshMainMenu();
    }

    private void LoadProfiles()
    {
        for (int i = 0; i < _numberOfProfiles; i++)
        {
            _profiles[i] = ProfileManager.LoadProfile(i);
        }
    }

    private void RefreshMainMenu()
    {
        for (int i = 0; i < _numberOfProfiles; i++)
        {
            int index = i;
            Button slotButton = slotButtons[i].GetComponent<Button>();
            Button deleteButton = deleteButtons[i].GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
            deleteButton.onClick.RemoveAllListeners();

            if (_profiles[i] != null)
            {
                slotButton.GetComponentInChildren<TextMeshProUGUI>().text = _profiles[i].ProfileName;
                slotButton.onClick.AddListener(() => PlayProfile(index));

                deleteButton.onClick.AddListener(() => DeleteProfile(index));
                deleteButton.interactable = true;
            }
            else
            {
                slotButton.GetComponentInChildren<TextMeshProUGUI>().text = _defaultSlotString + " " + (i + 1);
                slotButton.onClick.AddListener(() => OpenNewProfileMenu(index));

                deleteButton.interactable = false;
            }
        }
    }
}
