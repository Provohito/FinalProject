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

        public void Generate()
        {         
            var _tile = Instantiate(tilePrefab, rootTr);   
        }
    }
}
