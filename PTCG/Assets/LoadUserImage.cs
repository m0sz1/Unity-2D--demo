using UnityEngine;
using SFB;
using System.IO;

public class LoadUserImage : MonoBehaviour
{
    public Renderer targetRenderer; // 拖 card 的 MeshRenderer 上来

    public void ChooseImage()
    {
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg")
        };

        string[] paths = StandaloneFileBrowser.OpenFilePanel("选择图片", "", extensions, false);

        if (paths.Length > 0)
        {
            ApplyImage(paths[0]);
        }
    }

    void ApplyImage(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);

        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(bytes);

        // ⭐ 关键：替换 shader Graph 的 _Texture2D
        targetRenderer.material.SetTexture("_Texture2D", tex);
    }
}
