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
        public GameObject Button;
        public TextMeshPro CorrectText;
        public TextMeshPro WrongText;

        private void Awake()
        {
            Instance = this;
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
            Button.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Doğru Sayısı : " + correct.ToString();
            WrongText.text = "Yanlış Sayısı : " + wrong.ToString();
        }

        public void PassivePopup()
        {
            Button.SetActive(true);
            Popup.SetActive(false);
            CorrectText.text = " ";
            WrongText.text = " ";
        }

        public void ActiveHeart()
        {
            Button.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Canınız Yetersizdir, Bir önceki Seviyeyi Tekrar Etmek Zorundasınız.";
            WrongText.text = " ";
        }


        public void ActiveTimer()
        {
            Button.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Süreniz Bitmiştir, Bir önceki Seviyeyi Tekrar Etmek Zorundasınız.";
            WrongText.text = " ";
        }

        public void ActiveRestart()
        {
            Button.SetActive(true);
            Popup.SetActive(true);
            CorrectText.text = "Oyun Yeniden Sıfır Ayarlarında Başlatılacaktır.";
            WrongText.text = " ";
        }

        public void EndGame()
        {
            Button.SetActive(false);
            Popup.SetActive(true);
            CorrectText.text = "Oyun Başarıyla Tamamlanmıştır.";
            WrongText.text = " ";
        }
    }
}
