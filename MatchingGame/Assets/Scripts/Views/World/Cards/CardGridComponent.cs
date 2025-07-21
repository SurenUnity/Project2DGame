using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Views.World.Cards
{
    public class CardGridComponent : MonoBehaviour
    {
        [SerializeField] private float paddingX = 1f;
        [SerializeField] private float paddingY = 1f;
        [SerializeField] private float _spacingX;
        [SerializeField] private float _spacingY;
        
        private Camera _mainCamera;

        public void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Grid(int columns, int rows, CardItemView[] cards)
        {
            var screenHeight = 2f * _mainCamera.orthographicSize;
            var screenWidth = screenHeight * _mainCamera.aspect;

            var totalWidth = columns * (1f + _spacingX) - _spacingX;
            var totalHeight = rows * (1f + _spacingY) - _spacingY;

            var availableWidth = screenWidth - (2 * paddingX);
            var availableHeight = screenHeight - (2 * paddingY);
            
            var cellWidth = availableWidth / totalWidth;
            var cellHeight = availableHeight / totalHeight;

            var scale = Mathf.Min(cellWidth, cellHeight);

            var gridWidth = columns * scale + (columns - 1) * _spacingX * scale;
            var gridHeight = rows * scale + (rows - 1) * _spacingY * scale;

            Vector2 origin = new Vector2(
                -gridWidth / 2f + scale / 2f,
                gridHeight / 2f - scale / 2f
            );

            var cardIndex = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (cardIndex >= cards.Length)
                        return;

                    Vector2 position = new Vector2(
                        origin.x + x * (scale + _spacingX * scale),
                        origin.y - y * (scale + _spacingY * scale)
                    );

                    var obj = cards[cardIndex];
                    obj.SetScale(new Vector3(scale, scale, 1));
                    obj.transform.position = position;

                    cardIndex++;
                }
            }
        }
    }
}