using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    public int[] scene_bgm;
    [SerializeField] private int MainMenuBGM;
    [SerializeField] private float fruitSFX_time;
    [SerializeField] private float[] sfx_pitchNum;
    private int sfx_pitchIndex = 0;
    private float sfx_timer;
    private int bgmToPlay = -1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        PlayBGM(scene_bgm[GameManager.instance.levelNumber]);
    }

    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].Play();
        }
    }
    public void PlayBGM(int _bgmToPlay)
    {
        if (_bgmToPlay == bgmToPlay)
            return;

        for (int i = 0; i < bgm.Length;  i++)
        {
            bgm[i].Stop();
        }
        bgm[_bgmToPlay].Play();
        bgmToPlay = _bgmToPlay;
    }
    public void StopSFX(int sfxToPlay)
    {
        if (sfxToPlay < sfx.Length)
        {
            sfx[sfxToPlay].Stop();
        }
    }

    public void PlaySFX_Fruit()
    {
        if (sfx_timer > 0)
        {
            sfx[8].pitch = sfx_pitchNum[sfx_pitchIndex];
            if (sfx_pitchIndex + 1 < sfx_pitchNum.Length)
                sfx_pitchIndex++;
        }
        else
        {
            sfx_pitchIndex = 0;
            sfx[8].pitch = sfx_pitchNum[sfx_pitchIndex];
        }
        sfx_timer = fruitSFX_time;
        PlaySFX(8);
    }

    private void Update()
    {
        sfx_timer -= Time.deltaTime;
    }
}
