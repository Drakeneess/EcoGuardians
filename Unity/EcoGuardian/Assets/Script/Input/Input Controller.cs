using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance { get; private set; } // Singleton instance

    private static Actions inputs;

    private void Awake()
    {
        // Verifica si ya existe una instancia de InputController
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Elimina cualquier instancia duplicada
        }
        else
        {
            Instance = this; // Asigna esta instancia como la instancia principal
            DontDestroyOnLoad(gameObject); // Evita que se destruya al cargar nuevas escenas
        }
    }

    void Start()
    {
        inputs = new Actions();
        inputs.Game.Enable();
    }

    void Update()
    {
        // Opcional: Aquí puedes agregar lógica adicional si se necesita en cada frame
    }

    public static float RightAction()
    {
        return inputs.Game.RightTrigger.ReadValue<float>();
    }
    public static float LeftAction()
    {
        return inputs.Game.LeftTrigger.ReadValue<float>();
    }
    public static float ButtonA(){
        return inputs.Game.AButton.ReadValue<float>();
    }
    public static float ButtonB(){
        return inputs.Game.BButton.ReadValue<float>();
    }
    public static float ButtonX(){
        return inputs.Game.XButton.ReadValue<float>();
    }
    public static float ButtonY(){
        return inputs.Game.YButton.ReadValue<float>();
    }
}
