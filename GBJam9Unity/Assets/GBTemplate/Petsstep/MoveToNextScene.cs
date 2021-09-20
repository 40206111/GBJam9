using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        GameManager.Instance.LoadNextScene(eTransitionEnums.Petsstep, 0.5f);
    }

}
