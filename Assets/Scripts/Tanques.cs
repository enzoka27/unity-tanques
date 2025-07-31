using UnityEngine;

[System.Serializable]
public class Tanque
{
    public string Nome;
    public float Altura;
    public float Raio;
    public float Altura_X;
}

[System.Serializable]
public class TanqueList
{
    public Tanque[] tanques;
}