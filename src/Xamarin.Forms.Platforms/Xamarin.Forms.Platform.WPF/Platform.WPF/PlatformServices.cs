using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Xamarin.Forms.Platform.WPF
{
    using Xamarin.Forms.Internals;

    using Application = System.Windows.Application;

    class PlatformServices : IPlatformServices
    {
        HttpClient HttpClient = new HttpClient();

        Thread _uiThread;

        public bool IsInvokeRequired
        {
            get
            {
                lock (this)
                {
                    if (_uiThread == null && Application.Current!= null && Application.Current.Dispatcher!=null)
                       System.Windows. Application.Current.Dispatcher.Invoke(delegate { _uiThread = Thread.CurrentThread; });
                }

                return Thread.CurrentThread != _uiThread;
            }
        }

        public void BeginInvokeOnMainThread(Action action)
        {
                    if (Application.Current!= null && Application.Current.Dispatcher!=null)
                System.Windows.Application.Current.Dispatcher.BeginInvoke(action);
                    else
                    {
                        action();
                    }
        }

        public Ticker CreateTicker()
        {
            throw new NotImplementedException();
        }

        public ITimer CreateTimer(Action<object> callback, object state, int dueTime, int period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback)
        {
            return new ThreadTimer(callback);
        }

        public ITimer CreateTimer(Action<object> callback, object state, long dueTime, long period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback, object state, uint dueTime, uint period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public ITimer CreateTimer(Action<object> callback, object state, TimeSpan dueTime, TimeSpan period)
        {
            return new ThreadTimer(callback, state, dueTime, period);
        }

        public Assembly[] GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public string GetMD5Hash(string input)
        {
            throw new NotImplementedException();
        }

        public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
        {
            throw new NotImplementedException();
        }

        public async Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
        {
            if (!uri.IsAbsoluteUri || (uri.IsAbsoluteUri && uri.Scheme == "pack"))
            {
                // Build Action: Content, Copy Local: True
                try
                {
                    var contentInfo = System.Windows.Application.GetContentStream(uri);
                    if (contentInfo != null)
                        return contentInfo.Stream;
                }
                catch { }

                // Build Action: Resource
                try
                {
                    var resourceInfo = System.Windows.Application.GetResourceStream(uri);
                    if (resourceInfo != null)
                        return resourceInfo.Stream;
                }
                catch { }

                // Local file OR pack://siteoforigin:,,,/SiteOfOriginFile.ext
                try
                {
                    var remoteInfo = System.Windows.Application.GetRemoteStream(uri);
                    if (remoteInfo != null)
                        return remoteInfo.Stream;
                }
                catch { }
            }

            // Web file
            var response = await HttpClient.GetAsync(uri, cancellationToken);
            return await response.Content.ReadAsStreamAsync();
        }

        public IIsolatedStorageFile GetUserStoreForApplication()
        {
            var scope = IsolatedStorageScope.User | IsolatedStorageScope.Assembly | IsolatedStorageScope.Domain;
            var isolatedStorage = System.IO.IsolatedStorage.IsolatedStorageFile.GetStore(scope, null, null);
            return new IsolatedStorageFile(isolatedStorage);
        }

        public void OpenUriAction(Uri uri)
        {
            System.Diagnostics.Process.Start(uri.ToString());
        }

        public void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            Timer timer = null;
            TimerCallback timerCallback = delegate
            {
                if (!callback())
                    timer.Dispose();
            };

            timer = new Timer(timerCallback, null, interval, interval);
        }
    }
}
