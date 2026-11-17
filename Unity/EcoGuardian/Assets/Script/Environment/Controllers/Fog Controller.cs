using UnityEngine;

public class FogController : MonoBehaviour
{
    public Color colorFog;
    public float densityFog;
    public FogMode fogMode;
    public float startDistanceFog;
    public float endDistanceFog;

    void Update()
    {
        // Activar el fog
        RenderSettings.fog = true;
        // Establecer el color del fog
        RenderSettings.fogColor = colorFog;
        // Establecer el modo del fog
        RenderSettings.fogMode = fogMode;
        RenderSettings.fogDensity=densityFog;
        // Establecer la distancia de inicio y fin del fog
        RenderSettings.fogStartDistance = startDistanceFog;
        RenderSettings.fogEndDistance = endDistanceFog;
    }
}
