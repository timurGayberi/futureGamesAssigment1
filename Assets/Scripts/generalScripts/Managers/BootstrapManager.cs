using UnityEngine;
using UnityEngine.SceneManagement;

namespace generalScripts.Managers
{
    public enum SceneIndex
    {
        MainMenu = 1,
        GameScene = 2
    }

    public class BootstrapManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private SceneIndex firstScene = SceneIndex.MainMenu;

        [SerializeField] private bool autoLoadFirstScene = true;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {

            var servicesValid = ValidateServices();

            if (!servicesValid)
            {
                return;
            }

            if (autoLoadFirstScene)
            {
                LoadFirstScene();
            }
        }

        private bool ValidateServices()
        {
            var allValid = true;

            if (ServiceLocator.IsServiceRegistered<Interfaces.IApplicationManager>())
            {
                Debug.Log("[Bootstrap] ✓ IApplicationManager registered");
            }
            else
            {
                Debug.LogError("[Bootstrap] ✗ IApplicationManager NOT registered!");
                allValid = false;
            }

            if (ServiceLocator.IsServiceRegistered<Interfaces.IDataManager>())
            {
                Debug.Log("[Bootstrap] ✓ IDataManager registered");
            }
            else
            {
                Debug.LogError("[Bootstrap] ✗ IDataManager NOT registered!");
                allValid = false;
            }

            if (ServiceLocator.IsServiceRegistered<Interfaces.IGameplayManager>())
            {
                Debug.Log("[Bootstrap] ✓ IGameplayManager registered");
            }
            else
            {
                Debug.LogWarning("[Bootstrap] ⚠ IGameplayManager not registered (OK if not in gameplay scene)");
            }

            return allValid;
        }

        private void LoadFirstScene()
        {
            var sceneIndex = (int)firstScene;
            SceneManager.LoadScene(sceneIndex);
        }


        [ContextMenu("Reload Bootstrap Scene")]
        private void ReloadBootstrap()
        {
            SceneManager.LoadScene(0);
        }
    }
}
