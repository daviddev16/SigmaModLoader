

using System;
using System.Text;

namespace USML {

    /// <summary>
    /// Provides method for the USML logging.
    /// <br>use all static methods</br>
    /// </summary>
    public class Tracer {

        private readonly static Tracer TRACER = new Tracer();
        private static readonly string LOG_TRACER_FORMAT = "[{3}] [at {0}] [{1}]: {2}";

        /// <summary>
        /// The entire log String.
        /// </summary>
        public StringBuilder LogBuilder { get; }


        /// <summary>
        /// The current object using the Tracer
        /// <br>Can be set with <see cref="Here(in object)"/> function.</br>
        /// </summary>
        public object Holder { get; private set; } = null;


        /// <summary>
        /// this variable increases every time <see cref="Fail(string)"/> is called
        /// </summary>
        public int FailCount { get; private set; } = 0;

        private Tracer()
        {
            if(TRACER != null) 
            {
                throw new SystemException("Tracer cannot be instantiate twice.");
            }
            LogBuilder = new StringBuilder();
            DrawSeparator();
        }

        /// <summary>
        /// Set the Tracer holder object
        /// </summary>
        /// <param name="obj">the actual object using Tracer</param>
        public static void Here(in object obj) {
            TRACER.SetHolder(obj);
        }

        [Obsolete("use Tracer.Here() instead.", true)]
        private static void SetObjectHolder(object o) {
            TRACER.SetHolder(o);
        }

        /// <summary>
        /// Print a fail message.
        /// </summary>
        /// <param name="message">the message that will be sent</param>
        public static void Fail(string message) {
            TRACER.fail(message);
        }

        /// <summary>
        /// Print an Exception message.
        /// </summary>
        /// <param name="message">the message that will be sent</param>
        /// <param name="throwMessage">the message from the exception</param>
        public static void Throw(string message, string throwMessage) {
            TRACER.tHrow(message, throwMessage);
        }

        /// <summary>
        /// Print a common log message.
        /// </summary>
        /// <param name="message">the message that will be sent</param>
        public static void Log(string message) {
            TRACER.log(message);
        }

        /// <summary>
        /// Print a critical message.
        /// </summary>
        /// <param name="throwMessage">the message that will be sent</param>
        public static void Fatal(string throwMessage) {
            TRACER.critical(throwMessage);
        }

        /// <summary>
        /// Print a warning message.
        /// </summary>
        /// <param name="message">the message that will be sent</param>
        public static void Warning(string message) {
            TRACER.warn(message);
        }

        // <summary>
        /// Writes a simple "#" separator
        /// </summary>
        public static void AddLogSeparator() {
            TRACER.DrawSeparator();
        }

        /// <summary>
        /// Print a full exception message with the stacktrace include
        /// </summary>
        /// <param name="e">The thrown exception</param>
        /// <param name="hide">if the Tracer will hide in the Console</param>
        public static void Exception(Exception e, bool hide) {
            Throw(e.GetType().Name, e.Message);
            foreach(string line in e.StackTrace.Split("\n")) {
                TRACER.stacktrace(line, hide);
            }
        }

        /// <summary>
        /// The single instance of the Tracer
        /// </summary>
        public static Tracer GetSingleTrace() {
            return TRACER;
        }

        /// <summary>
        /// if the Tracer registered any fail.
        /// </summary>
        /// <returns>true if the <see cref="FailCount"/> is greater than 0</returns>
        public bool HasFailed() {
            return FailCount > 0;
        }

        /// <summary>
        /// gives the entire string of the logging.
        /// </summary>
        public override string ToString() {
            DrawSeparator();
            return LogBuilder.ToString();
        }

        private void Print(string message, string status, bool hide) 
        {
            CheckHolder();
            string typeClass = Holder.GetType().Name;
            string timeToString = DateTime.Now.ToString();
            string textString = string.Format(LOG_TRACER_FORMAT, typeClass, status.ToUpper(), message, timeToString);
            LogBuilder.Append(textString).AppendLine();
            if(!hide) {
                Console.WriteLine(textString);
            }
        }
        private void log(string message) 
        {
            Print(message, "info", false);
        }
        private void warn(string message)
        {
            Print(message, "warn",false);
        }
        private void fail(string message) 
        {
            Print(message, "fail", false);
            FailCount++;
        }
        private void critical(string throwMessage)
        {
            Print(throwMessage, "critical", false);
        }
        private void stacktrace(string line, bool hide) 
        {
            Print(line, "stacktrace", hide);
        }
        private void tHrow(string message, string throwMessage) 
        {
            fail(message);
            critical(throwMessage);
        }
        private void SetHolder(object holder) {
            this.Holder = holder;
        }
        private void CheckHolder() 
        {
            if(Holder == null) {
                Holder = this;
            }
        }
        private void DrawSeparator()
        {
            LogBuilder.Append("###################################################").AppendLine();
        }
    }

}