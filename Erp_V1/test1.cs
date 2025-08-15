using System;
using System.Diagnostics;
using System.IO;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Windows.Forms;

namespace Erp_V1
{
    public partial class test1 : Form
    {
        private SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        private SpeechSynthesizer speech = new SpeechSynthesizer();
        private bool isListening = false;
        private bool isWaitingForSearchQuery = false;

        public test1()
        {
            InitializeComponent();

            Choices choices = new Choices();
            string[] text = File.ReadAllLines(Environment.CurrentDirectory + "//grammar.txt");
            choices.Add(text);
            Grammar grammar = new Grammar(new GrammarBuilder(choices));
            recEngine.LoadGrammar(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recEngine_SpeechRecognized);
            speech.SelectVoiceByHints(VoiceGender.Female);
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isListening)
                {
                    recEngine.RecognizeAsync(RecognizeMode.Multiple);
                    label2.Text = "Listening...";
                    isListening = true;
                }
                else
                {
                    recEngine.RecognizeAsyncStop();
                    label2.Text = "Stopped listening.";
                    isListening = false;
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            try
            {
                string result = e.Result.Text.ToLower();

                if (isWaitingForSearchQuery)
                {
                    string searchQuery = result.Trim();
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        PerformSearch(searchQuery);
                        isWaitingForSearchQuery = false;
                        return;
                    }
                }

                if (!isWaitingForSearchQuery)
                {
                    StopListening();
                }

                string response = HandleCommand(result);

                if (!string.IsNullOrEmpty(response))
                {
                    speech.SpeakAsync(response);
                    label2.Text = response;

                    if (response.Contains("search on google") || response.Contains("search"))
                    {
                        speech.SpeakAsync("What do you want to search on Google?");
                        label2.Text = "What do you want to search on Google?";
                        isWaitingForSearchQuery = true;
                        StartListening();
                    }
                }
            }
            catch (Exception)
            {
                speech.SpeakAsync("Sorry, I could not understand. Could you please say that again?");
                label2.Text = "Sorry, I could not understand. Could you please say that again?";
                isWaitingForSearchQuery = false;
                StartListening();
            }
        }

