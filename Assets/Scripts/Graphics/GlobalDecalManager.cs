using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDecalManager : MonoBehaviour
{
    [SerializeField] private RenderTexture roomRenderTextureSettings;
    private RoomDecalHandler[] allRooms;

    private void Awake()
    {
        RenderTexture.active = roomRenderTextureSettings;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;
    }

    void Start()
    {
        allRooms = FindObjectsOfType<RoomDecalHandler>();
    }

    /// <param name="decalTexture">Decal texture</param>
    /// <param name="worldPosition">Decal world position</param>
    /// <param name="sizeInPixels">Decal size in pixels</param>
    /// <param name="rotationDegrees">Rotation in degrees for z-axis</param>
    public void RequestDecal(Texture2D decalTexture, Vector2 worldPosition, float sizeInPixels, float rotationDegrees)
    {
        foreach (RoomDecalHandler room in allRooms)
        {
            if (room.RoomBounds.Contains(worldPosition))
            {
                room.DrawDecalToThisRoom(decalTexture, worldPosition, sizeInPixels, rotationDegrees);
                return;
            }
        }
    }
}
