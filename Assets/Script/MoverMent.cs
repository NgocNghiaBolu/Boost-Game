using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverMent : MonoBehaviour
{
    
    [SerializeField] float SpeedThurst = 1000f;//gia toc de phong len
    [SerializeField] float RotationThrust = 100f;//gia toc khi phong qua trai/phai
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem MainPartical;
    [SerializeField] ParticleSystem RightParticle;
    [SerializeField] ParticleSystem LeftParticle;

    Rigidbody RiB;
    AudioSource AuDi;
    // Start is called before the first frame update
    void Start()
    {
        RiB = GetComponent<Rigidbody>();
        AuDi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
        QuitGame();
    }

   
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrust();
        }

    }
    void ProcessRotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            RightRotation();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            LeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        RiB.AddRelativeForce(Vector3.up * SpeedThurst * Time.deltaTime);
        if (!AuDi.isPlaying)
        {
            AuDi.PlayOneShot(mainEngine);//Audio se dc dung neu k dc hoat dong,con danh hoat dong r dung lam chi
        }
        if (!MainPartical.isPlaying)
        {
            MainPartical.Play();
        }
    }
   void LeftRotation()
    {
        ApplyRotation(-RotationThrust);
        if (!LeftParticle.isPlaying)
        {
            LeftParticle.Play();//x
        }
    }
    void RightRotation()
    {
        ApplyRotation(RotationThrust);//ctrl 
        if (!RightParticle.isPlaying)
        {
            RightParticle.Play();
        }
    }
    void StopThrust()
    {
        AuDi.Stop();
        MainPartical.Stop();
    }

    void StopRotation()
    {
        RightParticle.Stop();
        LeftParticle.Stop();
    }


    void ApplyRotation(float rotationThisFrame)//call bien
    {
        RiB.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);//de update
        RiB.freezeRotation = false;
    }
    void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Game Out");
        }
    }
}
