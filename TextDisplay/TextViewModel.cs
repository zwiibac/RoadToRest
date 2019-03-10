using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Nancy.Hosting.Self;

namespace TextDisplay
{
    public class TextViewModel : INotifyPropertyChanged, ITextApi, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> _text = new List<string>();
        private NancyHost _host;

        public TextViewModel()
        {
            Server.Bootstrapper bootStrapper = new Server.Bootstrapper(this);

            var hostConfigs = new HostConfiguration()
            {
                UrlReservations = new UrlReservations() { CreateAutomatically = true }
            };

            _host = new NancyHost(bootStrapper, hostConfigs, new Uri("http://localhost:1234/api/"));
            _host.Start();
        }

        public string Text
        {
            get { return string.Join(Environment.NewLine, _text); }
            set
            {
                _text = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();
                _PropertyChanged();
            }
        }

        public string Status { get; private set; } = " ";

        string[] ITextApi.Content => Application.Current.Dispatcher.Invoke(() => { return _text.ToArray(); });

        string ITextApi.Status
        {
            set
            {
                Application.Current.Dispatcher.Invoke(() => { Status=value; _PropertyChanged(); });
            }
        }

        void ITextApi.AppendLine(string line)
        {
            Application.Current.Dispatcher.Invoke(() => { AppendLine(line); });
        }

        void ITextApi.Clear()
        {
            Application.Current.Dispatcher.Invoke(() => { Clear(); });
        }

        string ITextApi.GetLine(int lineNumber)
        {
            return Application.Current.Dispatcher.Invoke(() => { return GetLine(lineNumber); });
        }

        void ITextApi.SetLine(int lineNumber, string line)
        {
            Application.Current.Dispatcher.Invoke(() => { SetLine(lineNumber, line); });
        }

        private void Clear()
        {
            _text.Clear();
            _PropertyChanged("Text");
        }

        private string GetLine(int lineNumber)
        {
            if (lineNumber <= _text.Count && lineNumber > 0)
            {
                return _text[lineNumber - 1];
            }

            return string.Empty;
        }

        private void SetLine(int lineNumber, string value)
        {
            if (lineNumber <= _text.Count && lineNumber > 0)
            {
                _text[lineNumber - 1] = value;
                _PropertyChanged("Text");
            }
        }

        private void AppendLine(string value)
        {
            _text.Add(value);
            _PropertyChanged("Text");
        }

        private void _PropertyChanged([CallerMemberName] string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            ((IDisposable)_host).Dispose();
        }
    }
}
