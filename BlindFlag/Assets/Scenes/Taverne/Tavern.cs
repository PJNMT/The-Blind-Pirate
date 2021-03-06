﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Tavern : MonoBehaviour
{
    public GameObject Bar;
    public GameObject Table1;
    public GameObject Table2;
    public GameObject Sol;
    
    public GameObject drunken;
    public GameObject recrutes;
    public GameObject singer;

    public AudioClip D;
    public AudioClip R;
    public AudioClip S;

    private AudioSource Audio;
    
    public AudioClip Simon; 
    public AudioClip Recrute; 
    public AudioClip Drink; 
    public AudioClip Matelot;
    public AudioClip NotEnougthGold;
    
    public AudioClip T1;
    public AudioClip T2;
    public AudioClip S1;
    public AudioClip B1;

    public bool sedeplacer;
    
    // Start is called before the first frame update
    void Start()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => Table1.GetComponent<AudioSource>().clip = T1);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Table1.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Table1.GetComponent<AudioSource>().Play());
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Table2.GetComponent<AudioSource>().clip = T2);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Table2.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Table2.GetComponent<AudioSource>().Play());
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Sol.GetComponent<AudioSource>().clip = S1);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Sol.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Sol.GetComponent<AudioSource>().Play());
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Bar.GetComponent<AudioSource>().clip = B1);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Bar.GetComponent<AudioSource>().loop = true);
        UnityMainThreadDispatcher.Instance().Enqueue(() => Bar.GetComponent<AudioSource>().Play());
        
        Audio = GetComponent<AudioSource>();
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchTavern());
    }

    public void LaunchTavern()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.position = new Vector3(7.44f, 1f, 7f));
        
        sedeplacer = true;
        UnityMainThreadDispatcher.Instance().Enqueue(() => drunken.GetComponent<AudioSource>().PlayOneShot(D));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) D.length * 1000 + 500));
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => recrutes.GetComponent<AudioSource>().PlayOneShot(R));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) R.length * 1000 + 500));
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => singer.GetComponent<AudioSource>().PlayOneShot(S));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) S.length * 1000 + 500));

        Recognition.Function Func = MainTavern;
        UnityMainThreadDispatcher.Instance().Enqueue(() => Recognition.start_recognition(Func, "chanson chanter jouer boire simon payer tournée recruter recrue nouveau équipage matelot quitter stop retourner retour port"));
    }

    private void MainTavern(string speech)
    {
        if (speech != "quitter" && speech != "stop" && speech != "retourner" && speech != "retour" && speech != "port")
        {
            switch (speech)
            {
                case "simon":
                case "jouer":
                case "chanter":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Sing());
                    break;
                
                case "recruter":
                case "recrue":
                case "nouveau":
                case "matelot":
                case "équipage":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Recrutes());
                    break;
                
                case "payer":
                case "tournée":
                case "boire":
                    UnityMainThreadDispatcher.Instance().Enqueue(() => Drinks());
                    break;
            }
        }
        else UnityMainThreadDispatcher.Instance().Enqueue(() => LoadScene.Load(LoadScene.Scene.Port, LoadScene.Scene.Taverne));
    }

    private void Sing()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.position = new Vector3(3.64f, 1f, 3.65f));
    }

    private void Recrutes()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.position = new Vector3(-5.95f, 0.94f, 5.95f));
        
        int crew_members = 0;
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => crew_members = Random.Range(0, BlindShip_Stat.Max_Crew - BlindShip_Stat.Crew));
        UnityMainThreadDispatcher.Instance().Enqueue(() => BlindShip_Stat.AddCrew(GetComponent<AudioSource>(), crew_members));
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Recrute));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Recrute.length * 1000 + 500));
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Synthesis.synthesis(crew_members + ""));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep(1300));
        
        UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Matelot));
        UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Matelot.length * 1000 + 500));
                    
        UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchTavern());
    }
    
    private void Drinks()
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => transform.position = new Vector3(-3.61f, 0.94f, -2.77f));
        
        int available_money = BlindShip_Stat.Money -= (BlindShip_Stat.Crew * 20);
        
        if (available_money < 0)
        {
            BlindCaptain_Stat.Reputation -= 10;
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(NotEnougthGold));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) NotEnougthGold.length * 1000 + 500));
        }
        else
        {
            BlindCaptain_Stat.Reputation += 10 * BlindShip_Stat.Crew;
            BlindShip_Stat.Money = available_money;
            
            UnityMainThreadDispatcher.Instance().Enqueue(() => Audio.PlayOneShot(Drink));
            UnityMainThreadDispatcher.Instance().Enqueue(() => Thread.Sleep((int) Drink.length * 1000 + 500));
        }
            
        UnityMainThreadDispatcher.Instance().Enqueue(() => LaunchTavern());
    }
}
