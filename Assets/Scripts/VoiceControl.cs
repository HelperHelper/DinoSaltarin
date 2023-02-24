using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Windows.Speech;


public class VoiceControl : MonoBehaviour
{

    private Dictionary<string, Action> keyActs = new Dictionary<string, Action>();
   [HideInInspector] public KeywordRecognizer recognizer;


    void Start()
    {
        // Voice commands for Jump
        keyActs.Add("arriba", Saltar);
        //Voice command for crouch
        keyActs.Add("abajo", Agachar);

        // Restart Game
        keyActs.Add("reiniciar", Reiniciar);

        recognizer = new KeywordRecognizer(keyActs.Keys.ToArray());
        recognizer.OnPhraseRecognized += OnKeywordsRecognized;
        recognizer.Start();

      

    }

    void OnDestroy()
    {
        if (recognizer != null)
        {
            recognizer.Stop();
            recognizer.Dispose();
        }
    }

    void OnKeywordsRecognized(PhraseRecognizedEventArgs args)
    {
        //Debug.Log("Commad: " + args.text);
        keyActs[args.text].Invoke();
    }

    void Reiniciar()
    {
        GameOver.Instance.Retry();
    }

    void Saltar()
    {
        PlayerController.Instance.Jump();
    }

    void Agachar()
    {
        PlayerController.Instance.Crouch();
    }
}
