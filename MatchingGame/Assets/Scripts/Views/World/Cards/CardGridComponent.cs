using System;
using System.Collections.Generic;
using UnityEngine;

namespace Views.World.Cards
{
    public class CardGridComponent : MonoBehaviour
    {
        [SerializeField] private float paddingX = 1f;
        [SerializeField] private float paddingY = 1f;
        [SerializeField] private float _spacing;
        
        private Camera _mainCamera;

        public void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void Grid(int columns, int rows, CardItemView[] cards)
        {
            var screenHeight = 2f * _mainCamera.orthographicSize;
            var screenWidth = screenHeight * _mainCamera.aspect;
            
            var availableWidth = screenWidth - (2 * paddingX);
            var availableHeight = screenHeight - (2 * paddingY);
            
            var cellWidth = availableWidth / columns;
            var cellHeight = availableHeight / rows;
            
            Vector2 origin = new Vector2(
                -screenWidth / 2f + paddingX + cellWidth / 2f,
                screenHeight / 2f - paddingY - cellHeight / 2f
            );

            var cardIndex = 0;
            
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Vector2 position = new Vector2(
                        origin.x + x * (cellWidth + _spacing),
                        origin.y - y * (cellHeight + _spacing)
                    );

                    var obj = cards[cardIndex];
                    
                    var scale = Mathf.Min(cellWidth, cellHeight);
                    obj.transform.localScale = new Vector3(scale, scale, 1);
                    obj.transform.position = position;
                    
                    cardIndex++;
                }
            }
        }
    }
}