using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DBXSync;
using UnityEngine;
using UnityEngine.UI;

public class obb_Download : MonoBehaviour
{
    public Text status_Text,title_Text;
    public Button downloadBtn,mulaiBtn,cancelBtn;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    // Start is called before the first frame update
    void Start()
    {
     checkFile("tutorial_bg.png");   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void obbDownload(string url){
            _cancellationTokenSource = new CancellationTokenSource();
            DropboxSync.Main.GetFileAsLocalCachedPath(url, 
                                new Progress<TransferProgressReport>((report) => {                        
                                    status_Text.text = $"Downloading: {report.progress}% {report.bytesPerSecondFormatted}";
                                }),
                                (localPath) => {
                                    // success
                                    print($"Completed");
			                        status_Text.text = $"<color=green>Local path: {localPath}</color>";
                                    checkFile("tutorial_bg.png");   
                                },
                                (ex) => {
                                    // exception
                                    if(ex is OperationCanceledException){
                                        Debug.Log("Download cancelled");
                                        status_Text.text = $"<color=orange>Download canceled.</color>";
                                    }else{
                                        Debug.LogException(ex);
                                        status_Text.text = $"<color=red>Download failed.</color>";
                                    }
                                },
                                cancellationToken: _cancellationTokenSource.Token);
    }

    void checkFile(string fileName){
        string filePath = "/storage/emulated/0/Android/obb/" + Application.identifier + "/" + fileName;
        if(System.IO.File.Exists(filePath))
        {
            mulaiBtn.gameObject.SetActive(true);
            title_Text.text = "File yang diperlukan sudah diunduh silahkan memulai aplikasi";
            downloadBtn.gameObject.SetActive(false);
            cancelBtn.gameObject.SetActive(false);
        }else{
            downloadBtn.gameObject.SetActive(true);
            cancelBtn.gameObject.SetActive(true);
            title_Text.text = "Silahkan unduh file yang diperulkan terlebih dahulu";
            mulaiBtn.gameObject.SetActive(false);
        }
    }

    public void cancelDownload(){
        _cancellationTokenSource.Cancel();
    }
}
