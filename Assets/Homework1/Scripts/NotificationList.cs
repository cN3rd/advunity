using System.Collections.Generic;
using UnityEngine;

namespace Homework1
{
    public class NotificationList : MonoBehaviour
    {
        [SerializeField] private List<NotifierBehaviour> updaters;
        [SerializeField] private GameObject notificationItemTemplate;

        private void Start()
        {
            foreach (var updater in updaters)
            {
                updater.OnNotify += AddUpdate;
            }
        }

        private void OnDestroy()
        {
            foreach (var updater in updaters)
            {
                updater.OnNotify -= AddUpdate;
            }
        }

        private void AddUpdate(string message)
        {
            Debug.Log(message);
            var messageGameObject = Instantiate(notificationItemTemplate, transform, false);
            var messageComponent = messageGameObject.GetComponent<Notification>();
            messageComponent.SetNotificationText(message);
        }
    }
}
