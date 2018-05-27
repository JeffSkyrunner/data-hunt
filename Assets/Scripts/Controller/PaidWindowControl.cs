﻿using model.timing.runner;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace controller
{
    public class PaidWindowControl : MonoBehaviour, IPaidWindowObserver
    {
        private PaidWindow window;

        public void Represent(PaidWindow window)
        {
            this.window = window;
            var flag = CreateFlag(gameObject).AddComponent<PaidWindowFlag>();
            var pass = CreatePass(gameObject).AddComponent<DropZone>();
            flag.Represent(window, pass);
            gameObject.SetActive(false);
            window.Observe(this);
        }

        private GameObject CreateFlag(GameObject parent)
        {
            var flag = new GameObject("Flag");
            flag.transform.SetParent(parent.transform, false);
            var image = flag.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("Images/UI/paid");
            image.preserveAspect = true;
            var rectangle = image.rectTransform;
            rectangle.anchorMin = Vector2.zero;
            rectangle.anchorMax = new Vector2(0.4f, 1.0f);
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return flag;
        }

        private GameObject CreatePass(GameObject parent)
        {
            var pass = new GameObject("Pass");
            pass.transform.SetParent(parent.transform, false);
            var image = pass.AddComponent<Image>();
            image.sprite = AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/Background.psd");
            image.type = Image.Type.Sliced;
            image.fillCenter = true;
            var rectangle = image.rectTransform;
            rectangle.anchorMin = new Vector2(0.6f, 0.0f);
            rectangle.anchorMax = Vector2.one;
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            var aspect = pass.AddComponent<AspectRatioFitter>();
            aspect.aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            aspect.aspectRatio = 1f;
            CreatePassLabel(pass);
            return pass;
        }

        private GameObject CreatePassLabel(GameObject parent)
        {
            var label = new GameObject("Label");
            label.transform.SetParent(parent.transform, false);
            var text = label.AddComponent<Text>();
            text.text = "PASS";
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 10;
            text.resizeTextMaxSize = 42;
            text.raycastTarget = false;
            var rectangle = text.rectTransform;
            rectangle.anchorMin = Vector2.zero;
            rectangle.anchorMax = Vector2.one;
            rectangle.offsetMin = Vector2.zero;
            rectangle.offsetMax = Vector2.zero;
            return label;
        }

        void IPaidWindowObserver.NotifyPaidWindowClosed()
        {
            gameObject.SetActive(false);
        }

        void IPaidWindowObserver.NotifyPaidWindowOpened()
        {
            gameObject.SetActive(true);
        }

        void OnDestroy()
        {
            window.Unobserve(this);
        }
    }
}