﻿using YYSLS.Service.Notification.Model;

namespace YYSLS.Service.Notification.Builder;

public class LifecycleNotificationBuilder : INotificationDataBuilder<LifecycleNotificationData>
{
    private readonly LifecycleNotificationData _notificationData = new();

    public LifecycleNotificationData Build()
    {
        return _notificationData;
    }
}
