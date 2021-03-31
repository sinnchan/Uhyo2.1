using UnityEngine;

namespace Main.Util
{
    /// <summary>
    /// Loggerのラッパー
    /// </summary>
    public static class Logger
    {
        private static ILogger _logger = Debug.unityLogger;
        
        public static void Info(string tag, object message) => _logger.Log(tag, message);

        public static void Warning(string tag, object message) => _logger.LogWarning(tag, message);

        public static void Error(string tag, object message) => _logger.LogError(tag, message);

        /// <summary>
        /// ログの出力設定
        /// </summary>
        /// <param name="enable"></param>
        public static void Enable(bool enable) => _logger.logEnabled = enable;

        /// <summary>
        /// 出力するログレベル
        /// 0 -> Info、Warning、Error
        /// 1 -> Warning、Error
        /// 2 -> Error
        /// </summary>
        /// <param name="logLevel"></param>
        public static void SetLogLevel(int logLevel)
        {
            if (logLevel < 0 || 3 < logLevel)
                Warning(nameof(Logger), "Misconfiguration of Log Level. Set LogLevel: 0");

            _logger.filterLogType = logLevel switch
            {
                0 => LogType.Log,
                1 => LogType.Warning,
                2 => LogType.Error,
                _ => LogType.Log
            };
        }
    }
}