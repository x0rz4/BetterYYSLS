﻿using YYSLS.Service.Notification.Converter;
using YYSLS.Service.Notification.Model.Enum;
using System.Drawing;
using System.Text.Json.Serialization;

namespace YYSLS.Service.Notification.Model;

public record TaskNotificationData : INotificationData
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationEvent Event { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationAction Action { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public NotificationConclusion? Conclusion { get; set; }

    [JsonConverter(typeof(ImageToBase64Converter))]
    public Image? Screenshot { get; set; }

    public object? Task { get; set; }
}
