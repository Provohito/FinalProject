using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class TileGrid : MonoBehaviour
    {
        [SerializeField]
        GameObject tilePrefab;
        [SerializeField]
        Transform rootTr;

        public void Generate(Vector2 _size)
        {
            for(int x = 0; x < _size.x; x ++)
            {
                for (int y = 0; y < _size.y; y++)
                {
                    var _tile = Instantiate(tilePrefab, rootTr).GetComponent<Tile>();

                    _tile.SetPosition(new Vector2(x,y));
                    
                }
            }

            rootTr.localPosition = new Vector3(- _size.x / 2, _size.y / 2);
        }
    }
}
