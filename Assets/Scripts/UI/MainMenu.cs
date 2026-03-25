using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> slotButtons = new List<GameObject>();
    [SerializeField] private List<GameObject> deleteButtons = new List<GameObject>();

    private static readonly int _numberOfProfiles = 3;
    private static readonly string _defaultSlotString = "EMPTY SLOT";
    private static readonly string _wildCardString = "*";
    private static readonly string _fileExtension = ".txt";
    private static readonly string _profileFileName = "profile_" + _wildCardString + _fileExtension;

    [SerializeField] private GameObject LoadMenu;
    [SerializeField] private GameObject newProfileMenu;
    [SerializeField] private InputField inputField;

    private readonly ProfileData[] _profiles = new ProfileData[_numberOfProfiles];

    private int _newProfileMenuIndex = -1;

    void Start()
    {
        LoadProfiles();
        RefreshMainMenu();
    }

    public void PlayProfile(int index)
    {
        //TODO: set profile to persistent object
        //_profiles[index];
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
            File.WriteAllText(GetProfilePath(_newProfileMenuIndex), JsonUtility.ToJson(profile));

            CloseNewProfileMenu();
            RefreshMainMenu();
        }
    }

    public void DeleteProfile(int index)
    {
        string path = GetProfilePath(index);
        if (File.Exists(path))
        {
            File.Delete(path);
            _profiles[index] = null;
        }

        RefreshMainMenu();
    }

    private void LoadProfiles()
    {
        for (int i = 0; i < _numberOfProfiles; i++)
        {
            string path = GetProfilePath(i);
            if (File.Exists(path))
            {
                _profiles[i] = JsonUtility.FromJson<ProfileData>(File.ReadAllText(path));
            }
        }
    }

    private void RefreshMainMenu()
    {
        for (int i = 0; i < _numberOfProfiles; i++)
        {
            int index = i;
            slotButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            deleteButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();

            if (_profiles[i] != null)
            {
                slotButtons[i].GetComponentInChildren<Text>().text = _profiles[i].ProfileName;
                slotButtons[i].GetComponent<Button>().onClick.AddListener(() => PlayProfile(index));

                deleteButtons[i].GetComponent<Button>().onClick.AddListener(() => DeleteProfile(index));
                deleteButtons[i].SetActive(true);
            }
            else
            {
                slotButtons[i].GetComponentInChildren<Text>().text = _defaultSlotString + " " + (i + 1);
                slotButtons[i].GetComponent<Button>().onClick.AddListener(() => OpenNewProfileMenu(index));

                deleteButtons[i].SetActive(false);
            }
        }
    }

    private string GetProfilePath(int index)
    {
        return Path.Combine(Application.persistentDataPath, _profileFileName.Replace(_wildCardString, (index + 1).ToString()));
    }
}
