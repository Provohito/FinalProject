using UnityEngine;

public enum projectTileType
{
    rock, arrow, fireball, mace, rock1, rrock, fireball1, thorns
};

public class ProjectTile : MonoBehaviour
{
    [SerializeField]
    int attackDamage;

    [SerializeField]
    projectTileType pType;

    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }
    }
    public projectTileType PType
    {
        get
        {
            return pType;
        }
    }


    }
