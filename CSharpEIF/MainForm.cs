using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using CSharpEIF.AppEnvironment;
using CSharpEIF.AppObjects;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpEIF
{
    public partial class MainForm : Form
    {
        /* Global Variables */
        static IConnection myConnection = null;
        static ISession mySession = null;
        static IMessageConsumer myConsumer = null;
        static IConnectionFactory myFactory = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            myBackgroundWorker.RunWorkerAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Enabled = false;
            myBackgroundWorker.CancelAsync();
        }

        private void myBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            /* Set worker */
            BackgroundWorker worker = (BackgroundWorker)sender;

            /* My 3DExperience Credentials */
            My3DExperience myVar = new My3DExperience();

            // Topic name...
            string userTopicName = "3dsevents." + myVar.Tenant + ".3DSpace.user"; // .3DSpace.admin/administrator

            // ClientID...
            string clientID = myVar.Tenant + "-" + myVar.AgentID;

            /* Create a new Connection Factory */
            myFactory = new ConnectionFactory(myVar.JmsUrl);

            /* Create a new Connection */
            myConnection = myFactory.CreateConnection(myVar.AgentID, myVar.AgentPassword);
            myConnection.ClientId = clientID;

            /* Start Connection */
            myConnection.Start();

            /* Create a new session */
            mySession = myConnection.CreateSession();

            /* Create a Consumer */
            myConsumer = mySession.CreateDurableConsumer(new ActiveMQTopic(userTopicName), clientID, null, false);
            myConsumer.Listener += new MessageListener(Consumer_Listener);

            /* Start to listen... */
            BeginInvoke(new Action(() => {
                this.btnStop.Enabled = true;
            }));

            /* Cancel to background working */
            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        public void Consumer_Listener(IMessage message) {
            /* Write message to console */
            BeginInvoke(new Action(() => { txtConsole.AppendText("Message Received:  " + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss")); }));
            BeginInvoke(new Action(() => { txtConsole.AppendText(Environment.NewLine); }));
            BeginInvoke(new Action(() => { txtConsole.AppendText("----------------------------------------------------------------------"); }));
            BeginInvoke(new Action(() => { txtConsole.AppendText(Environment.NewLine); }));
            BeginInvoke(new Action(() => { txtConsole.AppendText(((ITextMessage)message).Text); }));
            BeginInvoke(new Action(() => { txtConsole.AppendText(Environment.NewLine); }));
            BeginInvoke(new Action(() => { txtConsole.AppendText(Environment.NewLine); }));

            /* Convert message to Agent Message Object */
            BeginInvoke(new Action(() => {
                /* Returned message */
                AgentMessage msg = JsonConvert.DeserializeObject<AgentMessage>(((ITextMessage)message).Text);

                /* If status changed.. */
                if (msg.type == "statusChanged")
                {

                }

                /* If new object created.. */
                if (msg.type == "created")
                {

                }
            }));
            
        }

        private async void myBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            /* Clear connections */
            myConsumer.Close();
            mySession.Close();
            myConsumer.Dispose();
            mySession.Dispose();
            mySession = null;
            myConsumer = null;

            if(myConnection.IsStarted)
            {
                myConnection.Stop();
            }

            await myConnection.CloseAsync();
            myConnection.Dispose();

            /* Let's begin again */
            BeginInvoke(new Action(() => { this.btnStart.Enabled = true; }));
        }
    }
}
