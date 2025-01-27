using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_System : MonoBehaviour
{
    public static Game_System instance;

    public Deck_System deck;
    public Cards_System cards;
    public Hand_System hand;
    public Sound_Player sound;
    public Show_Help help;

    private void Start()
    {
        makeSingletone();
    }

    private void makeSingletone()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this);
    }

    public static void SetSingletone(MonoBehaviour mono)
    {
        switch (mono.GetType().ToString())
        {
            case "Deck_System":
                if (instance.deck == null)
                    instance.deck = (Deck_System)mono;
                return;

            case "Cards_System":
                if (instance.cards == null)
                    instance.cards = (Cards_System)mono;
                return;

            case "Hand_System":
                if (instance.hand == null)
                    instance.hand = (Hand_System)mono;
                return;

            case "Sound_Player":
                if (instance.sound == null)
                    instance.sound = (Sound_Player)mono;
                return;

            case "Show_Help":
                if (instance.help == null)
                    instance.help = (Show_Help)mono;
                return;
        }

        Destroy(mono);

    }

    public static void PlaySound(soundNames audioName)
    {
        instance.sound.PlayOneShot(audioName);
    }
}
