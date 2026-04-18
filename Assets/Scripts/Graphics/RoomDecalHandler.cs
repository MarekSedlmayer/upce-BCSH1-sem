using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomDecalHandler : MonoBehaviour
{
    [SerializeField] private RenderTexture roomRenderTextureSettings;
    [SerializeField] private Material blendMaterial;

    [Tooltip("Size of floor (Unity units)")]
    public Vector2 roomWorldSize = new Vector2(16f, 12f);
    public Bounds RoomBounds => new Bounds(new Vector3(transform.position.x, transform.position.y, 0), roomWorldSize);

    private RenderTexture _roomRenderTexture;

    void Awake()
    {
        _roomRenderTexture = new RenderTexture(roomRenderTextureSettings);
        GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", _roomRenderTexture);
    }

    void OnDestroy()
    {
        RenderTexture.active = _roomRenderTexture;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = null;
    }

    /// <param name="decalTexture">Decal texture</param>
    /// <param name="worldPosition">Decal world position</param>
    /// <param name="sizeInPixels">Decal size in pixels</param>
    /// <param name="rotationDegrees">Rotation in degrees for z-axis</param>
    public void DrawDecalToThisRoom(Texture2D decalTexture, Vector2 worldPosition, float sizeInPixels, float rotationDegrees)
    {
        float normalizedX = ((worldPosition.x - transform.position.x) / roomWorldSize.x) + 0.5f;
        float normalizedY = ((worldPosition.y - transform.position.y) / roomWorldSize.y) + 0.5f;

        float pixelX = normalizedX * _roomRenderTexture.width;
        float pixelY = _roomRenderTexture.height - (normalizedY * _roomRenderTexture.height);

        RenderTexture previousActive = RenderTexture.active;
        RenderTexture.active = _roomRenderTexture;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, _roomRenderTexture.width, _roomRenderTexture.height, 0);

        Matrix4x4 translationRotationMatrix = Matrix4x4.TRS(
            new Vector3(pixelX, pixelY, 0),
            Quaternion.Euler(0, 0, rotationDegrees),
            Vector3.one
        );

        GL.MultMatrix(translationRotationMatrix);

        Rect drawRect = new Rect(
            -(sizeInPixels / 2f),
            -(sizeInPixels / 2f),
            sizeInPixels,
            sizeInPixels
        );

        Graphics.DrawTexture(drawRect, decalTexture, blendMaterial);

        GL.PopMatrix();
        RenderTexture.active = previousActive;
    }
}
