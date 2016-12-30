﻿// Copyright (c) 2007-2016 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu-framework/master/LICENCE

using System;
using System.Threading.Tasks;

namespace osu.Framework.Platform
{
    public interface IIpcHost
    {
        event Action<IpcMessage> MessageReceived;

        Task SendMessage(IpcMessage ipcMessage);
    }
}
