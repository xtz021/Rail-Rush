using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoticePopUpController : MonoBehaviour
{
    [SerializeField] private TMP_Text noticeText;

    public void SetNoticeText(string text)
    {
        if (noticeText != null)
        {
            noticeText.text = text;
        }
        else
        {
            Debug.LogWarning("Notice Text component is not assigned.");
        }
    }

    public void CloseNoticePopUp()
    {
        Destroy(gameObject);
    }
}
