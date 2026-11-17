using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance { get; set; }
    public int actualMusicTrack = 0;
    public AudioClip[] soundEffects; // Array de efectos de sonido
    public AudioClip[] musicTracks;  // Array de pistas de música
    public AudioSource audioSource;  // AudioSource para los efectos de sonido
    public AudioSource backgroundMusic; // AudioSource para la música de fondo

    public float soundEffectsVolume = 1.0f;  // Volumen de los efectos de sonido (rango 0-1)
    public float backgroundMusicVolume = 0.5f; // Volumen de la música de fondo (rango 0-1)

    // Establecer la instancia del controlador
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Evitar que se destruya al cargar nuevas escenas
        }
        else
        {
            Destroy(gameObject); // Evitar duplicados
        }
        PlayBackgroundMusic(actualMusicTrack);
    }

    // Reproducir un efecto de sonido dado su índice
    public void PlaySound(int indexSound)
    {
        if (indexSound >= 0 && indexSound < soundEffects.Length)
        {
            audioSource.PlayOneShot(soundEffects[indexSound], soundEffectsVolume);
        }
        else
        {
            Debug.LogWarning("El índice de sonido es inválido.");
        }
    }

    // Obtener un efecto de sonido por índice (Método estático)
    public static AudioClip GetSound(int indexSound)
    {
        if (instance != null)
        {
            if (indexSound >= 0 && indexSound < instance.soundEffects.Length)
            {
                return instance.soundEffects[indexSound];
            }
            else
            {
                Debug.LogWarning("El índice de sonido es inválido.");
                return null;
            }
        }
        return null;
    }

    // Reproducir música de fondo dado el índice
    public static void PlayBackgroundMusic(int indexTrack)
    {
        if (instance != null && indexTrack >= 0 && indexTrack < instance.musicTracks.Length)
        {
            instance.backgroundMusic.clip = instance.musicTracks[indexTrack];
            instance.backgroundMusic.loop = true; // La música de fondo debe repetirse
            instance.backgroundMusic.volume = instance.backgroundMusicVolume;
            instance.backgroundMusic.Play();
        }
        else
        {
            Debug.LogWarning("El índice de pista de música es inválido.");
        }
    }

    // Detener la música de fondo
    public static void StopBackgroundMusic()
    {
        if (instance != null)
        {
            instance.backgroundMusic.Stop();
        }
    }

    // Ajustar el volumen de los efectos de sonido (Método estático)
    public static void SetSoundEffectsVolume(float volume)
    {
        if (instance != null)
        {
            instance.soundEffectsVolume = Mathf.Clamp01(volume);  // Asegurarse de que el volumen esté entre 0 y 1
            // Reajustar volumen de los efectos si es necesario
        }
    }

    // Ajustar el volumen de la música de fondo (Método estático)
    public static void SetBackgroundMusicVolume(float volume)
    {
        if (instance != null)
        {
            instance.backgroundMusicVolume = Mathf.Clamp01(volume);  // Asegurarse de que el volumen esté entre 0 y 1
            instance.backgroundMusic.volume = instance.backgroundMusicVolume;
        }
    }

    // Pausar la música de fondo
    public static void PauseBackgroundMusic()
    {
        if (instance != null)
        {
            instance.backgroundMusic.Pause();
        }
    }

    // Reanudar la música de fondo
    public static void ResumeBackgroundMusic()
    {
        if (instance != null)
        {
            instance.backgroundMusic.UnPause();
        }
    }
}
