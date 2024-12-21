using TMPro;
using UnityEngine;

namespace Homework1
{
    public class Notification : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp;
        
        public void SetNotificationText(string text) => tmp.text = text;
    }
}
