using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioClipMatch
{
    public  int sceneIndex;
    public AudioClip audioClip; 
}
public class BackGroundMusicManager : MonoBehaviour
{
    private static BackGroundMusicManager instance;
    public static BackGroundMusicManager Instance { get { return instance; } }

    [SerializeField]
    private AudioClipMatch[] audioClipMatch; 
    public Dictionary<int, AudioClip> sceneAudioClip;
    [SerializeField]
    private DataManager dataManger;
    [SerializeField]
    private AudioSource audioSource;

    AudioSource[] LevelAudioSources;

    private void Awake()
    {
        if (instance == null)
            instance = this;
      
        sceneAudioClip = new Dictionary<int, AudioClip>();
        foreach (AudioClipMatch acm in audioClipMatch)
        {
            sceneAudioClip.Add(acm.sceneIndex, acm.audioClip);
        }
    }
    private void Start()
    {
       
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene level,LoadSceneMode loadSceneMode)
    {
        LevelAudioSources = GameObject.FindObjectsOfType<AudioSource>(); 
        foreach(AudioSource audio in LevelAudioSources)
        {
            audio.mute = audioSource.mute = !DataManager.Instance.PlayerData.IsSoundPlaying;

        }
        AudioClip nextAudioClip; 
        
        if (sceneAudioClip.TryGetValue(level.buildIndex,out nextAudioClip))
        {
            if (nextAudioClip != audioSource.clip)
            {
                audioSource.clip = nextAudioClip;
                audioSource.Play();

            }

        }


    }

    public void MuteRestore()
    {
        DataManager.Instance.PlayerData.IsSoundPlaying = !DataManager.Instance.PlayerData.IsSoundPlaying;
        foreach (AudioSource audio in LevelAudioSources)
        {
            audio.mute = audioSource.mute = !DataManager.Instance.PlayerData.IsSoundPlaying;

        }
        DataManager.Instance.SaveData();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
