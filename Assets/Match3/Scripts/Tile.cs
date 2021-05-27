using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class Tile : MonoBehaviour
    {
        const float HALF_SIZE = 0.5f;

        SpriteRenderer spriteR;

        [SerializeField]
        Color[] colors;


        private void Awake()
        {
            spriteR = GetComponent<SpriteRenderer>();
        }
        public void SetPosition(Vector2 _position)
        {
            Position = _position;

            transform.localPosition = new Vector3(_position.x + HALF_SIZE, -(_position.y + HALF_SIZE));

            int _colorIndex = (int)Mathf.Repeat(_position.x + _position.y, 2);
            spriteR.color = colors[_colorIndex];
        }

        public Vector2 Position { get; private set; }
    }
}
