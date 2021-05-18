using UnityEngine;

public class SoundManager : Loader  <SoundManager>
{
    [SerializeField]
    AudioClip arrow;
    [SerializeField]
    AudioClip death;
    [SerializeField]
    AudioClip fireBall;
    [SerializeField]
    AudioClip gameOver;
    [SerializeField]
    AudioClip hit;
    [SerializeField]
    AudioClip level;
    [SerializeField]
    AudioClip newGame;
    [SerializeField]
    AudioClip rock;
    [SerializeField]
    AudioClip towerBuild;

    public AudioClip Arrow
    {
        get
        {
            return arrow;
        }
    }
    public AudioClip Hit
    {
        get
        {
            return hit;
        }
    }
    public AudioClip Death
    {
        get
        {
            return death;
        }
    }
    public AudioClip FireBall
    {
        get
        {
            return fireBall;
        }
    }
    public AudioClip GameOver
    {
        get
        {
            return gameOver;
        }
    }
    public AudioClip Level
    {
        get
        {
            return level;
        }
    }
    public AudioClip NewGame
    {
        get
        {
            return newGame;
        }
    }
    public AudioClip Rock
    {
        get
        {
            return rock;
        }
    }
    public AudioClip TowerBuild
    {
        get
        {
            return towerBuild;
        }
    }
}
