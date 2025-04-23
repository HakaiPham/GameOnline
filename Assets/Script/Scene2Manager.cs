using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Fusion;

public class Scene2Manager : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button buttonFemale;
    public Button buttonMale;
    public TMP_InputField input;
    void Start()
    {
        buttonFemale.onClick.AddListener(() => OnButtonClick("Female"));
        buttonMale.onClick.AddListener(() => OnButtonClick("Male"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnButtonClick(string playerClass)
    {
        //Lay ten tu Input
        var playerName = input.text;
        //Luu thong tin nguoi choi
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetString("PlayerClass", playerClass);

        //LoadScene
        SceneManager.LoadScene("SampleScene");
    }
}
