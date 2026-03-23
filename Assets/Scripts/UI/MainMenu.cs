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

    private List<ProfileData> _profiles;
    
    void Start()
    {
        LoadProfiles();
    }

    public void Play(int index)
    {
        //TODO: set profile to persistent object
        //_profiles[index];
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenNewProfileMenu()
    {
        newProfileMenu.SetActive(true);
        LoadMenu.SetActive(false);
    }

    public void NewProfile(string name)
    {
        //TODO: create new profile with "name"
        //Debug.Log(name);
    }

    private void LoadProfiles()
    {
        for (int i = 0; i < _numberOfProfiles; i++)
        {
            string fileName = _profileFileName.Replace(_wildCardString, (i + 1).ToString());
            string path = Path.Combine(Application.persistentDataPath, fileName);

            if (File.Exists(path))
            {
                string profileDataString = File.ReadAllText(path);
                ProfileData profile = JsonUtility.FromJson<ProfileData>(profileDataString);
                _profiles[i] = profile;

                slotButtons[i].GetComponentInChildren<Text>().text = profile.ProfileName;
                int index = i;
                slotButtons[i].GetComponent<Button>().onClick.AddListener(()=>Play(index));
                deleteButtons[i].SetActive(true); //TODO: add delete buttons

            }
            else
            {
                slotButtons[i].GetComponentInChildren<Text>().text = _defaultSlotString + " " + (i + 1);
                slotButtons[i].GetComponent<Button>().onClick.AddListener(OpenNewProfileMenu);
            }
        }
    }
}
