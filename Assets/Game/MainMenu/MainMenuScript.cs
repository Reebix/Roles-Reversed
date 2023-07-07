using SharpUI.Source.Common.UI.Elements.Button;
using SharpUI.Source.Common.Util.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenuScript : MonoBehaviour
    {
        [SerializeField] private RectButton playButton;
        [SerializeField] private RectButton toMetaProgressionButton;
        [SerializeField] private RectButton toMainMenuButton;
        [SerializeField] private GameObject fadeCircle;
        
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject metaProgressionPanel;
        
        private int _wantedPanelOffset = 0;
        private int _currentPanelOffset = 0;

        private void Awake()
        {
            fadeCircle.SetActive(false);
            
        }
        
        private void Start()
        {
            playButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnPlayButtonClicked);
            toMetaProgressionButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnToMetaProgressionButtonClicked);
            toMainMenuButton.GetEventListener().ObserveOnClicked().SubscribeWith(this, OnToMainMenuButtonClicked);
        }

        private void OnToMainMenuButtonClicked(Unit obj)
        {
            _wantedPanelOffset = 0;
        }

        private void OnToMetaProgressionButtonClicked(Unit obj)
        {
            _wantedPanelOffset = -1920;
        }

        private void Update()
        {
            if (fadeCircle.activeSelf)
            {
                fadeCircle.transform.localScale += Vector3.one * (Time.deltaTime * 2);
                if (fadeCircle.transform.localScale.x > 1f) SceneManager.LoadScene("Game/MainScene");
            }
            
            if (_currentPanelOffset != _wantedPanelOffset)
            {
                _currentPanelOffset = (int)Mathf.Lerp(_currentPanelOffset, _wantedPanelOffset, Time.deltaTime * 10);
                if (Mathf.Abs(_currentPanelOffset - _wantedPanelOffset) <= 10)
                {
                    _currentPanelOffset = _wantedPanelOffset;
                }
    
                mainMenuPanel.transform.localPosition = new Vector3(_currentPanelOffset, 0, 0);
                metaProgressionPanel.transform.localPosition = new Vector3(_currentPanelOffset + 1920, 0, 0);
            }
        }

        private void OnPlayButtonClicked(Unit obj)
        {
            fadeCircle.SetActive(true);
        }
    }
}