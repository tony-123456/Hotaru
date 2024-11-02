using UnityEngine.Video;
using UnityEngine;

public class End : MonoBehaviour
{
    public VideoPlayer vp;
    public GameObject EndView;

    private void Start()
    {
        vp.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Produce.mp4");
        vp.Play();
        ToEndVideo();
    }
    void ToEndVideo()
    {
        vp.loopPointReached += EndWithVideoPlay;
    }

    /// <summary>
    /// 播放结束逻辑
    /// </summary>
    /// <param name="thisPlay"></param>
    void EndWithVideoPlay(VideoPlayer thisPlay)
    {
        EndView.SetActive(true);
    }

}
