﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace controller
{
    public class TopOfTheStack : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public GripZone gripZone;
        private Vector3 originalPosition;

        private CanvasGroup CanvasGroup { get { return GetComponent<CanvasGroup>(); } }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
            CanvasGroup.blocksRaycasts = false;
            originalPosition = this.transform.position;
            BringToFront();
            gripZone.UpdateHighlights(eventData);
        }

        private void BringToFront()
        {
            transform.parent.SetAsLastSibling();
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            this.transform.position = eventData.position;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            eventData.selectedObject = null;
            CanvasGroup.blocksRaycasts = true;
            var raycast = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycast);
            var onGrip = raycast.Where(r => r.gameObject == gripZone.gameObject).Any();
            if (onGrip)
            {
                Netrunner.game.runner.stack.Draw();
            }
            this.transform.position = originalPosition;
            gripZone.UpdateHighlights(eventData);
        }
    }
}