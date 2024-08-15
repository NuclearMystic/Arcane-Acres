using UnityEngine;

public class RenderTextureToScreen : MonoBehaviour
{
    public RenderTexture renderTexture;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (renderTexture != null)
        {
            Graphics.Blit(renderTexture, dest);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
