using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class TanqueManager : MonoBehaviour
{
    public GameObject cilindroPrefab;       // o tanque (casca)
    public GameObject liquidoPrefab;        // o líquido dentro
    private string url = "http://localhost:5000/tanques"; // Altere se for deploy

    void Start()
    {
        StartCoroutine(CarregarTanques());
    }

    IEnumerator CarregarTanques()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Erro na API: " + www.error);
        }
        else
        {
            string json = "{\"tanques\":" + www.downloadHandler.text + "}";
            TanqueList dados = JsonUtility.FromJson<TanqueList>(json);

            float offsetX = 0;

            foreach (Tanque tanque in dados.tanques)
            {
                Debug.Log(tanque.Altura);
                Debug.Log(tanque.Raio);
                // Instanciar o cilindro do tanque
                GameObject tanqueObj = Instantiate(cilindroPrefab);
                tanqueObj.transform.position = new Vector3(offsetX, tanque.Altura / 2f, 0);
                tanqueObj.transform.localScale = new Vector3(tanque.Raio * 2, tanque.Altura / 2f, tanque.Raio * 2);

                // Instanciar o líquido dentro
                //GameObject liquidoObj = Instantiate(liquidoPrefab);
                //liquidoObj.transform.position = new Vector3(offsetX, tanque.Altura_X / 2f, 0);
                //liquidoObj.transform.localScale = new Vector3(tanque.Raio * 1.9f, tanque.Altura_X / 2f, tanque.Raio * 1.9f);

                // Mover para o lado
                offsetX += tanque.Raio * 3f;
            }
        }
    }
}
