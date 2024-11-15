//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.8.1
//     from Assets/Input/Actions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

public partial class @Actions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Actions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Actions"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""b4564587-a4b4-4be7-9037-cf3acec318eb"",
            ""actions"": [
                {
                    ""name"": ""Right Trigger"",
                    ""type"": ""Button"",
                    ""id"": ""675542cc-e746-4ab0-baaa-c4869630af34"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Left Trigger"",
                    ""type"": ""Button"",
                    ""id"": ""aad3f883-d83a-4f88-9dae-73d90be5e0a8"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AButton"",
                    ""type"": ""Button"",
                    ""id"": ""ca4a9e99-8291-4629-99cd-e1958e420311"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""BButton"",
                    ""type"": ""Button"",
                    ""id"": ""0081a22a-4bec-440b-9c0b-6894c83ad110"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""YButton"",
                    ""type"": ""Button"",
                    ""id"": ""8297b254-8d8b-470b-b628-c61063f5adac"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""XButton"",
                    ""type"": ""Button"",
                    ""id"": ""9093e133-9e2a-4476-93a8-43936dc77a44"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2792945a-1a93-4984-84a6-f0c7fa15d9c6"",
                    ""path"": ""<XRController>{RightHand}/{TriggerButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfcb7478-3027-48ef-81a2-72a0a5e7c06e"",
                    ""path"": ""<XRController>{LeftHand}/{TriggerButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left Trigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0e27c8d-b27b-4e7e-b4ef-b1f571638294"",
                    ""path"": ""<XRController>{RightHand}/{PrimaryButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97ee2e45-c464-406d-b30f-76cbc0d2c75d"",
                    ""path"": ""<XRController>{RightHand}/{SecondaryButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c46c90ef-6b03-4cda-a25b-f600722a40b8"",
                    ""path"": ""<XRController>{LeftHand}/{SecondaryButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""YButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c710e208-c03f-4042-b861-d95021494ee0"",
                    ""path"": ""<XRController>{LeftHand}/{PrimaryButton}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_RightTrigger = m_Game.FindAction("Right Trigger", throwIfNotFound: true);
        m_Game_LeftTrigger = m_Game.FindAction("Left Trigger", throwIfNotFound: true);
        m_Game_AButton = m_Game.FindAction("AButton", throwIfNotFound: true);
        m_Game_BButton = m_Game.FindAction("BButton", throwIfNotFound: true);
        m_Game_YButton = m_Game.FindAction("YButton", throwIfNotFound: true);
        m_Game_XButton = m_Game.FindAction("XButton", throwIfNotFound: true);
    }

    ~@Actions()
    {
        Debug.Assert(!m_Game.enabled, "This will cause a leak and performance issues, Actions.Game.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Game
    private readonly InputActionMap m_Game;
    private List<IGameActions> m_GameActionsCallbackInterfaces = new List<IGameActions>();
    private readonly InputAction m_Game_RightTrigger;
    private readonly InputAction m_Game_LeftTrigger;
    private readonly InputAction m_Game_AButton;
    private readonly InputAction m_Game_BButton;
    private readonly InputAction m_Game_YButton;
    private readonly InputAction m_Game_XButton;
    public struct GameActions
    {
        private @Actions m_Wrapper;
        public GameActions(@Actions wrapper) { m_Wrapper = wrapper; }
        public InputAction @RightTrigger => m_Wrapper.m_Game_RightTrigger;
        public InputAction @LeftTrigger => m_Wrapper.m_Game_LeftTrigger;
        public InputAction @AButton => m_Wrapper.m_Game_AButton;
        public InputAction @BButton => m_Wrapper.m_Game_BButton;
        public InputAction @YButton => m_Wrapper.m_Game_YButton;
        public InputAction @XButton => m_Wrapper.m_Game_XButton;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void AddCallbacks(IGameActions instance)
        {
            if (instance == null || m_Wrapper.m_GameActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameActionsCallbackInterfaces.Add(instance);
            @RightTrigger.started += instance.OnRightTrigger;
            @RightTrigger.performed += instance.OnRightTrigger;
            @RightTrigger.canceled += instance.OnRightTrigger;
            @LeftTrigger.started += instance.OnLeftTrigger;
            @LeftTrigger.performed += instance.OnLeftTrigger;
            @LeftTrigger.canceled += instance.OnLeftTrigger;
            @AButton.started += instance.OnAButton;
            @AButton.performed += instance.OnAButton;
            @AButton.canceled += instance.OnAButton;
            @BButton.started += instance.OnBButton;
            @BButton.performed += instance.OnBButton;
            @BButton.canceled += instance.OnBButton;
            @YButton.started += instance.OnYButton;
            @YButton.performed += instance.OnYButton;
            @YButton.canceled += instance.OnYButton;
            @XButton.started += instance.OnXButton;
            @XButton.performed += instance.OnXButton;
            @XButton.canceled += instance.OnXButton;
        }

        private void UnregisterCallbacks(IGameActions instance)
        {
            @RightTrigger.started -= instance.OnRightTrigger;
            @RightTrigger.performed -= instance.OnRightTrigger;
            @RightTrigger.canceled -= instance.OnRightTrigger;
            @LeftTrigger.started -= instance.OnLeftTrigger;
            @LeftTrigger.performed -= instance.OnLeftTrigger;
            @LeftTrigger.canceled -= instance.OnLeftTrigger;
            @AButton.started -= instance.OnAButton;
            @AButton.performed -= instance.OnAButton;
            @AButton.canceled -= instance.OnAButton;
            @BButton.started -= instance.OnBButton;
            @BButton.performed -= instance.OnBButton;
            @BButton.canceled -= instance.OnBButton;
            @YButton.started -= instance.OnYButton;
            @YButton.performed -= instance.OnYButton;
            @YButton.canceled -= instance.OnYButton;
            @XButton.started -= instance.OnXButton;
            @XButton.performed -= instance.OnXButton;
            @XButton.canceled -= instance.OnXButton;
        }

        public void RemoveCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameActions instance)
        {
            foreach (var item in m_Wrapper.m_GameActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameActions @Game => new GameActions(this);
    public interface IGameActions
    {
        void OnRightTrigger(InputAction.CallbackContext context);
        void OnLeftTrigger(InputAction.CallbackContext context);
        void OnAButton(InputAction.CallbackContext context);
        void OnBButton(InputAction.CallbackContext context);
        void OnYButton(InputAction.CallbackContext context);
        void OnXButton(InputAction.CallbackContext context);
    }
}
