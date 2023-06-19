using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConllisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Loss;

    [SerializeField] ParticleSystem ParticleSuccess;
    [SerializeField] ParticleSystem ParticleCrash;
   
    AudioSource AuDi;

    bool isTransitioning = false;//ktra va cham
    
    void Start()
    {
        AuDi = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning == true)
        {
            return;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You Conlli Friendly ");
                break;

            case "Finish":
                AuDi.PlayOneShot(Success);
                NextCuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
              
        }
    }

    void NextCuccessSequence()
    {
        isTransitioning = true;//va cham dung 
        AuDi.Stop();//thi dung am thanh lai
        AuDi.PlayOneShot(Success);
        ParticleSuccess.Play();
        GetComponent<MoverMent>().enabled = false;
        Invoke("NextLoadLevel",levelLoadDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        AuDi.Stop();
        AuDi.PlayOneShot(Loss);
        ParticleCrash.Play();
        GetComponent<MoverMent>().enabled = false;
        Invoke("ReLoad", levelLoadDelay);

    }
    void NextLoadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int NextScene = currentScene + 1;// neu moi lan next no se tu tang len 1 scene
        if(NextScene == SceneManager.sceneCountInBuildSettings)//nu next = voi so luong scene thi tro ve tu dau
        {
            NextScene = 0;
        }
        SceneManager.LoadScene(NextScene);
    }
    void ReLoad()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    } 
    public void LoadButton(string name)
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
         Application.Quit();
         Debug.Log("Game Quit");
        
    }
}
