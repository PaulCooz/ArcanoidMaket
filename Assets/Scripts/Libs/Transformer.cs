using UnityEngine;

namespace Libs
{
    public static class Transformer
    {
        public static Vector2 Position(float byWidth, float byHeight, Camera mainCamera)
        {
            if (mainCamera == null)
            {
                Debug.LogWarning("can't set position (camera = null)");
                return Vector2.zero;
            }

            return mainCamera.ScreenToWorldPoint(new Vector3(Screen.width * byWidth, Screen.height * byHeight, 0));
        }

        public static Vector2 Scale(float byWidth, float byHeight, Camera mainCamera, SpriteRenderer spriteRenderer)
        {
            if (spriteRenderer == null || mainCamera == null)
            {
                Debug.LogWarning("can't set sprite size (sprite render or camera = null)");
                return Vector2.zero;
            }

            var sprite = spriteRenderer.sprite;
            var size = sprite.textureRect.size / sprite.pixelsPerUnit;

            var screenUnitsHeight = 2 * mainCamera.orthographicSize;
            var screenUnitsWidth = screenUnitsHeight / Screen.height * Screen.width;

            return new Vector2(screenUnitsWidth * byWidth / size.x, screenUnitsHeight * byHeight / size.y);
        }
    }
}