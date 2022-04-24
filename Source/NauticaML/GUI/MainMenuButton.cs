using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NauticaML.GUI
{
    public class MainMenuButton
    {
        private GameObject _buttonObj;
        private Button _button;
        private TMP_Text _text;
        private Action _action;

        public string Name { get => _buttonObj.name; set => _buttonObj.name = value; }

        public Action OnClick
        {
            get
            {
                return _action;
            }
            set
            {
                _button.onClick.SetPersistentListenerState(0, UnityEventCallState.Off);
                _button.onClick.RemoveAllListeners();

                if (value != null)
                {
                    _button.onClick.AddListener(new UnityAction(value));
                    _action = value;
                }
            }
        }

        public void Delete()
        {
            GameObject.DestroyImmediate(_buttonObj);
            _buttonObj = null;
            _button = null;
            _text = null;
            _action = null;
        }

        protected MainMenuButton(GameObject gobj, string text, Action onClick = null)
        {
            _buttonObj = gobj;
            _button = gobj.GetComponent<Button>();
            _text = gobj.GetComponentInChildren<TMP_Text>();

            if (_button == null)
                throw new NullReferenceException($"Cannot retrive the menu button in '{gobj.name}'");
            if (_text == null)
                throw new NullReferenceException($"Cannot retrive the menu button text in '{gobj.name}'");

            _text.text = text ?? string.Empty;

            var translationComponent = _buttonObj.GetComponentInChildren<TranslationLiveUpdate>();
            if (translationComponent != null)
                translationComponent.translationKey = text;

            // initialize the button event
            OnClick = onClick;
        }

        /// <summary>
        /// Create a new menu button on the main menu, text can be string or a translation string in language file
        /// </summary>
        /// <param name="actionName">The action name, used for the naming the GameObject</param>
        /// <param name="text">the text string or the translation key</param>
        /// <param name="onClick">onClick <see cref="Action"/> Callback</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException">'ButtonPlay' GameObject is not found</exception>
        public static MainMenuButton CreateNew(string actionName, string text, int buttonIndex = 0, Action onClick = null)
        {
            // clone an existing one
            var playBtnInstance = GameObject.Find("ButtonPlay");

            if (playBtnInstance == null)
                throw new NullReferenceException($"'ButtonPlay' GameObject not found in {SceneManager.GetActiveScene().name}");

            var newBtnInstance = GameObject.Instantiate(playBtnInstance);
            newBtnInstance.name = $"Button{actionName}";

            newBtnInstance.transform.position = new Vector3(-0.4f, -0.5f, 1.0f);
            newBtnInstance.transform.SetParent(playBtnInstance.transform.parent);
            newBtnInstance.transform.localScale = Vector3.one;

            newBtnInstance.transform.SetSiblingIndex(buttonIndex);

            // create the main menu button management class
            return new MainMenuButton(newBtnInstance, text, onClick);
        }
    }
}
