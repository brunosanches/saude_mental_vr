using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class JsonDownloader : MonoBehaviour
{
    // URL do arquivo JSON a ser baixado
    public string jsonUrl = "https://raw.githubusercontent.com/brunosanches/saude_mental_vr/main/Assets/Resources/mock.json";
    //public string jsonUrl = "C:\\Users\\bruno\\My project (3)\\Assets\\Resources\\mock_2.json";

    // Callback para lidar com o TextAsset baixado
    public System.Action<TextAsset> onComplete;

    // Função para iniciar o download
    public void StartDownload()
    {
        StartCoroutine(DownloadJson());
    }

    IEnumerator DownloadJson()
    {
        UnityWebRequest www = UnityWebRequest.Get(jsonUrl);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro ao baixar o arquivo JSON: " + www.error);
        }
        else
        {
            // Cria um TextAsset a partir do conteúdo baixado
            TextAsset textAsset = new TextAsset(www.downloadHandler.text);

            // Chama o callback com o TextAsset
            if (onComplete != null)
                onComplete.Invoke(textAsset);

            Debug.Log("Arquivo JSON baixado como TextAsset");

            // Importante: o TextAsset não precisa ser destruído manualmente,
            // o Unity cuidará disso automaticamente.
        }
    }
}
