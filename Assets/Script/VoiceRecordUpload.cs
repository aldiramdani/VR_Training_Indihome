using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.IO;
using DBXSync;
using UnityEngine.SceneManagement;

public class VoiceRecordUpload : MonoBehaviour
{
    private string session_nik = Profilling.session_nik;
    private string namaFile;
    Scene m_sceneName;
    string currentScene;
    public Text statusText;
    public Button btn_Home,btn_upload;
    BasicAudio bs = new BasicAudio();
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private string _uploadDropboxPath;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setNamaFile()
    {
        namaFile = session_nik + PlayerPrefs.GetString("Nama" + session_nik)+ System.DateTime.Now.ToString("yyyyMMddhhmmss");
        PlayerPrefs.SetString("NamaFileRecord", namaFile);
    }

    public void stopRecording()
    {
        string namaRecord = CheckFileName(PlayerPrefs.GetString("NamaFileRecord"));
        PlayerPrefs.SetString("namaRecod", namaRecord);
        RARE.Instance.StopMicRecording(namaRecord);        
    }

    public string CheckFileName(string input)
    {
        if (File.Exists(Application.persistentDataPath + "/" + input + " (" + 1 + ").wav"))
        {
            int x = 2;
            while (File.Exists(Application.persistentDataPath + "/" + input + " (" + x + ").wav"))
            {
                x++;
            }
            return input + " (" + x + ")";
        }
        else if (File.Exists(Application.persistentDataPath + "/" + input + ".wav"))
        {
            return input + " (1)";
        }
        else
        {
            return input;
        }
    }

    public void uploadFile()
    {
        string namaRekaman = PlayerPrefs.GetString("namaRecod");
        _uploadDropboxPath = Path.Combine("/DropboxSyncExampleFolder/", Path.GetFileName(Application.persistentDataPath + "/" + namaRekaman + ".wav"));

        _cancellationTokenSource = new CancellationTokenSource();
        var localFilePath = Application.persistentDataPath + "/" + namaRekaman + ".wav";
        DropboxSync.Main.UploadFile(localFilePath, _uploadDropboxPath, new Progress<TransferProgressReport>((report) => {
            if (Application.isPlaying)
            {
                statusText.text = $"Uploading file {report.progress}% {report.bytesPerSecondFormatted}";
            }
        }), (metadata) => {
            // success			
            print($"Upload completed:\n{metadata}");
            statusText.text = $"<color=green>Uploaded. {metadata.id}</color>";
            btn_Home.gameObject.SetActive(true);
            btn_upload.gameObject.SetActive(false);
        }, (ex) => {
            // exception
            if (ex is OperationCanceledException)
            {
                Debug.Log("Upload cancelled");
                statusText.text = $"<color=orange>Upload canceled.</color>";
                btn_Home.gameObject.SetActive(false);
                btn_upload.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogException(ex);
                statusText.text = $"<color=red>Upload failed.</color>";
                btn_Home.gameObject.SetActive(false);
                btn_upload.gameObject.SetActive(true);
            }
        }, _cancellationTokenSource.Token);
        
    }

    public void startRecording()
    {
        RARE.Instance.StartMicRecording(360);
    }
}
