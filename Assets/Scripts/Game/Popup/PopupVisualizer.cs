namespace Game.Popup
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;

    public class PopupVisualizer : MonoBehaviour
    {
        public static PopupVisualizer Instance { get; private set; }

        public GameObject Popup;
        public GameObject CloseButton;
        public GameObject OkButton;
        public TextMeshPro CorrectText;
        public TextMeshPro WrongText;
        public GameObject Oriantation;

        private void Awake()
        {
            Instance = this;
            Oriantation.SetActive(false);
        }

        private void Start()
        {
            if (Popup != null)
            {
                Popup.SetActive(false);
            }
        }

        public void ActivePopup(int correct, int wrong)
        {
            CloseButton.SetActive(true);
            OkButton.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Doğru Sayısı : " + correct.ToString();
            WrongText.text = "Yanlış Sayısı : " + wrong.ToString();
            Oriantation.SetActive(false);
        }

        public void PassivePopup()
        {
            CloseButton.SetActive(true);
            OkButton.SetActive(true);
            Popup.SetActive(false);
            CorrectText.text = " ";
            WrongText.text = " ";
            Oriantation.SetActive(false);
        }

        public void ActiveHeart()
        {
            CloseButton.SetActive(true);
            OkButton.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Canınız Yetersizdir, Bir önceki Seviyeyi Tekrar Etmek Zorundasınız.";
            WrongText.text = " ";
            Oriantation.SetActive(false);
        }


        public void ActiveTimer()
        {
            CloseButton.SetActive(true);
            OkButton.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Süreniz Bitmiştir, Bir önceki Seviyeyi Tekrar Etmek Zorundasınız.";
            WrongText.text = " ";
            Oriantation.SetActive(false);
        }

        public void ActiveRestart()
        {
            CloseButton.SetActive(true);
            OkButton.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Oyun Yeniden Sıfır Ayarlarında Başlatılacaktır.";
            WrongText.text = " ";
            Oriantation.SetActive(false);
        }

        public void EndGame()
        {
            CloseButton.SetActive(false);
            OkButton.SetActive(false);
            Popup.SetActive(true);
            CorrectText.text = "Oyun Başarıyla Tamamlanmıştır.";
            WrongText.text = " ";

            Oriantation.SetActive(true);
        }
    }
}
