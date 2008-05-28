/*
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;

namespace Apache.NMS.EMS
{
    /// <summary>
	/// Represents a NMS session to TIBCO.
    /// </summary>
    public class Session : Apache.NMS.ISession
    {
    	public readonly TIBCO.EMS.Session tibcoSession;
    	private bool closed = false;
    	private bool disposed = false;

        public Session(TIBCO.EMS.Session session)
        {
        	this.tibcoSession = session;
		}

		~Session()
		{
			Dispose(false);
		}

		#region ISession Members

		public Apache.NMS.IMessageProducer CreateProducer()
        {
            return CreateProducer(null);
        }

		public Apache.NMS.IMessageProducer CreateProducer(Apache.NMS.IDestination destination)
        {
			Apache.NMS.EMS.Destination destinationObj = (Apache.NMS.EMS.Destination) destination;

			return EMSConvert.ToNMSMessageProducer(this, this.tibcoSession.CreateProducer(destinationObj.tibcoDestination));
        }

		public Apache.NMS.IMessageConsumer CreateConsumer(Apache.NMS.IDestination destination)
        {
			Apache.NMS.EMS.Destination destinationObj = (Apache.NMS.EMS.Destination) destination;

			return EMSConvert.ToNMSMessageConsumer(this, this.tibcoSession.CreateConsumer(destinationObj.tibcoDestination));
        }

		public Apache.NMS.IMessageConsumer CreateConsumer(Apache.NMS.IDestination destination, string selector)
        {
			Apache.NMS.EMS.Destination destinationObj = (Apache.NMS.EMS.Destination) destination;

			return EMSConvert.ToNMSMessageConsumer(this, this.tibcoSession.CreateConsumer(destinationObj.tibcoDestination, selector));
		}

		public Apache.NMS.IMessageConsumer CreateConsumer(Apache.NMS.IDestination destination, string selector, bool noLocal)
        {
			Apache.NMS.EMS.Destination destinationObj = (Apache.NMS.EMS.Destination) destination;

			return EMSConvert.ToNMSMessageConsumer(this, this.tibcoSession.CreateConsumer(destinationObj.tibcoDestination, selector, noLocal));
        }

		public Apache.NMS.IMessageConsumer CreateDurableConsumer(Apache.NMS.ITopic destination, string name, string selector, bool noLocal)
        {
			Apache.NMS.EMS.Topic topicObj = (Apache.NMS.EMS.Topic) destination;

			return EMSConvert.ToNMSMessageConsumer(this, this.tibcoSession.CreateDurableSubscriber(topicObj.tibcoTopic, name, selector, noLocal));
        }

		public Apache.NMS.IQueue GetQueue(string name)
        {
			return EMSConvert.ToNMSQueue(this.tibcoSession.CreateQueue(name));
        }

		public Apache.NMS.ITopic GetTopic(string name)
        {
			return EMSConvert.ToNMSTopic(this.tibcoSession.CreateTopic(name));
        }

		public Apache.NMS.ITemporaryQueue CreateTemporaryQueue()
        {
			return EMSConvert.ToNMSTemporaryQueue(this.tibcoSession.CreateTemporaryQueue());
        }

		public Apache.NMS.ITemporaryTopic CreateTemporaryTopic()
        {
			return EMSConvert.ToNMSTemporaryTopic(this.tibcoSession.CreateTemporaryTopic());
        }

		public Apache.NMS.IMessage CreateMessage()
        {
			return EMSConvert.ToNMSMessage(this.tibcoSession.CreateMessage());
        }

		public Apache.NMS.ITextMessage CreateTextMessage()
        {
			return EMSConvert.ToNMSTextMessage(this.tibcoSession.CreateTextMessage());
        }

		public Apache.NMS.ITextMessage CreateTextMessage(string text)
        {
			return EMSConvert.ToNMSTextMessage(this.tibcoSession.CreateTextMessage(text));
        }

		public Apache.NMS.IMapMessage CreateMapMessage()
        {
			return EMSConvert.ToNMSMapMessage(this.tibcoSession.CreateMapMessage());
        }

		public Apache.NMS.IBytesMessage CreateBytesMessage()
        {
			return EMSConvert.ToNMSBytesMessage(this.tibcoSession.CreateBytesMessage());
        }

		public Apache.NMS.IBytesMessage CreateBytesMessage(byte[] body)
        {
			Apache.NMS.IBytesMessage bytesMessage = CreateBytesMessage();

			if(null != bytesMessage)
			{
				bytesMessage.Content = body;
			}

			return bytesMessage;
        }

		public Apache.NMS.IObjectMessage CreateObjectMessage(Object body)
		{
			return EMSConvert.ToNMSObjectMessage(this.tibcoSession.CreateObjectMessage(body));
		}
		
        public void Commit()
        {
			this.tibcoSession.Commit();
        }
        
        public void Rollback()
        {
			this.tibcoSession.Rollback();
        }
        
        // Properties
        
        public bool Transacted
        {
            get { return this.tibcoSession.Transacted; }
        }

		public Apache.NMS.AcknowledgementMode AcknowledgementMode
        {
            get { return EMSConvert.ToAcknowledgementMode(this.tibcoSession.SessionAcknowledgeMode); }
        }

        public void Close()
        {
			lock(this)
			{
				if(closed)
				{
					return;
				}

				this.tibcoSession.Close();
				closed = true;
			}
        }

		#endregion

		#region IDisposable Members
		
		///<summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		///</summary>
		///<filterpriority>2</filterpriority>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected void Dispose(bool disposing)
		{
			if(disposed)
			{
				return;
			}

			if(disposing)
			{
				// Dispose managed code here.
			}

			try
			{
				Close();
			}
			catch
			{
				// Ignore errors.
			}

			disposed = true;
		}

		#endregion
	}
}
