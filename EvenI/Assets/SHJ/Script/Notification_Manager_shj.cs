using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Notifications.Android;

public class Notification_Manager_shj : MonoBehaviour
{
    AndroidNotificationChannel notificationChannel;
    AndroidNotification notification;

    public void Noti_Panda(int sec)
    {
        AndroidNotificationCenter.CancelAllNotifications();
        notificationChannel = new AndroidNotificationChannel();
        notificationChannel.Id = "channel";
        notificationChannel.Name = "Default Channel";
        notificationChannel.Importance = Importance.High;
        
        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        notification = new AndroidNotification();
        notification.Title = "Panda Rush";
        notification.Text = "판다가 아파요 X_X 치료해주세요!!";
        notification.LargeIcon = "icon_1";
        notification.SmallIcon = "icon_0";
        notification.FireTime = DateTime.Now.AddSeconds(20);

        AndroidNotificationCenter.SendNotification(notification, "channel");
    }
    //public void Push_Alarm()
    //{
    //    AndroidNotificationChannel channel = new AndroidNotificationChannel()
    //    {
    //        Id = "channel_id",
    //        Name = "Default Channel",
    //        Importance = Importance.High
    //    };
    //    AndroidNotificationCenter.RegisterNotificationChannel(channel);

    //    AndroidNotification notification = new AndroidNotification();
    //    notification.Title = "테스트 타이틀";
    //    notification.Text = "테스트 텍스트";
    //    notification.FireTime = DateTime.Now.AddSeconds(20);
    //    AndroidNotificationCenter.SendNotification(notification, "channel_id");
    //}
}
