using System;
namespace XamarinBGServ.Models
{
    public class LoggerItem
    {
        public string Time { get; set; }
        public string Content { get; set; }
        public string ConsoleOutput
        {
            get
            {
                return String.Format("{0} \t{1}", Time, Content);
            }
        }
    }
}
