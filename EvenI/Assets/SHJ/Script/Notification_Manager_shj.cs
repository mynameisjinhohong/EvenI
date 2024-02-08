using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Notifications.Android;

//Mobile Notifications ��Ű��

public class Notification_Manager_shj : MonoBehaviour
{
    AndroidNotificationChannel notificationChannel;
    AndroidNotification notification;

    public delegate void Noti(int sec);

    public void Noti_Panda(int sec)
    {
        AndroidNotificationCenter.CancelAllNotifications();
        notificationChannel = new AndroidNotificationChannel(
       "channel",
       "Default Channel",
       "info",
       Importance.High
        );
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        notification = new AndroidNotification(
        "Panda Rush",
       "�Ǵٰ� ���Ŀ� X_X ġ�����ּ���!!",
        DateTime.Now.AddSeconds(sec)
        );
        AndroidNotificationCenter.SendNotification(notification, "channel");
    }


    //private void OnApplicationPause(bool pause) //����� �۵�
    //{
    //    if(pause)
    //    {
    //        int sec = GameManager_shj.Getinstance.Save_data.nexthealtime 
    //            - int.Parse(DateTime.Now.ToString("HH")) * 3600 + int.Parse(DateTime.Now.ToString("mm")) * 60 + int.Parse(DateTime.Now.ToString("ss"));
    //        Noti_Panda(sec);
    //    }
    //}
}
