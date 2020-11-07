using System;

using System.Text;

namespace USML {
    
    public class Tracer {
        
        public static readonly string LOG_TRACER_FORMAT = "[{4}] [at {0}] [{1}] [{2}]: {3}";

        public StringBuilder LogBuilder { get; }
        public string Label { get; private set; } = null;
        public object Holder { get; private set; } = null;

        public int FailCount { get; private set; } = 0;

        public Tracer()
        {
            LogBuilder = new StringBuilder();
            DrawSeparator();
        }

       

        public void Log(string message) 
        {
            Print(message, "info");
        }

        public void Warning(string message)
        {
            Print(message, "warn");
        }

        public void Fail(string message) 
        {
            Print(message, "fail");
            FailCount++;
        }

        public void Detail(string throwMessage)
        {
            Print(throwMessage, "exception|detail");
        }

        public void SetOptionalLabel(string label) {
            this.Label = label;
        }

        public void SetHolder(object holder) {
            this.Holder = holder;
        }

        public void Throw(string message, string throwMessage)
        {
            Fail(message);
            Detail(throwMessage);
        }

        private void CheckLabelHolder() 
        {
            if(Holder == null) {
                Holder = this;
            }

            if(Label == null) {
                Label = "UNKNOWN";
            }
        }

        private void Print(string message, string status)
        {
            CheckLabelHolder();
            string typeClass = Holder.GetType().Name;
            string timeToString = DateTime.Now.ToString();
            string textString = string.Format(LOG_TRACER_FORMAT, typeClass, Label, status.ToUpper(), message, timeToString);
            LogBuilder.Append(textString).AppendLine();
            Console.WriteLine(textString);
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

    }

}