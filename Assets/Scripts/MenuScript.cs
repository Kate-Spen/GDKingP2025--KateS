using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void gotoGame()
    {
        StartCoroutine(WaitForSoundAndTransition("SampleScene"));
    }
    public IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
