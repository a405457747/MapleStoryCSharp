using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace QFramework
{

    /// <summary>
    /// 封装日志模块
    /// </summary>
    public class QLog : MonoBehaviour
    {
        /// <summary>
        /// 日志等级，为不同输出配置用
        /// </summary>
        public enum LogLevel
        {
            LOG     = 0,
            WARNING = 1,
            ASSERT  = 2,
            ERROR   = 3,
            MAX     = 4,
        }

        /// <summary>
        /// 日志数据类
        /// </summary>
        public class LogData
        {
            public string   Log   { get; set; }
            public string   Track { get; set; }
            public LogLevel Level { get; set; }
        }

        /// <summary>
        /// OnGUI回调
        /// </summary>
        public delegate void OnGUICallback();

        /// <summary>
        /// UI输出日志等级，只要大于等于这个级别的日志，都会输出到屏幕
        /// </summary>
        public LogLevel uiOutputLogLevel = LogLevel.LOG;

        /// <summary>
        /// 文本输出日志等级，只要大于等于这个级别的日志，都会输出到文本
        /// </summary>
        public LogLevel fileOutputLogLevel = LogLevel.MAX;

        /// <summary>
        /// unity日志和日志输出等级的映射
        /// </summary>
        private Dictionary<LogType, LogLevel> logTypeLevelDict = null;

        /// <summary>
        /// OnGUI回调
        /// </summary>
        public OnGUICallback onGUICallback = null;

        /// <summary>
        /// 日志输出列表
        /// </summary>
        private List<ILogOutput> logOutputList = null;

        private int mainThreadID = -1;

        /// <summary>
        /// Unity的Debug.Assert()在发布版本有问题
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="info">输出信息</param>
        public static void Assert(bool condition, string info)
        {
            if (condition)
                return;
            Debug.LogError(info);
        }

        private void Awake()
        {
            Application.logMessageReceived += LogCallback;
            Application.logMessageReceivedThreaded += LogMultiThreadCallback;

            this.logTypeLevelDict = new Dictionary<LogType, LogLevel>
            {
                {LogType.Log, LogLevel.LOG},
                {LogType.Warning, LogLevel.WARNING},
                {LogType.Assert, LogLevel.ASSERT},
                {LogType.Error, LogLevel.ERROR},
                {LogType.Exception, LogLevel.ERROR},
            };

            this.uiOutputLogLevel = LogLevel.LOG;
            this.fileOutputLogLevel = LogLevel.ERROR;
            this.mainThreadID = Thread.CurrentThread.ManagedThreadId;
            this.logOutputList = new List<ILogOutput>
            {
                new QFileLogOutput(),
            };

        }

        void OnGUI()
        {
            if (this.onGUICallback != null)
                this.onGUICallback();
        }

        void OnDestroy()
        {
            Application.logMessageReceived -= LogCallback;
            Application.logMessageReceivedThreaded -= LogMultiThreadCallback;
        }

        private void OnApplicationQuit()
        {
            this.logOutputList.ForEach(output => output.Close());
        }

        /// <summary>
        /// 日志调用回调，主线程和其他线程都会回调这个函数，在其中根据配置输出日志
        /// </summary>
        /// <param name="log">日志</param>
        /// <param name="track">堆栈追踪</param>
        /// <param name="type">日志类型</param>
        void LogCallback(string log, string track, LogType type)
        {
            if (this.mainThreadID == Thread.CurrentThread.ManagedThreadId)
                Output(log, track, type);
        }

        void LogMultiThreadCallback(string log, string track, LogType type)
        {
            if (this.mainThreadID != Thread.CurrentThread.ManagedThreadId)
                Output(log, track, type);
        }

        void Output(string log, string track, LogType type)
        {
            LogLevel level = this.logTypeLevelDict[type];
            LogData logData = new LogData
            {
                Log = log,
                Track = track,
                Level = level,
            };
            foreach (var t in this.logOutputList)
                t.Log(logData);
        }
    }



    /// <summary>
    /// 日志输出接口
    /// </summary>
    public interface ILogOutput
    {
        /// <summary>
        /// 输出日志数据
        /// </summary>
        /// <param name="logData">日志数据</param>
        void Log(QLog.LogData logData);

        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
    }


    /// <summary>
    /// 文本日志输出
    /// </summary>
    public class QFileLogOutput : ILogOutput
    {

#if UNITY_EDITOR
        string mDevicePersistentPath = Application.dataPath + "/../PersistentPath";
#elif UNITY_STANDALONE_WIN
        string mDevicePersistentPath = Application.dataPath + "/PersistentPath";
#elif UNITY_STANDALONE_OSX
        string mDevicePersistentPath = Application.dataPath + "/PersistentPath";
#else
        string mDevicePersistentPath = Application.persistentDataPath;
#endif


        static string LogPath = "Log";

        private Queue<QLog.LogData> mWritingLogQueue = null;
        private Queue<QLog.LogData> mWaitingLogQueue = null;
        private object              mLogLock         = null;
        private Thread              mFileLogThread   = null;
        private bool                mIsRunning       = false;
        private StreamWriter        mLogWriter       = null;

        public QFileLogOutput()
        {
            this.mWritingLogQueue = new Queue<QLog.LogData>();
            this.mWaitingLogQueue = new Queue<QLog.LogData>();
            this.mLogLock = new object();
            System.DateTime now = System.DateTime.Now;
            string logName = string.Format("Q{0}{1}{2}{3}{4}{5}",
                now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
            string logPath = string.Format("{0}/{1}/{2}.txt", mDevicePersistentPath, LogPath, logName);
            if (File.Exists(logPath))
                File.Delete(logPath);
            string logDir = Path.GetDirectoryName(logPath);
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);
            this.mLogWriter = new StreamWriter(logPath);
            this.mLogWriter.AutoFlush = true;
            this.mIsRunning = true;
            this.mFileLogThread = new Thread(new ThreadStart(WriteLog));
            this.mFileLogThread.Start();
        }

        void WriteLog()
        {
            while (this.mIsRunning)
            {
                if (this.mWritingLogQueue.Count == 0)
                {
                    lock (this.mLogLock)
                    {
                        while (this.mWaitingLogQueue.Count == 0)
                            Monitor.Wait(this.mLogLock);
                        Queue<QLog.LogData> tmpQueue = this.mWritingLogQueue;
                        this.mWritingLogQueue = this.mWaitingLogQueue;
                        this.mWaitingLogQueue = tmpQueue;
                    }
                }
                else
                {
                    while (this.mWritingLogQueue.Count > 0)
                    {
                        QLog.LogData log = this.mWritingLogQueue.Dequeue();
                        if (log.Level == QLog.LogLevel.ERROR)
                        {
                            this.mLogWriter.WriteLine(
                                "---------------------------------------------------------------------------------------------------------------------");
                            this.mLogWriter.WriteLine(System.DateTime.Now.ToString() + "\t" + log.Log + "\n");
                            this.mLogWriter.WriteLine(log.Track);
                            this.mLogWriter.WriteLine(
                                "---------------------------------------------------------------------------------------------------------------------");
                        }
                        else
                        {
                            this.mLogWriter.WriteLine(System.DateTime.Now.ToString() + "\t" + log.Log);
                        }
                    }
                }
            }
        }

        public void Log(QLog.LogData logData)
        {
            lock (this.mLogLock)
            {
                this.mWaitingLogQueue.Enqueue(logData);
                Monitor.Pulse(this.mLogLock);
            }
        }

        public void Close()
        {
            this.mIsRunning = false;
            this.mLogWriter.Close();
        }
    }
}