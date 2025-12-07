using System;

namespace generalScripts.Interfaces
{
    /// <summary>
    /// Interface for managing player input across all platforms
    /// Supports keyboard, mouse, gamepad, and touch controls
    /// </summary>
    public interface IInputManager
    {
        // Input Values
        float ThrottleInput { get; }      // W/S: -1 to +1 (forward/backward)
        float SteerInput { get; }         // A/D: -1 to +1 (left/right)
        UnityEngine.Vector2 AimPosition { get; } // Mouse position in screen space

        // Button States
        bool IsAttacking { get; }         // LMB held
        bool IsSpecialAttacking { get; }  // RMB held

        // Events (for one-time actions)
        event Action OnAttackPressed;
        event Action OnAttackReleased;
        event Action OnSpecialAttackPressed;
        event Action OnSpecialAttackReleased;
        event Action OnPausePressed;

        // Platform Detection
        bool IsMobile { get; }
        string CurrentControlScheme { get; }

        // Control Management
        void EnableGameplayControls();
        void DisableGameplayControls();
        void EnableUIControls();
        void DisableUIControls();

        // Rebinding (for settings menu)
        void StartRebinding(string actionName, int bindingIndex, Action<bool> onComplete);
        void ResetBindingsToDefault();
        void SaveBindings();
        void LoadBindings();
    }
}
