using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    /*
     * **********************************
     * 다른 코드에서 호출할 때
     * LoadingSceneManager.LoadScene(넘길 씬 이름);
     * 쓰면 될거임
     * **********************************
     */
    public static string nextScene;
    //[SerializeField] Image progressBar;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        /*while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
<<<<<<< Updated upstream
            if(op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if(progressBar.fillAmount == 1.0f)//>= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);
                if(progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    yield break;//return new WaitForSeconds(4.0f);
                }
            }
            
                
        }*/
        while(true)
        {
            timer += Time.deltaTime;

            if(timer >= 10000.0f)
            {
                timer = 0.0f;
                op.allowSceneActivation = true;
                yield break;
                
            }
=======
            //if(op.progress < 0.9f)
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
            //    if(progressBar.fillAmount >= op.progress)
            //    {
            //        timer = 0f;
            //    }
            //}
            //else
            //{
            //    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1.0f, timer);
            //    if(progressBar.fillAmount == 1.0f)
            //    {
            //        op.allowSceneActivation = true;
            //        yield return new WaitForSeconds(4.0f);
            //    }
            //}
>>>>>>> Stashed changes
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
