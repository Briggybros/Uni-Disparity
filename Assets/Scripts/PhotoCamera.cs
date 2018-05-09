using UnityEngine;
using System.Collections;

public class PhotoCamera : MonoBehaviour
{
  public int resWidth = 3840, resHeight = 2160;

  private bool takeHiResShot = false;
  private Camera c;

  private void Start()
  {
    c = GetComponent<Camera>();
  }

  public static string ScreenShotName(int width, int height)
  {
    return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png",
                         Application.dataPath,
                         width, height,
                         System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
  }

  public void TakeHiResShot()
  {
    takeHiResShot = true;
  }

  void LateUpdate()
  {
    takeHiResShot |= Input.GetKeyDown("k");
    if (takeHiResShot)
    {
      RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
      c.targetTexture = rt;
      Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
      c.Render();
      RenderTexture.active = rt;
      screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
      c.targetTexture = null;
      RenderTexture.active = null;
      Destroy(rt);
      byte[] bytes = screenShot.EncodeToPNG();
      string filename = ScreenShotName(resWidth, resHeight);
      System.IO.File.WriteAllBytes(filename, bytes);
      Debug.Log(string.Format("Took screenshot to: {0}", filename));
      takeHiResShot = false;
    }
  }
}
