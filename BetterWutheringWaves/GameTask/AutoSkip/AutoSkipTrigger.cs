﻿using YYSLS.Core.Simulator;
using YYSLS.GameTask.AutoSkip.Assets;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using YYSLS.GameTask.Common;
using YYSLS.View.Drawable;
using Vanara.PInvoke;

namespace YYSLS.GameTask.AutoSkip;

/// <summary>
/// 自动剧情有选项点击
/// </summary>
public class AutoSkipTrigger : ITaskTrigger
{
    private readonly ILogger<AutoSkipTrigger> _logger = App.GetLogger<AutoSkipTrigger>();

    public string Name => "自动剧情";
    public bool IsEnabled { get; set; }
    public int Priority => 20;
    public bool IsExclusive => false;

    private readonly PostMessageSimulator _simulator = Simulation.PostMessage(TaskContext.Instance().GameHandle);

    private readonly Random _random = new();

    public void Init()
    {
        IsEnabled = TaskContext.Instance().Config.AutoSkipConfig.Enabled;
    }

    private DateTime _prevExecute = DateTime.MinValue;

    public void OnCapture(CaptureContent content)
    {
        if ((DateTime.Now - _prevExecute).TotalMilliseconds <= 200)
        {
            return;
        }

        _prevExecute = DateTime.Now;

        if (TaskContext.Instance().Config.AutoSkipConfig.PressSkipEnabled && SystemControl.IsGameActive())
        {
            var skipRa = content.CaptureRectArea.Find(AutoSkipAssets.Instance.SkipButtonRo);
            if (skipRa.IsExist())
            {
                skipRa.BackgroundClick();
                Thread.Sleep(1000);
                using var captureRa = TaskControl.CaptureToRectArea();
                captureRa.Find(AutoSkipAssets.Instance.NotPromptAgainButtonRo, region =>
                {
                    region.BackgroundClick();
                });
                Thread.Sleep(300);
                captureRa.Find(AutoSkipAssets.Instance.ConfirmButtonRo, region =>
                {
                    region.BackgroundClick();
                });
                VisionContext.Instance().DrawContent.ClearAll();
                return;
            }
        }

        bool inStory = true;
        var r1 = content.CaptureRectArea.Find(AutoSkipAssets.Instance.StartAutoButtonRo);
        if (r1.IsEmpty())
        {
            var r2 = content.CaptureRectArea.Find(AutoSkipAssets.Instance.StopAutoButtonRo);
            if (r2.IsEmpty())
            {
                inStory = false;
            }
        }

        if (inStory)
        {
            // 点最下面的对话选项
            _simulator.KeyPressBackground(User32.VK.VK_UP);
            Thread.Sleep(50);
            _simulator.KeyPressBackground(User32.VK.VK_F);
        }
    }
}
