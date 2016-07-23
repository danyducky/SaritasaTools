﻿// Copyright (c) 2015-2016, Saritasa. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

#if !NETCOREAPP1_0 && !NETSTANDARD1_6
namespace Saritasa.Tools.Messages.MessagePipelineMiddlewares
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents performance counter that count total messages passed.
    /// </summary>
    public class PerformanceCounterMiddleware : IMessagePipelineMiddleware
    {
        /// <summary>
        /// Total processed messages counter.
        /// </summary>
        public const string TotalMessagesProcessed = "Total Messages Processed";

        /// <summary>
        /// Messages per second counter.
        /// </summary>
        public const string RateMessagesProcessed = "Messages per Second Processed";

        /// <summary>
        /// Average message processing duration counter.
        /// </summary>
        public const string AverageMessagesDuration = "Average Message Processing Duration";

        /// <summary>
        /// Base counter for average message execution duration counter.
        /// </summary>
        public const string AverageMessagesDurationBase = "Base Average Message Processing Duration";

        bool initialized = false;

        string category;

        PerformanceCounter performanceCounterTotal;
        PerformanceCounter performanceCounterRate;
        PerformanceCounter performanceCounterAvg;
        PerformanceCounter performanceCounterAvgBase;

        /// <inheritdoc />
        public string Id
        {
            get { return "perf-counter"; }
        }

        /// <summary>
        /// .ctor
        /// </summary>
        /// <param name="category">Performance counter category.</param>
        public PerformanceCounterMiddleware(string category = "Saritasa Tools Messages")
        {
            this.category = category;
        }

        private void Initialize()
        {
            if (!PerformanceCounterCategory.Exists(category))
            {
                CounterCreationDataCollection counterDataCollection = new CounterCreationDataCollection();

                var counterTotal = new CounterCreationData(TotalMessagesProcessed, "Total processed messages by application",
                    PerformanceCounterType.NumberOfItems64);
                counterDataCollection.Add(counterTotal);
                var counterRate = new CounterCreationData(RateMessagesProcessed, "Average messages per second",
                    PerformanceCounterType.RateOfCountsPerSecond64);
                counterDataCollection.Add(counterRate);
                var counterAvg = new CounterCreationData(AverageMessagesDuration, "Average message processing time in ms",
                    PerformanceCounterType.AverageCount64);
                counterDataCollection.Add(counterAvg);
                var counterAvgBase = new CounterCreationData(AverageMessagesDurationBase,
                    "Base average message processing time in ms",
                    PerformanceCounterType.AverageBase);
                counterDataCollection.Add(counterAvgBase);

                PerformanceCounterCategory.Create(category, "Saritasa Tools Messages", PerformanceCounterCategoryType.SingleInstance,
                    counterDataCollection);
            }

            performanceCounterTotal = new PerformanceCounter(category, TotalMessagesProcessed, false);
            performanceCounterRate = new PerformanceCounter(category, RateMessagesProcessed, false);
            performanceCounterAvg = new PerformanceCounter(category, AverageMessagesDuration, false);
            performanceCounterAvgBase = new PerformanceCounter(category, AverageMessagesDurationBase, false);
        }

        /// <inheritdoc />
        public void Handle(Message message)
        {
            if (!initialized)
            {
                Initialize();
            }

            performanceCounterTotal.Increment();
            performanceCounterRate.Increment();
            performanceCounterAvg.IncrementBy(message.ExecutionDuration);
            performanceCounterAvgBase.Increment();
        }
    }
}
#endif
