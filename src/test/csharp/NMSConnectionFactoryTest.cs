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
using Apache.NMS;
using NUnit.Framework;
using System.Reflection;
using System.IO;

namespace Apache.NMS.ActiveMQ
{
    [TestFixture]
    public class NMSConnectionFactoryTest 
    {
        [Test]
        public void TestEmsURI()
        {
            NMSConnectionFactory factory = new NMSConnectionFactory("ems:tcp://localhost:7222");
            Assert.IsNotNull(factory);
            Assert.IsNotNull(factory.ConnectionFactory);
            Assert.IsTrue(factory.ConnectionFactory is Apache.NMS.EMS.ConnectionFactory);
        }

    }


}

