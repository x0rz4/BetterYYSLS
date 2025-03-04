﻿using YYSLS.Core.Simulator;
using YYSLS.GameTask.Model.Area.Converter;
using YYSLS.Helpers;
using System.Drawing;

namespace YYSLS.GameTask.Model.Area;

/// <summary>
/// 桌面区域类
/// 无缩放的桌面屏幕大小
/// 主要用于点击操作
/// </summary>
public class DesktopRegion() : Region(0, 0, PrimaryScreen.WorkingArea.Width, PrimaryScreen.WorkingArea.Height)
{
    public void DesktopRegionClick(int x, int y, int w, int h)
    {
        Simulation.SendInput.Mouse.MoveMouseTo((x + (w * 1d / 2)) * 65535 / Width,
            (y + (h * 1d / 2)) * 65535 / Height).LeftButtonClick().Sleep(50).LeftButtonUp();
    }

    /// <summary>
    /// 静态方法,每次都会重新计算屏幕大小
    /// </summary>
    /// <param name="cx"></param>
    /// <param name="cy"></param>
    public static void DesktopRegionClick(double cx, double cy)
    {
        Simulation.SendInput.Mouse.MoveMouseTo(cx * 65535 * 1d / PrimaryScreen.WorkingArea.Width,
            cy * 65535 * 1d / PrimaryScreen.WorkingArea.Height).LeftButtonDown().Sleep(50).LeftButtonUp();
    }

    public static void DesktopRegionMove(double cx, double cy)
    {
        Simulation.SendInput.Mouse.MoveMouseTo(cx * 65535 * 1d / PrimaryScreen.WorkingArea.Width,
            cy * 65535 * 1d / PrimaryScreen.WorkingArea.Height);
    }

    public GameCaptureRegion Derive(Bitmap captureBitmap, int x, int y)
    {
        return new GameCaptureRegion(captureBitmap, x, y, this, new TranslationConverter(x, y));
    }
}
