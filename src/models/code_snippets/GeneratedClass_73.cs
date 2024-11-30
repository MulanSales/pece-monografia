// Copyright 2006 Alp Toker <alp@atoker.com>
// This software is made available under the MIT License
// See COPYING for details

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

using System.Runtime.InteropServices;

using Mono.Unix;
using Mono.Unix.Native;

namespace NDesk.DBus
{
public class UnixSocket
{
//TODO: verify these
[DllImport ("libc")]
protected static extern int socket (int domain, int type, int protocol);

}
}
