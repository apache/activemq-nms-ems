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

namespace Apache.NMS.EMS
{
	public class Topic : Apache.NMS.EMS.Destination, Apache.NMS.ITopic
	{
		public TIBCO.EMS.Topic tibcoTopic
		{
			get { return this.tibcoDestination as TIBCO.EMS.Topic; }
			set { this.tibcoDestination = value; }
		}

		public Topic(TIBCO.EMS.Topic topic)
			: base(topic)
		{
		}

        public void Dispose()
        {
        }
        
        #region ITopic Members

		public string TopicName
		{
			get { return this.tibcoTopic.TopicName; }
		}

		#endregion

		#region IDestination Members

		public Apache.NMS.DestinationType DestinationType
		{
			get { return Apache.NMS.DestinationType.Topic; }
		}

		public bool IsTopic
		{
			get { return true; }
		}

		public bool IsQueue
		{
			get { return false; }
		}

		public bool IsTemporary
		{
			get { return false; }
		}

		#endregion

		/// <summary>
		/// </summary>
		/// <returns>string representation of this instance</returns>
		public override System.String ToString()
		{
			return "topic://" + TopicName;
		}
	}
}