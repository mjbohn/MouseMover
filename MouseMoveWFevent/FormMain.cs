using System.Diagnostics;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;


namespace MouseMoveWFevent;

public partial class FormMain : Form
{
    private Timer inactivityTimer;
    private IntPtr hookId = IntPtr.Zero;
    private NativeMethods.LowLevelMouseProc mouseProc;

    private string _status_watch         = "watching mouse moves...";
    private string _status_sim           = "last simulated move:";
    private string _status_move_detected = "mouse move detected:";
    private bool _reallyCloseThis = false;

    public FormMain()
    {
        InitializeComponent();

        //trayIcon.Icon = SystemIcons.Application;
        trayIcon.ContextMenuStrip = trayMenu;
        trayIcon.Text = "MousMover";
        trayIcon.Visible = true;

        this.ShowInTaskbar = false;

        inactivityTimer = new Timer();
        inactivityTimer.Interval = (int)numericUpDownwaitTime.Value * 1000;
        inactivityTimer.Tick += (s, e) => SimulateMouseMove();
        inactivityTimer.Start();

        mouseProc = HookCallback;
        hookId = NativeMethods.SetHook(mouseProc);

        lblStatus.Text = _status_watch;
    }

    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0)
        {
            // Mouse move detected => reset timer

            if (inactivityTimer.Enabled)
            {
                inactivityTimer.Stop();
                inactivityTimer.Start();
            }

            lblStatus.Invoke((MethodInvoker)(() =>
            {
                lblLastMove.Text = $"{_status_move_detected}    {DateTime.Now:T}";

            }));


        }
        return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
    }

    private void SimulateMouseMove()
    {
        lblStatus.Text = $"{_status_sim}  {DateTime.Now:T}";

        NativeMethods.INPUT[] input = new NativeMethods.INPUT[2];
        input[0] = new NativeMethods.INPUT
        {
            type = NativeMethods.INPUT_MOUSE,
            mi = new NativeMethods.MOUSEINPUT { dx = 10, dy = 10, dwFlags = NativeMethods.MOUSEEVENTF_MOVE }
        };
        input[1] = new NativeMethods.INPUT
        {
            type = NativeMethods.INPUT_MOUSE,
            mi = new NativeMethods.MOUSEINPUT { dx = -10, dy = -10, dwFlags = NativeMethods.MOUSEEVENTF_MOVE }
        };

        NativeMethods.SendInput((uint)input.Length, input, Marshal.SizeOf(typeof(NativeMethods.INPUT)));
    }

    private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
    {
        NativeMethods.UnhookWindowsHookEx(hookId);
    }

    private void numericUpDownwaitTime_ValueChanged(object sender, EventArgs e)
    {
        inactivityTimer.Interval = (int)numericUpDownwaitTime.Value * 1000;
    }

    private void showToolStripMenuItem_Click(object sender, EventArgs e)
    {
        this.Show();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        _reallyCloseThis = true;
        Application.Exit();
    }

    private void trayIcon_DoubleClick(object sender, EventArgs e)
    {
        this.Show();
    }

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!_reallyCloseThis)
        {
            this.Hide();
            e.Cancel = true;
            trayIcon.ShowBalloonTip(3000,"MouseMover is still running!","use context menu to exit",ToolTipIcon.Info);
        }
    }
}

internal static class NativeMethods
{
    public const int WH_MOUSE_LL = 14;
    public const int INPUT_MOUSE = 0;
    public const int MOUSEEVENTF_MOVE = 0x0001;

    public delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx, dy, mouseData, dwFlags, time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public int type;
        public MOUSEINPUT mi;
    }

    [DllImport("user32.dll")]
    public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll")]
    public static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll")]
    public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    public static IntPtr SetHook(LowLevelMouseProc proc)
    {
        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule curModule = curProcess.MainModule!)
        {
            return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
        }
    }
}

