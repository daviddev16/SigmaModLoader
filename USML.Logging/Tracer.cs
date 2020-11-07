using System;
using System.Diagnostics;
using System.Text;

namespace USML {
    
    public class Tracer {

        private readonly static Tracer TRACER = new Tracer();

        public static readonly string LOG_TRACER_FORMAT = "[{3}] [at {0}] [{1}]: {2}";

        public StringBuilder LogBuilder { get; }
        public object Holder { get; private set; } = null;

        public int FailCount { get; private set; } = 0;

        public Tracer()
        {

            if(TRACER != null) 
            {
                throw new SystemException("Tracer cannot be instantiate twice.");
            }

            LogBuilder = new StringBuilder();
            DrawSeparator();
        }

        public void log(string message) 
        {
            Print(message, "info", false);
        }

        public void warn(string message)
        {
            Print(message, "warn",false);
        }

        public void fail(string message) 
        {
            Print(message, "fail", false);
            FailCount++;
        }

        public void critical(string throwMessage)
        {
            Print(throwMessage, "critical", false);
        }

        public void stacktrace(string line, bool hide) 
        {
            Print(line, "stacktrace", hide);
        }

        public void tHrow(string message, string throwMessage) 
        {
            fail(message);
            critical(throwMessage);
        }

        public void SetHolder(object holder) {
            this.Holder = holder;
        }


        private void CheckHolder() 
        {
            if(Holder == null) {
                Holder = this;
            }
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

        public override string ToString()
        {
            DrawSeparator();
            return LogBuilder.ToString();
        }

        public bool HasFailed() 
        {
            return FailCount > 0;
        }

        private void DrawSeparator()
        {
            LogBuilder.Append("###################################################").AppendLine();
        }

        public static void Here(in object obj) {
            TRACER.SetHolder(obj);
        }

        [Obsolete("use Tracer.Here() instead.", true)]
        public static void SetObjectHolder(object o) {
            TRACER.SetHolder(o);
        }

        public static void Fail(string message) {
            TRACER.fail(message);
        }

        public static void Throw(string message, string throwMessage) {
            TRACER.tHrow(message, throwMessage);
        }

        public static void Log(string message) {
            TRACER.log(message);
        }

        public static void Fatal(string throwMessage) {
            TRACER.critical(throwMessage);
        }

        public static void Warning(string message) {
            TRACER.warn(message);
        }

        public static void AddLogSeparator() {
            TRACER.DrawSeparator();
        }

        public static void Exception(Exception e, bool hide) 
        {
            Throw(e.GetType().Name, e.Message);
            foreach (string line in e.StackTrace.Split("\n")) {
                TRACER.stacktrace(line, hide);
            }
        }

        public static Tracer GetSingleTrace() 
        {
            return TRACER;
        }

    }

}