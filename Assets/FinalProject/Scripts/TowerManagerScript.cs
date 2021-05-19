using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManagerScript : Loader<TowerManagerScript>
{

   public TowerButton towerButtonPressed{get; set;}

    SpriteRenderer spriteRenderer;

    private List<TowerControll> TowerList = new List<TowerControll>();
    private List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();
        spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "towerSite" && GameManager.Instance.TotalMoney >= towerButtonPressed.TowerPrice) 
            {
                buildTile = hit.collider;
                buildTile.tag = "TowerSideFull";
                RegisterBuildSite(buildTile);
                PlaceTower(hit);
            }

           
        }
        if (spriteRenderer.enabled)
        {
            FollowMouse();
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }
    public void RegisterTower(TowerControll tower)
    {
        TowerList.Add(tower);
    }
    public void RenameBuildSite()
    {
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "towerSite";
        }
        BuildList.Clear();
    }

    public void DestroyAllTowers()
    {
        foreach (TowerControll tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }
    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerButtonPressed != null) 
        {
            TowerControll newTower = Instantiate(towerButtonPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            BuyTower(towerButtonPressed.TowerPrice);
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuild);
            RegisterTower(newTower);
            DisabledDrag();
        }
    }

    public void BuyTower(int price)
    {
        GameManager.Instance.substractMoney(price);
    }

    public void SelectedTower(TowerButton towerSelected)
    {
        if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            towerButtonPressed = towerSelected;
            EnableDrag(towerButtonPressed.DragSprite);
        }
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnableDrag(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisabledDrag()
    {
        spriteRenderer.enabled = false;
    }
}
