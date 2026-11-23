using UnityEngine;
using System;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource musicSource;
    public AudioSource playerSfxSource;
    public Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadClipsFromResources();
    }
    void LoadClipsFromResources()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Audio");
        for (int i = 0; i < clips.Length; i++)
        {
            audioClips.Add(clips[i].name, clips[i]);
        }
    }



    // dùng để play music oneshot (có audio source riêng so với sfx)
    public void PlayMusic(string src="", float volumn=1)
    {
        if (src.Equals(""))
        {
            return;
        }
        musicSource.PlayOneShot(audioClips[src], volumn);
    }


    // dùng để play 1 sfx tại 1 điểm trong không gian
    public void PlaySFXAt(string src="", float volumn=1f, Vector2 position=default)
    {
        if (src.Equals(""))
        {
            return;
        }
        AudioSource.PlayClipAtPoint(audioClips[src], position, volumn);
    }

    // dùng để play ngẫu nhiên 1 danh sách sfx tại 1 điểm trong không gian
    public void PlaySFXsAt(string[] src, float volumn=1f, Vector2 position=default)
    {
        if (src.Equals(""))
        {
            return;
        }
        AudioSource.PlayClipAtPoint(audioClips[src[UnityEngine.Random.Range(0, src.Length)]], position, volumn);
    }

    // dùng để play 1 sfx tại ngay vị trí player (nghe rõ nhất)
    public void PlaySFX(string src="", float volumn = 1f)
    {
        if (src.Equals(""))
        {
            return;
        }
        playerSfxSource.PlayOneShot(audioClips[src], volumn);
    }
    // dùng để play ngẫu nhiên 1 danh sách sfx tại ngay vị trí player
    public void PlaySFXs(string[] src=null, float volumn=1f)
    {
        if (src == null)
        {
            return;
        }
        playerSfxSource.PlayOneShot(audioClips[src[UnityEngine.Random.Range(0, src.Length)]], volumn);
    }
}
