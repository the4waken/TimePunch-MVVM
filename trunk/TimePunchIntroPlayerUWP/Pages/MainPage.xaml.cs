using System;
using System.Reflection;
using System.Threading.Tasks;
using TimePunch.MVVM.EventAggregation;
using TimePunchIntroPlayer.Events;
using TimePunchIntroPlayerUWP.Core;
using TimePunchIntroPlayerUWP.Events.Exceptions;
using Windows.ApplicationModel;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TimePunchIntroPlayerUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
        , IHandleMessage<OpenVideoEvent>
        , IHandleMessage<PauseVideoEvent>
        , IHandleMessage<PlayVideoEvent>
        , IHandleMessage<ChangeVolumeEvent>
        , IHandleMessage<SeekToPlayerPositionEvent>
        , IHandleMessageAsync<MessageBoxException>
        , IDisposable
    {

        public MainPage()
        {
            InitializeComponent();
            TimePunchIntroPlayerUWPKernel.Get().EventAggregator.Subscribe(this);

            PositionUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            PositionUpdateTimer.Tick += OnUpdatePosition;
        }

        #region Fields

        private TimeSpan OldPosition;

        private double currentPosition;

        #endregion

        //Properties
        #region Controlling Videoposition Properties

        public DispatcherTimer PositionUpdateTimer { get; private set; }

        private void OnUpdatePosition(object sender, object e)
        {
            if (VideoPlayer != null)
            {
                if (OldPosition != VideoPlayer.Position)
                {
                    TimePunchIntroPlayerUWPKernel.Get().EventAggregator.PublishMessage(new VideoPositionChangedEvent(VideoPlayer.Position));
                    OldPosition = VideoPlayer.Position;
                }
            }
        }

        #endregion

        #region PlayVideoProperties

        public void Handle(OpenVideoEvent message)
        {
            if (message.FileStream != null)
            {
                VideoPlayer.SetSource(message.FileStream, message.Type);

                currentPosition = message.Position;

                PositionUpdateTimer.Start();
            }
        }

        public void Handle(PauseVideoEvent message)
        {
            VideoPlayer.Pause();
            PositionUpdateTimer.Stop();
        }

        public void Handle(PlayVideoEvent message)
        {
            VideoPlayer.Play();
            PositionUpdateTimer.Start();
        }


        #endregion

        #region MediaEvents

        private void VideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Position = TimeSpan.Zero;
            VideoPlayer.Play();
        }

        private void VideoPlayer_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            TimePunchIntroPlayerUWPKernel.Get().EventAggregator.PublishMessage(new MediaFailedEvent());
        }

        private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (!VideoPlayer.NaturalDuration.HasTimeSpan)
            {
                return;
            }

            if (VideoPlayer.CanSeek)
            {
                var timeSpan = TimeSpan.FromMilliseconds(currentPosition);
                VideoPlayer.Position = timeSpan;
                VideoPlayer.Play();
            }

            TimePunchIntroPlayerUWPKernel.Get().EventAggregator.PublishMessage(new MediaOpenedEvent());
            VideoPositionDefiner.Maximum = VideoPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        #endregion

        //Events that get called by the users action
        #region UserperformedEvents

        public void Handle(SeekToPlayerPositionEvent message)
        {
            try
            {
                var timeSpan = TimeSpan.FromMilliseconds(message.Position);
                VideoPlayer.Position = timeSpan;
            }
            catch (Exception e)
            {
                TimePunchIntroPlayerUWPKernel.Get().EventAggregator.PublishMessage(new MessageBoxException(e.Message));
            }
        }

        async Task<MessageBoxException> IHandleMessageAsync<MessageBoxException>.Handle(MessageBoxException message)
        {
            var errorDialog = new MessageDialog(message.ExceptionMessage);
            await errorDialog.ShowAsync();

            return message;
        }

        public void Handle(ChangeVolumeEvent message)
        {
            VideoPlayer.Volume = message.Volume / 100.0;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TimePunchIntroPlayerUWPKernel.Get().EventAggregator.Unsubscribe(this);
                    PositionUpdateTimer.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }


        #endregion
    }
}
