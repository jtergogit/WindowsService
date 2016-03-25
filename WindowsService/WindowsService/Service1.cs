using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer t;
        public Service1()
        {
            InitializeComponent();
        }

        public void TimeElapse(object source, System.Timers.ElapsedEventArgs e)
        {
            FileStream fs = new FileStream(@"d:\timetick.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine("过了一秒 " + DateTime.Now.ToString() + "\n");
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();

        }

        protected override void OnStart(string[] args)
        {
            IntialSaveRecord();
        }

        private void IntialSaveRecord()
        {
            t = new System.Timers.Timer(1000);//实例化Timer类，设置间隔时间为10000毫秒；   
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimeElapse);//到达时间的时候执行事件；   
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
        }

        protected override void OnStop()
        {
            if (t != null)
            {
                t.Dispose();
            }
        }
    }
}
