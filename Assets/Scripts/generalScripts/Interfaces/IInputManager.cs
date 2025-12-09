using System;

namespace generalScripts.Interfaces
{
    public interface IInputManager
    {
        float ThrottleInput { get; }      // W/S: -1 to +1 (forward/backward)
        float SteerInput { get; }         // A/D: -1 to +1 (left/right)
        UnityEngine.Vector2 AimPosition { get; } // Mouse position in screen space

        bool IsAttacking { get; }         // LMB held
        bool IsSpecialAttacking { get; }  // RMB held
        bool IsBoosting { get; }          // Shift held

        event Action OnAttackPressed;
        event Action OnAttackReleased;
        event Action OnSpecialAttackPressed;
        event Action OnSpecialAttackReleased;
        event Action OnPausePressed;
        event Action OnBoostPressed;
        event Action OnBoostReleased;

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
