// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 22/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;

public class SceneGetter : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.Instance.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }

}