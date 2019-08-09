using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReplaceWordServer.Server
{
   public  class HttpServer : IDisposable
    {
        private HttpListener _httpListener = null;
        private Thread _connectionThread = null;
        private Boolean _running, _disposed;


        private HttpResourceLocator _resourceLocator = null;


        public HttpServer(params string[] prefix)
        {
            if (!HttpListener.IsSupported)
            {
                // Requires at least a Windows XP with Service Pack 2
                throw new NotSupportedException(
                    "The Http Server cannot run on this operating system.");
            } // end if HttpListener is not supported

            _httpListener = new HttpListener();
            // Add the prefixes to listen to
            foreach (String p in prefix)
            {
                _httpListener.Prefixes.Add(p);
            }
            

            _resourceLocator = new HttpResourceLocator();

        }

        public bool IsListening()
        {
            return _httpListener.IsListening;
        }

        public void Start()
        {
            _httpListener.IgnoreWriteExceptions = true;
            if (!_httpListener.IsListening)
            {
                _httpListener.Start();
                _running = true;
                // Use a thread to listen to the Http requests
                _connectionThread = new Thread(new ThreadStart(this.ConnectionThreadStart));
                _connectionThread.Start();
            } // end if httpListener is not listening

        }


        public void Stop()
        {
            if (_httpListener.IsListening)
            {
                _running = false;
                _httpListener.Stop();
            } // end if httpListener is listening
        }


        public virtual void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }
            if (disposing)
            {
                if (this._running)
                {
                    this.Stop();
                }
                if (this._connectionThread != null)
                {
                    this._connectionThread.Abort();
                    this._connectionThread = null;
                }
            }
            this._disposed = true;
        }


        private void ConnectionThreadStart()
        {
            try
            {
                while (_running)
                {
                    // Grab the context and pass it to the HttpResourceLocator to handle it
                    HttpListenerContext context = _httpListener.GetContext();
                    _resourceLocator.HandleContext(context);

                } // while running
            }
            catch (HttpListenerException)
            {
                // This will occurs when the listener gets shutdown.
                Console.WriteLine("HTTP server was shut down.");
            } // end try-catch

        }


        public void AddHttpRequestHandler(HttpRequestHandler requestHandler)
        {
            _resourceLocator.AddHttpRequestHandler(requestHandler);
        }


    }
}
