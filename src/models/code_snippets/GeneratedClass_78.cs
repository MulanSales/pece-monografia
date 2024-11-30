using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;
using MouseEvent;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows;

namespace DoubleCursor
{
class IPC
{
LocalIndicator localIndicator;
RemoteIndicator remoteIndicator;
LocalCursor localCursor;
RemoteCursor remoteCursor;

private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

}
}
