﻿using System;
using System.Threading.Tasks;
using Godot;

namespace Sandbox;

public static class TaskExtensions
{
    public static async void Fire(this Task task, Action? onComplete = null, Action<Exception>? onError = null)
    {
        try
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                GD.Print("something wrong during fire & forget: ");
                GD.Print(e);
                onError?.Invoke(e);
            }

            onComplete?.Invoke();
        }
        catch (Exception e)
        {
            GD.Print("something wrong on fire & forget complete : ");
            GD.Print(e);
            onError?.Invoke(e);
        }
    }
}