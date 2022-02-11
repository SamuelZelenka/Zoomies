using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderWithTransition : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;
    private Image[] _children;

    private void Awake()
    {
        _children = GetComponentsInChildren<Image>();
        foreach (var child in _children)
        {
            child.enabled = true;
        }
    }

    

    public void Load(string sceneName)
    {
        StartCoroutine(Coroutine(sceneName));
    }
    
    
    private IEnumerator Coroutine(string sceneName)
    {
        transition.SetTrigger("Close");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
    
}
