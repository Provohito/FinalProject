using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum gameStatus
    {
    next, play, gameover, win
    }

public class GameManager : Loader<GameManager>
{
    [SerializeField]
    int totalWaves = 10;
    [SerializeField]
    Text totalMoneyLabel;// Now money
    [SerializeField]
    Text currentWave;
    [SerializeField]
    Text playBtnLabel;
    [SerializeField]
    Text totalEscapedLabel;
    [SerializeField]
    Button playBtn;


    GameObject spawnPoint;
    [SerializeField]
    Enemy[] enemies;
    [SerializeField]
    int totalEnemies;
    [SerializeField]
    int enemiesPerSpawn;

    [SerializeField]
    GameObject towersBtns;

    int waveNumber = 0;
    int totalMoney;
    int totalEscaped = 0;
    int roundEscaped = 0;
    int totalKilled = 0;
    int whichEnemiesToSpawn = 0;
    int enemiesToSpawn = 0;
    gameStatus currentState = gameStatus.play;
    AudioSource audioSource;
    private GameObject[] FreeProjectTiles;

    public int TotalEscaped
    {
        get
            {
                return totalEscaped;
            }
        set
            {
                totalEscaped = value;
            }
    }
    public int RoundEscaped
    {
        get
            {
                return roundEscaped;
            }
        set
            {
                roundEscaped = value;
            }
    }
    public int TotalKilled
    {
        get
            {
                return totalKilled;
            }
        set
            {
                totalKilled = value;
            }
    }

    public int TotalMoney
    {
        get
            {
                return totalMoney;
            }
         set
            {
                totalMoney = value;
                totalMoneyLabel.text = TotalMoney.ToString();
            }
    
    }

    public AudioSource AudioSource
    {
        get
        {
            return audioSource;
        }
    }

    public List<Enemy> EnemyList = new List<Enemy>();

    const float spawnDelay = 0.5f;
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn");
        playBtn.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        ShowMenu();
    }
    private void Update()
    {
        HandleEscape();
    }

    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < totalEnemies)
                {
                    Enemy newEnemy = Instantiate(enemies[Random.Range(0, enemiesToSpawn)]) as Enemy;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    } 

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }


    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyEnemies()
    {
        foreach(Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }

    public void addMoney(int amount)
    {
        TotalMoney += amount;
    }

    public void substractMoney(int amount)
    {
        if (TotalMoney < amount)
        {
            TowerManagerScript.Instance.DisabledDrag();
            TowerManagerScript.Instance.towerButtonPressed = null;
        }
        else
            TotalMoney -= amount;

    }

    public void IsWaveOver()
    {
        totalEscapedLabel.text = "Escaped " + TotalEscaped + "/ 10";

        if ((RoundEscaped + TotalKilled) == totalEnemies)
	    {
            if (waveNumber <= enemies.Length)
            {
                enemiesToSpawn = waveNumber;
            }
            SetCurrentGameState();
            ShowMenu();
	    }
    }

    public void SetCurrentGameState()
    {
        if (totalEscaped >= 10)
	    {
            currentState = gameStatus.gameover;
	    }
        else if (waveNumber == 0 && (RoundEscaped + TotalKilled) == 0)
        {
            currentState = gameStatus.play;
        }
        else if (waveNumber >= totalWaves)
        {
            currentState = gameStatus.win;
        }
        else
        {
            currentState = gameStatus.next;
        }
    }

    public void PlayButtonPressed()
    {
        switch(currentState)
        {
            case gameStatus.next:
                waveNumber += 1;
                totalEnemies += 1;
                DestroyAllProjectTile();
                break;
            default:
                totalEnemies = 1;
                TotalEscaped = 0;
                
                TotalMoney = 20;
                enemiesToSpawn = 0;
                TowerManagerScript.Instance.DestroyAllTowers();
                TowerManagerScript.Instance.RenameBuildSite();
                totalMoneyLabel.text = TotalMoney.ToString();
                totalEscapedLabel.text = "Escaped " + totalEscaped + " / 10";
                audioSource.PlayOneShot(SoundManager.Instance.NewGame);
                break;
        }
        DestroyEnemies();
        TotalKilled = 0;
        RoundEscaped = 0;
        currentWave.text = "Wave " + (waveNumber +1);
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);
    }

    public void DestroyAllProjectTile()
    {

        FreeProjectTiles = GameObject.FindGameObjectsWithTag("ProjectTile");
        foreach (GameObject freePrTiles in FreeProjectTiles)
        {
            Destroy(freePrTiles);
        }
             
        
    }

    public void ShowTowers()
    {
        if (towersBtns.activeSelf == true)
            towersBtns.SetActive(false);
        else
            towersBtns.SetActive(true);
    }

    public void ShowMenu()
    {
        switch(currentState)
        {
            case gameStatus.gameover:
                playBtnLabel.text = "U Lose. Play Again";
                AudioSource.PlayOneShot(SoundManager.Instance.GameOver);
                break;

            case gameStatus.next:
                playBtnLabel.text = "Next wave";
                break;

            case gameStatus.play:
                playBtnLabel.text = "Play game";
                break;

            case gameStatus.win:
                playBtnLabel.text = "U win this!";
                break;
        }
        playBtn.gameObject.SetActive(true);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
	{
            TowerManagerScript.Instance.DisabledDrag();
            TowerManagerScript.Instance.towerButtonPressed = null;
	}
    }

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject); // подумать наж этим
    }
}
