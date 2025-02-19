using UnityEngine;

public class MenuScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void gotoGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
    public void gotoCharacter()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelect");
    }
    public void gotoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void gotoOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverMenu");
    }
}
