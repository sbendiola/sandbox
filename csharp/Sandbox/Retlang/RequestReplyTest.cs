using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Retlang;

namespace Sandbox.Retlang
{
    [TestFixture]
    public class RequestReplyTest
    {

        public delegate T Resolver<T>();
        public class Publisher
        {
            public IUnsubscriber Publish<I, O>(Channel<Resolver<I>> input, I i, Action<O> success)
            {
                Channel<O> channel = new Channel<O>();
                IUnsubscriber unsubscriber = channel.Subscribe(null, success);
        //        input.Publish(i);
       //         MessageEnvelope
                return unsubscriber;
            }

        
        }

        public class Envelope<I, R>
        {
            private readonly IChannel<R> response = new Channel<R>();
            private readonly I message;

            public Envelope(I message)
            {
                this.response = response;
                this.message = message;
            }

            public IChannel<R> Sender
            {
                get { return response; }
            }

            public I Message
            {
                get { return message; }
            }

            public bool Respnd(R msg)
            {
                return response.Publish(msg);
            }
        }
    }
}