        private string HandleCommand(string command)
        {
            string response = string.Empty;

            if (command.Contains("hello"))
            {
                response = "Hello, I'm AI Assistant. How can I help you?";
            }
            else if (command.Contains("what time is it"))
            {
                response = "It is currently " + DateTime.Now.ToString("h:mm tt");
            }
            else if (command.Contains("google"))
            {
                Process.Start("https://www.google.com/");
                response = "Opening Google.";
            }
            else if (command.Contains("wikipedia"))
            {
                Process.Start("https://www.wikipedia.org/");
                response = "Opening Wikipedia.";
            }
            else if (command.Contains("open calculator") || command.Contains("calculator") || command.Contains("cal"))
            {
                Process.Start("calc.exe");
                response = "Opening Calculator.";
            }
            else if (command.Contains("open settings") || command.Contains("settings"))
            {
                Process.Start("ms-settings:");
                response = "Opening Settings.";
            }
            else if (command.Contains("open camera") || command.Contains("camera"))
            {
                Process.Start("microsoft.windows.camera:");
                response = "Opening Camera.";
            }
            else if (command.Contains("open file explorer") || command.Contains("file explorer") || command.Contains("explorer"))
            {
                Process.Start("explorer.exe");
                response = "Opening File Explorer.";
            }
            else if (command.Contains("open control panel") || command.Contains("control panel"))
            {
                Process.Start("control.exe");
                response = "Opening Control Panel.";
            }
            else if (command.Contains("open task manager") || command.Contains("task manager"))
            {
                Process.Start("taskmgr.exe");
                response = "Opening Task Manager.";
            }
            else if (command.Contains("open edge") || command.Contains("edge"))
            {
                Process.Start("msedge.exe");
                response = "Opening Microsoft Edge.";
            }
            else if (command.Contains("open firefox") || command.Contains("firefox"))
            {
                Process.Start("firefox.exe");
                response = "Opening Firefox.";
            }
            else if (command.Contains("open chrome") || command.Contains("chrome"))
            {
                Process.Start("chrome.exe");
                response = "Opening Google Chrome.";
            }
            else if (command.Contains("open word") || command.Contains("word"))
            {
                Process.Start("winword.exe");
                response = "Opening Microsoft Word.";
            }
            else if (command.Contains("open excel") || command.Contains("excel"))
            {
                Process.Start("excel.exe");
                response = "Opening Microsoft Excel.";
            }
            else if (command.Contains("open powerpoint") || command.Contains("powerpoint"))
            {
                Process.Start("powerpnt.exe");
                response = "Opening Microsoft PowerPoint.";
            }
            else if (command.Contains("open spotify") || command.Contains("spotify"))
            {
                Process.Start("spotify.exe");
                response = "Opening Spotify.";
            }
            else if (command.Contains("open skype") || command.Contains("skype"))
            {
                Process.Start("skype.exe");
                response = "Opening Skype.";
            }
            else if (command.Contains("play music") || command.Contains("music"))
            {
                Process.Start("wmplayer.exe");
                response = "Opening Windows Media Player.";
            }

            else if (command.Contains("open"))
            {
                string itemToOpen = command.Replace("open", "").Trim().ToLower();

                // Match the form names with the ones used in OpenForm
                switch (itemToOpen)
                {
                    case "sales":
                    case "category":
                    case "customer":
                    case "product":
                    case "stock":
                    case "ai tools":
                        OpenForm(itemToOpen);
                        response = $"Opening {itemToOpen}.";
                        break;

                    case "notepad":
                        Process.Start("notepad.exe");
                        response = "Opening Notepad.";
                        break;

                    case "command prompt":
                    case "cmd":
                        Process.Start("cmd.exe");
                        response = "Opening Command Prompt.";
                        break;

                    case "calculator":
                    case "cal":
                        Process.Start("calc.exe");
                        response = "Opening Calculator.";
                        break;

                    default:
                        response = "Command not recognized.";
                        break;
                }
            }
            else
            {
                response = "Command not recognized.";
            }

            return response;
        }


        private void OpenForm(string formName)
        {
            Form formToOpen = null;

            if (formName.Equals("sales", StringComparison.OrdinalIgnoreCase))
                formToOpen = new FrmSales();
            else if (formName.Equals("category", StringComparison.OrdinalIgnoreCase))
                formToOpen = new FrmCategory();
            else if (formName.Equals("customer", StringComparison.OrdinalIgnoreCase))
                formToOpen = new FrmCustomerList();
            else if (formName.Equals("product", StringComparison.OrdinalIgnoreCase))
                formToOpen = new FrmProductList();
            else if (formName.Equals("stock", StringComparison.OrdinalIgnoreCase))
                formToOpen = new FrmStockAlert();
            //else if (formName.Equals("ai tools", StringComparison.OrdinalIgnoreCase))
            //    formToOpen = new frmRs();

            if (formToOpen != null)
            {
                speech.SpeakAsync($"Opening {formName}.");
                formToOpen.Show();
            }
            else
            {
                speech.SpeakAsync("I could not find the specified form. Please try again.");
            }
        }

        private void PerformSearch(string query)
        {
            Process.Start($"https://www.google.com/search?q={query}");
            speech.SpeakAsync($"Searching Google for {query}.");
            label2.Text = $"Searching Google for {query}.";
        }

        private void StartListening()
        {
            if (!isListening)
            {
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
                label2.Text = "Listening...";
                isListening = true;
            }
        }

        private void StopListening()
        {
            recEngine.RecognizeAsyncStop();
            label2.Text = "Stopped listening.";
            isListening = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialization code if needed
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            // Optional: Use this for manual speech synthesis if needed
            speech.SpeakAsync("Button clicked!");
        }
    }
}
